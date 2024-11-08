using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using TkwEF.BLL;
using System.Data.Entity;
using TkwEF.Helper;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Text;
using System.Drawing;

namespace TkwEF.UI.Comp
{
    public partial class f_Comp : f_BUI
    {
        #region enum

        private enum Mode { Create, Edit }

        #endregion

        #region Fields

        private Mode _mode;
        private BLL.Competition _compParam { get; set; }

        #endregion

        #region ctor

        public f_Comp()
        {
            InitializeComponent();
            _mode = Mode.Create;
        }

        public f_Comp(int id) : this()
        {
            _fillCompParam(id);
            grdUsers.SetReadOnlyNot(new[] { ColumnVes });
        }

        #endregion

        #region Handled

        private void f_Comp_Load(object sender, EventArgs e)
        {
            if (_mode == Mode.Edit)
                this.Text = string.Format("Изменение соревнования за {0}", _compParam.DateBegin.ToShortDateString());
            else
                this.Text = "Создание соревнования";
        }

        private async void f_Comp_Shown(object sender, EventArgs e)
        {
            try
            {
                await _fillGrdUsers();
                await _fillGrdVesKates();
                _setEnabledMenu();
                if (_mode == Mode.Create)
                {
                    int? id = null;
                    f_Comp_edit frm = new f_Comp_edit();
                    frm.FormClosed += (s, ev) => { frm.Dispose(); };
                    DialogResult dr = await frm.ShowDialogAsync(this);
                    if (dr == DialogResult.OK)
                    {
                        id = frm.RetVal.Value;
                        _mode = Mode.Edit;
                    }
                    else if (dr == DialogResult.Cancel)
                    {
                        this.Close();
                    }
                    else
                    {
                        new UiException("Неопределенная ошибка при создании соревнования").ShowMessage(this);
                        this.Close();
                    }
                    if (id.HasValue)
                        _fillCompParam(id.Value);
                    await _fillGrdUsers();
                }

                if (_mode == Mode.Edit)
                {
                    await _fillGrafs(false);
                }
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException("Ошибка при создании соревнования", ex).ShowMessage(this);
            }
        }

