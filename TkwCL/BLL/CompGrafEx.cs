using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TkwEF.Core.Extentions;

namespace TkwEF.BLL
{
    public partial class CompGraf
    {
        #region enum

        public enum TypeGraf { Olymp = 1, Turnir}

        #endregion

        #region Feilds

        private static volatile object _lockStat = new object();

        public bool AllowEditPobeditel()
        {
            return this.Id > 0 && this.GrafStatus?.Id == (int)GrafStatus.Statuses.New;
        }

        #region ByGrid

        public string StatusName { get { return this.GrafStatus?.Name ?? string.Empty; } }
        public string RedUserFIO { get { return RedUser?.FIO ?? string.Empty; } }
        public string BlueUserFIO { get { return BlueUser?.FIO ?? string.Empty; } }
        public string PobeditelFIO { get { return Pobeditel?.FIO ?? string.Empty; } }
        public decimal? Ves => RedUser == null ? (BlueUser == null ? null : BlueUser.Ves) : RedUser.Ves;
        public string VesToString => RedUser == null ? (BlueUser == null ? string.Empty : BlueUser.VesToString) : RedUser.VesToString;
        public string RedPoyasName => RedUser?.CurPoyasName;
        public string BluePoyasName => BlueUser?.CurPoyasName;

        #endregion

        #endregion

        #region Static

        #region Public

        /// <summary>
        /// Проверка данных перед составлением графика
        /// </summary>
        /// <returns></returns>
        public static async Task<List<CompGraf>> GetData(BLL.TkwContext context, int compId, TypeGraf type)
        {
            try
            {
                return await Task.Run(async () =>
                {
                    context.CompUsers.Load();
                    var grafUsers = await BLL.CompGraf.GetGrafUsersByCompAsync(context, compId);
                    //var users = CompUser.GetList(_compParam.Id);
                    var users = context.CompUsers.Include("UserClub").Include("UserClub.User").Include("UserClub.Club").Where(w => w.Comp.Id == compId && w.Actual).ToList();
                    if (users.Count == 0)
                        throw new BllException("Заполните участников соревнований.");
                    if (users.Any(c => (c.Ves ?? 0) == 0))
                        throw new BllException("Заполните вес для участников соревнования.");
                    //if (users.Any(c => (c.Poyas == null)))
                    //    throw new BllException("Не для всех участников соревнования выбран пояс.");
                    List<BLL.CompUser> v;
                    if ((v = users.Where(c => (c.UserPoyas == null && c.Actual)).ToList()).Any())
                        throw new BllException("Не для всех участников соревнования выбран пояс.");

                    List<List<CompUser>> groups = _fillUsersByVesGroup(context, compId, users);

                    //проверка на наличие участников, не вошедших ни в одну из групп
                    List<CompUser> lst = new List<CompUser>();// groups.SelectMany(groups.Select(s => s));
                    string msg = string.Empty;
                    for (int i = 0; i < groups.Count; i++)
                    {
                        lst.AddRange(groups[i]);
                    }
                    List<CompUser> exLst = users.Except(lst).ToList();
                    foreach (var item in exLst)
                        msg += string.Format("{0} ({1}), ", item.FIO, item.Ves);
                    if (msg.Length > 0)
                    {
                        msg = msg.Substring(0, msg.Length - 2);
                        throw new BllException($"Участники не попавшие в весовые группы{Environment.NewLine}{msg}");
                    }

                    var usersNotGraf = (from c in users
                                        join g in grafUsers on c.Id equals g.Id into cg
                                        from g in cg.DefaultIfEmpty()
                                        where g == null
                                        select c).ToList();

                    int etap = await BLL.CompGraf.GetEtapLast(context, compId);

                    List<CompGraf> compGrafs;
                    if (usersNotGraf.Count > 0)
                    {
                        groups = _fillUsersByVesGroup(context, compId, usersNotGraf);
                        if (type == TypeGraf.Olymp)
                            compGrafs = await GetByOlympAsync(context, groups, etap + 1);
                        else if (type == TypeGraf.Turnir)
                            compGrafs = await GetByTurnirAsync(context, groups, etap + 1);
                        else
                            return compGrafs = null;
                        return compGrafs;
                    }
                    //графики без победителя
                    var usersNotPobeditel = context.CompGrafs.Where(w => (w.RedUser != null ? w.RedUser.Comp.Id : w.BlueUser.Comp.Id) == compId
                        //&& w.GrafStatus.Id == (int)BLL.GrafStatus.Statuses.Accept
                        && (w.Pobeditel == null || (w.Pobeditel != null && w.GrafStatus.Id != (int)BLL.GrafStatus.Statuses.Accept)))
                        .Select(s => s.Pobeditel).ToList();
                    if (usersNotPobeditel.Count > 0)
                        throw new BllException($"Определены не все победители ({users.Count}).");

                    users = context.CompGrafs.Where(w => (w.RedUser != null ? w.RedUser.Comp.Id : w.BlueUser.Comp.Id) == compId
                                            && w.GrafStatus.Id == (int)BLL.GrafStatus.Statuses.Accept
                                            && w.Pobeditel != null
                                            && w.Etap == etap)
                                            .Select(s => s.Pobeditel).ToList();

                    groups = _fillUsersByVesGroup(context, compId, users);

                    if (type == TypeGraf.Olymp)
                        compGrafs = await GetByOlympAsync(context, groups, etap + 1);
                    else if (type == TypeGraf.Turnir)
                        compGrafs = await GetByTurnirAsync(context, groups, etap + 1);
                    else
                        return compGrafs = null;
                    return compGrafs;
                });
            }
            catch (BllException) { throw; }
            catch (Exception ex) { throw new BllException("Неожиданная ошибка при формировании графика соревнований уровня логики.", ex); }
        }
        /// <summary>
        /// Заполнение весовых групп участниками соревнования
        /// </summary>
        /// <param name="context"></param>
        /// <param name="compId"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        private static List<List<CompUser>> _fillUsersByVesGroup(TkwContext context, int compId, List<CompUser> users)
        {
            var vesKats = context.CompVesKates.Include("VesKat").Where(w => w.Comp.Id == compId && w.Actual).ToList();
            if (vesKats.Count == 0)
                throw new BllException("Выберите весовые категории для соревнования.");

            List<List<BLL.CompUser>> groups = new List<List<CompUser>>();
            for (int i = 0; i < vesKats.Count; i++)
            {
                BLL.CompVesKat vesKat = vesKats[i];
                groups.Add(users.Distinct().Where(w => w.Ves >= vesKat.BeginVes && w.Ves < vesKat.EndVes).ToList());
            }

            return groups;
        }

