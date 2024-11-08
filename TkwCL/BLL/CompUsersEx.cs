using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TkwEF.BLL
{
    public partial class CompUser
    {
        #region Fields

        private bool? _allowVesChange = null;
        //[NotMapped]
        public bool AllowVesChange(TkwContext context)
        {
                if (!_allowVesChange.HasValue)
                {
                    //using (TkwContext context = new TkwContext())
                    //{
                        _allowVesChange = context.CompGrafs
                            .Where(w => (w.RedUser != null ? w.RedUser.CompId : w.BlueUser.CompId) == this.CompId)
                            .AsEnumerable()
                            .FirstOrDefault(w => ((w.RedUser?.UserClubId == this.UserClubId || w.BlueUser?.UserClubId == this.UserClubId) && w.GrafStatusId == (int)BLL.GrafStatus.Statuses.Accept && w.Actual)) == null;
                    //}
                }
                return _allowVesChange.Value;
        }

        //[NotMapped]
        public bool AllowUserDelete(TkwContext context)
        {
            //get
            //{
                bool res = false;
                //using (TkwContext context = new TkwContext())
                //{
                    res = context.CompGrafs
                        .Where(w => (w.RedUser != null ? w.RedUser.CompId : w.BlueUser.CompId) == this.CompId)
                        .AsEnumerable()
                        .FirstOrDefault(w => ((w.RedUser?.UserClubId == this.UserClubId || w.BlueUser?.UserClubId == this.UserClubId) && w.GrafStatusId == (int)BLL.GrafStatus.Statuses.Accept && w.Actual)) == null;
                //}
                return res;
            //}
        }

        public bool AllowPoyasChange(TkwContext context)
        {
            //get
            //{
                return AllowVesChange(context);
            //}
        }

        #endregion

        public static List<CompUser> GetList(int comp)
        {
            List<CompUser> res = null;
            using (BLL.TkwContext cntx = new TkwContext())
            {
                //res = cntx.CompUsers.Where(w => w.Comp.Id == comp && w.Actual).ToList();
                res = cntx.CompUsers.Include("UserClub").Include("UserClub.User").Include("UserClub.Club").Where(w => w.Comp.Id == comp && w.Actual).ToList();
            }
            return res;
        }
        public void ResetAllowVesChange()
        {
            _allowVesChange = null;
        }
    }
}