        private async void tsbAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                tsbAddUser.Enabled = false;
                int[] vals = null;
                //Sprav.f_SpravUserClub.Mode mode = Sprav.f_SpravUserClub.Mode.Add | Sprav.f_SpravUserClub.Mode.Delete | Sprav.f_SpravUserClub.Mode.Edit | Sprav.f_SpravUserClub.Mode.SelectMore;
                using (UI.Sprav.f_SpravUserClub frm = new Sprav.f_SpravUserClub(comp: _compParam.Id))
                {
                    DialogResult dr = await frm.ShowDialogAsync(this);
                    if (dr == DialogResult.OK)
                        vals = frm.RetVals;
                }
                if (vals != null)
                {
                    //using (BLL.TkwContext context = new TkwContext())
                    //{
                    BLL.Competition comp = await Task.Run(() => context.Competitions.First(f => f.Id == _compParam.Id));
                    foreach (var val in vals)
                    {
                        BLL.UserClub userClub = await Task.Run(() => context.UserClubs.Where(w => w.Id == val).First());
                        //BLL.CompUser compUser = new BLL.CompUser { Comp = _compParam, UserClub = userClub, Actual = true, Ves = 0 };
                        BLL.CompUser compUser = context.CompUsers.Create();
                        compUser.Comp = comp;
                        compUser.UserClub = userClub;
                        compUser.Ves = 0.00m;
                        compUser.UserPoyasId = userClub?.CurPoyas?.Id;
                        await Task.Run(() => context.CompUsers.Add(compUser));
                    }
                    await Task.Run(() => context.SaveChanges());
                    //}
                }
                await _fillGrdUsers();
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex) { new UiException("Неизвестная ошибка при добавлении участника соревнования", ex).ShowMessage(this); }
            finally
            {
                _setEnabledMenu();
            }
        }

        private async void tsbAddVesKat_Click(object sender, EventArgs e)
        {
            try
            {
                tsbAddVesKat.Enabled = false;
                int[] vals = null;
                Sprav.f_SpravVesKat.Mode mode = Sprav.f_SpravVesKat.Mode.Add | Sprav.f_SpravVesKat.Mode.Delete | Sprav.f_SpravVesKat.Mode.Edit | Sprav.f_SpravVesKat.Mode.SelectMore;
                IEnumerable<BLL.VesKat> src = _getSrcVesKat();
                using (UI.Sprav.f_SpravVesKat frm = new Sprav.f_SpravVesKat(src: src, cap: "Добавление весовой категории в соревнование", mode: mode))
                {
                    frm.Added += (s, ev) =>
                    {
                        frm.FillGrd(_getSrcVesKat());
                    };
                    DialogResult dr = frm.ShowDialog(this);
                    if (dr == DialogResult.OK)
                        vals = frm.RetVals;
                }
                if (vals != null)
                {
                    using (BLL.TkwContext tkwContext = new TkwContext())
                    {
                        Competition comp = await Task.Run(() => tkwContext.Competitions.First(f => f.Id == _compParam.Id));
                        foreach (var val in vals)
                        {
                            BLL.VesKat vesKat = await Task.Run(() => tkwContext.VesKates.Where(w => w.Id == val).FirstOrDefault());
                            BLL.CompVesKat compVesKat = new CompVesKat { Comp = comp, VesKat = vesKat, Actual = true };
                            await Task.Run(() => tkwContext.CompVesKates.Add(compVesKat));
                        }
                        await Task.Run(() => tkwContext.SaveChanges()); 
                    }
                }
                await _fillGrdVesKates();
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex) { new UiException("Неизвестная ошибка при добавлении участника соревнования", ex).ShowMessage(this); }
            finally
            {
                _setEnabledMenu();
            }
        }

        private async void tsbDeleteVesKat_Click(object sender, EventArgs e)
        {
            try
            {
                tsbDeleteVesKat.Enabled = false;
                if (bsVesKat.Current == null)
                    return;
                bool hasInVesKat;
                if (grdVesKat.SelectedRows.Count == 0)
                {
                    BLL.CompVesKat cvk = (BLL.CompVesKat)bsVesKat.Current;
                    hasInVesKat = _hasInVesKat(cvk);
                    if (hasInVesKat)
                    {
                        MessageBox.Show(this, $"Нельзя удалить весовую категорию ({cvk.BeginVes}-{cvk.EndVes}) с соревнования" +
                            $"{Environment.NewLine}Существуют участники, входящие в данную весовую категорию."
                            , "Удаление весовой категории", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        return;
                    }
                    if (MessageBox.Show(this, $"Удалить весовую категорию ({cvk.BeginVes}-{cvk.EndVes}) с соревнования"
                        , "Удаление весовой категории", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;
                    cvk.Actual = false;
                }
                else
                {
                    if (MessageBox.Show(this, $"Удалить весовые категории ({grdVesKat.SelectedRows.Count}) с соревнования"
                        , "Удаление весовых категорий", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;
                    foreach (DataGridViewRow row in grdVesKat.SelectedRows)
                    {
                        BLL.CompVesKat compVesKat = (BLL.CompVesKat)row.DataBoundItem;
                        hasInVesKat = _hasInVesKat(compVesKat);
                        if (hasInVesKat)
                        {
                            MessageBox.Show(this, $"Нельзя удалить весовую категорию ({compVesKat.BeginVes}-{compVesKat.EndVes}) с соревнования" +
                                $"{Environment.NewLine}Существуют участники, входящие в данную весовую категорию."
                                , "Удаление весовой категории", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                            continue;
                        }
                        compVesKat.Actual = false;
                    }
                }
                context.SaveChanges();
                await _fillGrdVesKates();
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex) { new UiException("Неизвестная ошибка при добавлении участника соревнования", ex).ShowMessage(this); }
            finally
            {
                _setEnabledMenu();
            }
        }

        private bool _hasInVesKat(CompVesKat cvk)
        {
            return context.CompUsers.Where(w => w.CompId == _compParam.Id && w.Actual).AsEnumerable().Any(w => cvk.InRange(w.Ves ?? 0));
        }

        #endregion

        #region Private function

        private async Task _fillGrdUsers()
        {
            IList<BLL.CompUser> src = null;
            if (_compParam != null)
            {
                src = await Task.Run(() => context.CompUsers.Where(w => w.Comp.Id == _compParam.Id && w.Actual)
                    .OrderBy(o => o.UserClub.User.Fam).ThenBy(t => t.UserClub.User.Name).ThenBy(t => t.UserClub.User.Otch).ThenBy(t => t.Id)
                    .ToList());
            }
            bsUsers.DataSource = src;
        }

        private void _fillCompParam(int id)
        {
            _compParam = context.Competitions.FirstOrDefault(w => w.Id == id);
            if (_compParam != null)
                _mode = Mode.Edit;
            else
                _mode = Mode.Create;
        }

        private async Task _fillGrdVesKates()
        {
            List<BLL.CompVesKat> src;
            await Task.Run(() => context.CompVesKates.Load());
            src = await Task.Run(() => context.CompVesKates.Local.Where(w => (w.Comp?.Id ?? 0) == _compParam?.Id && w.Actual).OrderBy(o => o.BeginVes).ThenBy(t => t.EndVes).ToList());
            bsVesKat.DataSource = new BindingList<BLL.CompVesKat>(src);
        }
        /// <summary>
        /// Возвращает набор весовых категорий, которые не входят набор соревнования
        /// </summary>
        /// <returns></returns>
        private IEnumerable<VesKat> _getSrcVesKat()
        {
            ObservableCollection<VesKat> vc;
            List<VesKat> lstComp;
            var cvk = context.CompVesKates.Local;
            lstComp = cvk.Where(w => (w.Comp?.Id ?? 0) == _compParam.Id && w.Actual).Select(s => s.VesKat).ToList();
            context.VesKates.Load();
            vc = context.VesKates.Local;
            var src = vc.Except(lstComp).ToList();

            //var cvk = context.VesKates.Local;
            //var lstComp = context.CompVesKates.Local.Where(w => w.Comp.Id == _compParam.Id && w.Actual).Select(s => s.VesKat).ToList();
            return src;
        }

        private void _setEnabledMenu()
        {
            tsUserClub.SuspendLayout();
            tsGraf.SuspendLayout();
            this.SuspendLayout();

            tsbAddUser.Enabled = true;
            tsbDeleteUser.Enabled = bsUsers.Position >= 0;
            tsbAddVesKat.Enabled = true;
            tsbDeleteVesKat.Enabled = bsVesKat.Position >= 0;
            tsUserClub.Enabled = true;

            tsbEditGraf.Enabled = true;
            tsGraf.Enabled = true;

            tsUserClub.ResumeLayout(false);
            tsUserClub.PerformLayout();
            tsGraf.ResumeLayout(false);
            tsGraf.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private async void grdUsers_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (grdUsers.IsDisposed || grdUsers.Disposing)
                return;
            if (e.ColumnIndex == ColumnVes.Index)
            {
                if (grdUsers.IsCurrentRowDirty)
                {
                    if (grdUsers[e.ColumnIndex, e.RowIndex].ValueType.Equals(typeof(decimal?)))
                    {
                        decimal valNew;
                        if (!decimal.TryParse(e.FormattedValue.ToString(), out valNew))
                            valNew = 0;
                        bool res;
                        if (valNew == 0)
                            res = false;
                        else
                            res = await Task.Run(() => context.CompVesKates.Where(w => w.CompId == _compParam.Id && w.Actual)
                                .AsEnumerable().Any(a => valNew == 0 || a.VesKat.InRange(valNew)));
                        if (grdUsers.IsDisposed || grdUsers.Disposing)
                            return;
                        if (!res)
                        {
                            MessageBox.Show(this, $"Выберите весовую категорию для веса {valNew.ToString("g")}кг.", "Заполнение веса");
                            grdUsers.CurrentCell = grdUsers[e.ColumnIndex, e.RowIndex];
                            grdUsers[e.ColumnIndex, e.RowIndex].Value = 0.00m;
                            grdUsers.CancelEdit();
                            return;
                        }
                        await context.SaveChangesAsync();
                    }
                }
                //else
                //    grdUsers[e.ColumnIndex, e.RowIndex].Value = 0.00m;
            }
        }

        private async void tsbEditGraf_Click(object sender, EventArgs e)
        {
            try
            {
                tsbEditGraf.Enabled = false;
                //IEnumerable<CompGraf> compGrafs = (IEnumerable<CompGraf>)bsGrafs.DataSource;
                DialogResult dr;
                this.Hide();
                using (UI.Graf.f_Graf frm = new Graf.f_Graf(_compParam?.Id ?? 0))
                {
                    frm.FormClosed += (s, ev) => { this.Visible = true; this.BringToFront(); };
                    dr = await frm.ShowDialogAsync();
                }
                await _fillGrafs(true);
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex) { new UiException($"Неожиданная ошибка при формировании графика соревнований (код={_compParam?.Id ?? 0}).", ex).ShowMessage(this); }
            finally
            {
                _setEnabledMenu();
            }
        }

        private async Task _fillGrafs(bool refresh)
        {
            BindingList<BLL.CompGraf> src;
            if (refresh)
            {
                context.Dispose();
                context = new TkwContext();
                await context.CompGrafs.LoadAsync();
            }
            src = await Task.Run(async () =>
            {
                var tmp = context.CompGrafs.ToList();
                var lst = await Task.Run(() => tmp.Where(w => w.RedUser != null ? w.RedUser.Comp.Id == _compParam.Id
                    : (w.BlueUser != null ? w.BlueUser.Comp.Id == _compParam.Id : false)).ToList());
                return new BindingList<BLL.CompGraf>(lst);
            });
            bsGrafs.DataSource = src;
        }

        private void grdUsers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == ColumnVes.Index && e.RowIndex >= 0)
            {
                BLL.CompUser data = grdUsers.Rows[e.RowIndex].DataBoundItem as BLL.CompUser;
                grdUsers.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = !data.AllowVesChange(context);
            }
        }

        private async void tsbDeleteUser_Click(object sender, EventArgs e)
        {
            try
            {
                tsbDeleteUser.Enabled = false;
                if (bsUsers.Current == null)
                    return;
                if (grdUsers.SelectedRows.Count == 0 && grdUsers.SelectedCells.Count == 0)
                {
                    BLL.CompUser record = (BLL.CompUser)bsUsers.Current;
                    if (MessageBox.Show(this, $"Удалить участника {record.FIO} с соревнования"
                        , "Удаление участника", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;
                    if (!(await Task.Run(() => record.AllowUserDelete(context))))
                    {
                        MessageBox.Show(this, $"{record.FIO} имеет проведенный(е) бой(и){Environment.NewLine}Удаление невозможно.", "Удаление участника", MessageBoxButtons.OK);
                        return;
                    }
                    record.Actual = false;
                }
                else
                {
                    DataGridViewRow[] rows = null;
                    if (grdUsers.SelectedCells.Count > 0)
                    {
                        //rows = new DataGridViewRow[grdUsers.SelectedCells.Count];
                        //for (int i = 0; i < grdUsers.SelectedCells.Count; i++)
                        //    rows[i] = grdUsers.SelectedCells[i].OwningRow;
                        DataGridViewCell[] array = new DataGridViewCell[grdUsers.SelectedCells.Count];
                        grdUsers.SelectedCells.CopyTo(array, 0);
                        rows = array.Select(s => s.OwningRow).ToArray();
                    }
                    else
                    {
                        grdUsers.SelectedRows.CopyTo(rows, 0);
                    }
                    if (MessageBox.Show(this, $"Удалить выбранных участников соревнования ({rows.Count()})"
                        , "Удаление участников", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        return;
                    StringBuilder sb = new StringBuilder();
                    foreach (DataGridViewRow row in rows)
                    {
                        BLL.CompUser record = (BLL.CompUser)row.DataBoundItem;
                        if (!(await Task.Run(() => record.AllowUserDelete(context))))
                        {
                            sb.Append($"{record.FIO}, ");
                            continue; ;
                        }
                        record.Actual = false;
                    }
                    if (sb.Length > 0)
                    {
                        string msg = sb.ToString().Substring(0, sb.Length - 2);
                        if (MessageBox.Show(this, $"Нельзя удалить {msg}, потому как они уже участвовали в соревновании.{Environment.NewLine}Продолжить удаление?"
                                        , "Удаление участников", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                        {
                            context = new TkwContext();
                            await _fillGrdUsers();
                            return;
                        }
                    }
                }
                context.SaveChanges();
                await _fillGrdUsers();
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex) { new UiException("Неизвестная ошибка при добавлении участника соревнования", ex).ShowMessage(this); }
            finally
            {
                _setEnabledMenu();
            }
        }

        private async void grdUsers_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == ColumnCurPoyasName.Index)
            {
                tsUserClub.Enabled = false;
                grdUsers.CellMouseDoubleClick -= grdUsers_CellMouseDoubleClick;
                grdUsers.Enabled = false;
                await _changePoyas(e);
                grdUsers.Enabled = true;
                grdGrafs.Focus();
                grdUsers.CellMouseDoubleClick += grdUsers_CellMouseDoubleClick;
                _setEnabledMenu();
            }
        }
        private async Task _changePoyas(DataGridViewCellMouseEventArgs ev)
        {
            try
            {
                BLL.CompUser data = (BLL.CompUser)bsUsers.Current;
                if (data == null || await Task.Run(() => !data.AllowPoyasChange(context)))
                    return;
                Point point;
                    if (ev.Button != MouseButtons.Left)
                        return;
                    point = this.PointToScreen(PointToClient(new Point(ev.X, ev.Y)));
                    Point pc = grdUsers.GetCellDisplayRectangle(grdUsers.CurrentCell.ColumnIndex, grdUsers.CurrentCell.RowIndex, true).Location;
                    point = grdUsers.PointToScreen(Point.Empty);
                    point.Offset(pc);

                BLL.Poyas poyas = data.CurPoyas;
                List<BLL.Poyas> src = null;
                await Task.Run(() =>
                {
                    int order = poyas?.Order ?? 0;
                    src = context.Poyases.Where(w => w.Order >= order).OrderBy(o => o.Order).ThenBy(t => t.Id).ToList();
                    if (poyas == null)
                        poyas = src[0];
                });

                BLL.Poyas newPoyas = null;
                using (UI.Sprav.f_SpravPoyas_change frm = new UI.Sprav.f_SpravPoyas_change(point, poyas, src))
                {
                    DialogResult dr = await frm.ShowDialogAsync(this);
                    if (dr == DialogResult.OK)
                        newPoyas = frm.Poyas;
                }
                if (newPoyas == null || poyas.Equals(newPoyas))
                {
                    _setEnabledMenu();
                    return;
                }
                await Task.Run(() =>
                {
                    BLL.UserPoyas userPoyas = new BLL.UserPoyas { UserId = data.UserClub.UserId, PoyasId = newPoyas.Id, Actual = true, Delivery = DateTime.Now };
                    data.UserClub.User.UserPoyases.Add(userPoyas);
                    data.UserPoyasId = userPoyas.Id;
                    context.SaveChanges();
                    context = new BLL.TkwContext();
                });
                await _fillGrdUsers();
                grdUsers.CurrentCell = grdUsers.CurrentCell.OwningRow.Cells[ColumnCurPoyasName.Index];
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex) { new UiException("Непредвиденная ошибка при изменении пояса участника соревнования", ex).ShowMessage(this); }
            finally
            {
            }
        }
    }
}