        /// <summary>
        /// Возвращает участников графика соревнования
        /// </summary>
        /// <param name="context"></param>
        /// <param name="comp"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static async Task<List<CompUser>> GetGrafUsersByCompAsync(TkwContext context, int comp, BLL.GrafStatus.Statuses? status = null)
        {
            TkwContext tkwContext;
            if (context == null)
                tkwContext = new TkwContext();
            else
                tkwContext = context;
            try
            {
                List<CompUser> res;
                lock (_lockStat)
                {
                    res = new List<CompUser>();
                }
                res = await Task.Run(() =>
                {
                    tkwContext.CompGrafs.Load();
                    var grafs = tkwContext.CompGrafs.Include("RedUser").Include("BlueUser").Include("GrafStatus").Include("RedUser.Comp").Include("BlueUser.Comp").ToList();
                    var lst = grafs.Where(w => (w.RedUser != null ? w.RedUser.Comp.Id : w.BlueUser.Comp.Id) == comp
                        && (status.HasValue ? w.GrafStatus.Id == (int)status : true)).ToList();
                    var redLst = lst.Where(w => w.RedUser != null).Select(s => s.RedUser).ToList();
                    var blueLst = lst.Where(w => w.BlueUser != null).Select(s => s.BlueUser).ToList();
                    List<CompUser> tmp = new List<CompUser>();
                    tmp.AddRange(redLst);
                    tmp.AddRange(blueLst);
                    return tmp;
                });
                lock (_lockStat)
                {
                    return res;
                }
            }
            finally
            {
                if (context == null)
                {
                    tkwContext.Dispose();
                    tkwContext = null;
                }
            }
        }
        //public static async Task<List<CompGraf>> GetByOlympAsync(BLL.TkwContext context, int compId)
        //{
        //    try
        //    {
        //        return await Task.Run(async () =>
        //        {
        //            context.CompUsers.Load();
        //            var grafUsers = await BLL.CompGraf.GetGrafUsersByCompAsync(context, compId);
        //            //var users = CompUser.GetList(_compParam.Id);
        //            var users = context.CompUsers.Include("UserClub").Include("UserClub.User").Include("UserClub.Club").Where(w => w.Comp.Id == compId && w.Actual).ToList();
        //            if (users.Count == 0)
        //                throw new BllException("Заполните участников соревнований.");
        //            if (users.Any(c => (c.Ves ?? 0) == 0))
        //                throw new BllException("Заполните вес для участников соревнования.");
        //            //if (users.Any(c => (c.Poyas == null)))
        //            //    throw new BllException("Не для всех участников соревнования выбран пояс.");
        //            List<BLL.CompUser> v;
        //            if ((v = users.Where(c => (c.Poyas == null)).ToList()).Count() > 0)
        //                throw new BllException("Не для всех участников соревнования выбран пояс.");

        //            var usersNotGraf = (from c in users
        //                                join g in grafUsers on c.Id equals g.Id into cg
        //                                from g in cg.DefaultIfEmpty()
        //                                where g == null
        //                                select c).ToList();

        //            var vesKats = context.CompVesKates.Include("VesKat").Where(w => w.Comp.Id == compId && w.Actual).ToList();
        //            if (vesKats.Count == 0)
        //                throw new BllException("Выберите весовые категории для соревнования.");

        //            int etap = await BLL.CompGraf.GetEtapLast(context, compId);

        //            List<CompGraf> compGrafs;
        //            if (usersNotGraf.Count > 0)
        //            {
        //                compGrafs = await _getGrafsBySrc(context, usersNotGraf, vesKats, etap);
        //                return compGrafs;
        //            }
        //            //графики без победителя
        //            users = context.CompGrafs.Where(w => (w.RedUser != null ? w.RedUser.Comp.Id : w.BlueUser.Comp.Id) == compId
        //                                    //&& w.GrafStatus.Id == (int)BLL.GrafStatus.Statuses.Accept
        //                                    && (w.Pobeditel == null || (w.Pobeditel != null && w.GrafStatus.Id != (int)BLL.GrafStatus.Statuses.Accept)))
        //                                    .Select(s => s.Pobeditel).ToList();
        //            if (users.Count > 0)
        //                throw new BllException($"Определены не все победители ({users.Count}).");

        //            users = context.CompGrafs.Where(w => (w.RedUser != null ? w.RedUser.Comp.Id : w.BlueUser.Comp.Id) == compId
        //                                    && w.GrafStatus.Id == (int)BLL.GrafStatus.Statuses.Accept
        //                                    && w.Pobeditel != null
        //                                    && w.Etap == etap)
        //                                    .Select(s => s.Pobeditel).ToList();
        //            compGrafs = await _getGrafsBySrc(context, users, vesKats, etap);

        //            return compGrafs;
        //        });
        //    }
        //    catch (BllException) { throw; }
        //    catch (Exception ex) { throw new BllException("Неожиданная ошибка при формировании графика соревнований уровня логики.", ex); }
        //}
        /// <summary>
        /// Возвращает номер последнего этапа соревнования
        /// </summary>
        /// <param name="context"></param>
        /// <param name="comp"></param>
        /// <returns></returns>
        public async static Task<int> GetEtapLast(TkwContext context, int comp)
        {
            TkwContext tkwContext;
            if (context == null)
                tkwContext = new TkwContext();
            else
                tkwContext = context;
            try
            {
                return await Task.Run(() =>
                {
                    var lst = tkwContext.CompGrafs.Where(w => (w.RedUser != null ? w.RedUser.Comp.Id : w.BlueUser.Comp.Id) == comp
                        && (w.GrafStatus.Id == (int)BLL.GrafStatus.Statuses.Accept)).ToList();
                    return lst.Count == 0 ? 0 : lst.Max(m => m.Etap);
                });
            }
            catch (BllException) { throw; }
            catch (Exception ex) { throw new BllException("Неожиданная ошибка при извлечении номера последнего этопа соревнований уровня логики", ex); }
            finally
            {
                if (context == null)
                {
                    tkwContext.Dispose();
                    tkwContext = null;
                }
            }
        }
        /// <summary>
        /// Относится ли график к соревнованию
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="w"></param>
        /// <returns></returns>
        private static bool IsThisComp(int comp, CompGraf w)
        {
            return (w.RedUser != null ? w.RedUser.Comp.Id : w.BlueUser.Comp.Id) == comp;
        }

        #endregion

        #region Private function

        /// <summary>
        /// Построение графика
        /// </summary>
        /// <param name="compUsers"></param>
        /// <param name="vesKats"></param>
        /// <returns></returns>
        private static async Task<List<CompGraf>> GetByOlympAsync(BLL.TkwContext context, List<List<CompUser>> groups, int etap)
        {
            List<BLL.CompGraf> compGrafs = new List<CompGraf>();
            BLL.GrafStatus status;

            await Task.Run(() =>
            {
                context.GrafStatuses.Load();
                status = context.GrafStatuses.Where(g => g.Id == (int)BLL.GrafStatus.Statuses.New).FirstOrDefault();
                if (status == null)
                    throw new BllException("Не удалось определить статус для построения графика");

                //int etap = await GetEtapLast(context, (compUsers.Count > 0 ? compUsers[0].CompId : 0)) + 1;

                for (int i = 0; i < groups.Count; i++)
                {
                    List<BLL.CompUser> cuList = groups[i];
                    int cnt = cuList.Count;
                    if (cnt == 0)
                    {
                        continue;
                    }
                    else if (cnt <= 2)
                    {
                        int compId = cuList[0].CompId;
                        int? idRed = cuList[0].Id;
                        int? idBlue = cuList.Count == 2 ? (int?)cuList[1].Id : null;
                        //Такой пары нет на предыдущем этапе
                        bool hasInPrevEtap = context.CompGrafs.Where(a => (a.RedUser != null ? a.RedUser.Comp.Id : a.BlueUser.Comp.Id) == compId
                            && a.Etap == etap - 1)
                            .AsEnumerable()
                            .Any(a => a.RedUserId.IN(idRed ?? 0, idBlue ?? 0) && a.BlueUserId.IN(idRed, idBlue));
                        if (hasInPrevEtap)
                            continue;
                    }
                    int nomer = 0;
                    while (cnt > 0)
                    {
                        BLL.CompUser cu = cuList[0];
                        CompGraf cg = new CompGraf { RedUser = cu };
                        cuList.RemoveAt(0);
                        if (cuList.Count > 0)
                        {
                            cu = cuList.FirstOrDefault(f => f.UserClub.Club.Id != cu.UserClub.Club.Id);//Первый из группы из другого клуба
                            if (cu == null)//остались только из этого клуба
                                cu = cuList[0];
                            cg.BlueUser = cu;
                            cuList.RemoveAt(cuList.IndexOf(cu));
                        }

                        cg.GrafStatus = status;
                        cg.Nomer = ++nomer;
                        cg.Etap = etap;
                        compGrafs.Add(cg);
                        cnt = cuList.Count;
                    }
                }
            });

            return compGrafs;
        }

        private static async Task<List<CompGraf>> GetByTurnirAsync(BLL.TkwContext context, List<List<CompUser>> groups, int etap)
        {
            //if (type == TypeGraf.Olymp)
            //{
            //    int compId = lst[0].CompId;
            //    List<CompGraf> grafsPrevEtap =
            //        context.CompGrafs.Where(w => (w.RedUser != null ? w.RedUser.Comp.Id : w.BlueUser.Comp.Id) == compId
            //        && w.Etap == etap - 1).ToList();
            //    if (grafsPrevEtap.Count < 3)
            //        return;

            //    CompGraf cg = new CompGraf { RedUser = lst[0], BlueUser = lst[1] };
            //    cg.GrafStatus = status;
            //    cg.Nomer = 1;
            //    cg.Etap = etap;
            //    compGrafs.Add(cg);

            //    List<CompUser> bronzaUsers =
            //        grafsPrevEtap.Where(w => w.GrafStatus.Id == (int)BLL.GrafStatus.Statuses.Accept
            //        && w.Pobeditel != null)
            //        .Select(s => s.RedUser.Equals(s.Pobeditel) ? s.BlueUser : s.RedUser).ToList();

            //    //List<CompUser> bronzaUsers = compGrafs.Where(w => w.Etap == etap && w.Actual)
            //    //    .Select(s => s.RedUser.Equals(s.Pobeditel) ? s.BlueUser : s.RedUser).ToList();
            //    if (bronzaUsers.Count != 2)
            //        throw new BllException("Ошибка логики программы при заполнении финального графика");
            //    cg = new CompGraf { RedUser = bronzaUsers[0], BlueUser = bronzaUsers[1] };
            //    cg.GrafStatus = status;
            //    cg.Nomer = 2;
            //    cg.Etap = etap;
            //    compGrafs.Add(cg);
            //}
            ////else if(lst.Count == 3)
            //else if(type == TypeGraf.Turnir)
            //{
            List<BLL.CompGraf> compGrafs = new List<CompGraf>();
            if ((groups?.Count ?? 0) == 0)
                return compGrafs;
            // Междусобойчик
            for (int i = 0; i < groups.Count; i++)
            {
                List<BLL.CompUser> users = groups[i];
                int cnt = users.Count;
                if (cnt == 0)
                    continue;
                else if (cnt <= 2 && etap > 1)
                    continue;
                for (int y = 0; y < cnt; y++)
                {
                    CompGraf graf;
                    //if (i < 2)
                    //{
                    //    graf = new CompGraf { RedUser = lst[0], BlueUser = lst[i + 1] };
                    //}
                    //else
                    //{
                    //    graf = new CompGraf { RedUser = lst[1], BlueUser = lst[2] };
                    //}
                    for (int z = y + 1; z < cnt; z++)
                    {
                        graf = new CompGraf { RedUser = users[y], BlueUser = users[z] };
                        graf.GrafStatusId = (int)BLL.GrafStatus.Statuses.New;
                        graf.Nomer = (i * cnt) + (y * cnt) + z + 1;
                        graf.Etap = etap;
                        compGrafs.Add(graf);
                    }
                }
            }
            return compGrafs;



                //int compId = users[0].CompId;
            //int cnt = users.Count(c => users[0].UserClub.UserId.IN(c.RedUser.UserClub.UserId, c.BlueUser.UserClub.UserId));
            //if (cnt == 2)
            //    return;
            //}
        }

        #endregion

        #endregion
    }
}
