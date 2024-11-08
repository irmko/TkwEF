using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using TkwEF.Helper;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;
using TkwEF.Core.Extentions;
using System.Drawing;

namespace TkwEF.UI.Sprav
{
    public partial class f_SpravUserClub : TkwEF.UI.f_BUI
    {
        #region enum

        [Flags]
        public enum Mode { Show = 1, SelectOne = 2, SelectMore = 4, Add = 8, Edit = 16, Delete = 32 }

        #endregion

        #region Fields

        private Mode _mode { get; set; }
        private readonly string _capParam = string.Empty;
        private readonly int? _compParam = null;
        public int? RetVal { get; set; }
        /// <summary>
        /// При множественном выборе
        /// </summary>
        public int[] RetVals { get; set; }

        #endregion

        #region ctor

        public f_SpravUserClub()
        {
            InitializeComponent();
            _bindingByLoad();
            _mode = Mode.Show | Mode.Add | Mode.Edit | Mode.Delete;
        }
        public f_SpravUserClub(Mode mode, string cap = "") : this()
        {
            _mode = mode;
            _capParam = cap;
        }
        public f_SpravUserClub(int comp) : this()
        {
            _mode = Mode.Add | Mode.SelectMore;
            _compParam = comp;
        }

        #endregion

        #region Handled

        private async void f_SpravUserClub_Load(object sender, EventArgs e)
        {
            _setEnabledBtn(false);
            cbxClub.FillSrc();
            await _fillGrdAsync();
        }
        private void f_SpravUserClub_Shown(object sender, EventArgs e)
        {
            cbxClub.DroppedDown = true;
            _setEnabledBtn(true);
        }

        private void cbxClub_Enter(object sender, EventArgs e)
        {
            cbxClub.Enter -= cbxClub_Enter;
            cbxClub.SelectedValueChanged += cbxClub_SelectedValueChanged;
            cbxClub.Leave += cbxClub_Leave;
        }

        private void cbxClub_Leave(object sender, EventArgs e)
        {
            cbxClub.SelectedValueChanged -= cbxClub_SelectedValueChanged;
            cbxClub.Leave -= cbxClub_Leave;
            cbxClub.Enter += cbxClub_Enter;
        }

        private async void cbxClub_SelectedValueChanged(object sender, EventArgs e)
        {
            await _fillGrdAsync();
        }

        private async void btnAddClub_Click(object sender, EventArgs e)
        {
            try
            {
                btnAddClub.Enabled = false;
                int? id = null;
                using (f_SpravClub_edit frm = new f_SpravClub_edit())
                {
                    DialogResult dr = DialogResult.None;
                    dr = await frm.ShowDialogAsync(this);
                    if (dr == DialogResult.OK)
                        id = frm.RetVal;
                }
                if (id.HasValue)
                {
                    //await cbxClub.FillSrcAsync(); // выбор не той позиции ???
                    cbxClub.FillSrc();
                    cbxClub.SelectedValue = id.Value;
                    cbxClub_SelectedValueChanged(sender, e);
                }
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex) { new UiException("Непредвиденная ошибка при создании клуба", ex); }
            finally
            {
                _setEnabledBtn(true);
            }
        }

        private async void tsbAddUserClub_Click(object sender, EventArgs e)
        {
            try
            {
                tsUser.Enabled = false;
                int[] idVals = null;
                f_SpravFizLitsa.Mode mode = f_SpravFizLitsa.Mode.Add | f_SpravFizLitsa.Mode.Delete | f_SpravFizLitsa.Mode.Edit | f_SpravFizLitsa.Mode.SelectMore;
                Sprav.f_SpravFizLitsa frm = new f_SpravFizLitsa(mode, cap: "Добавление в клуб", src: _fillSrcSpravFizL());
                frm.FormClosing += (s, ev) => { idVals = frm.RetVals; };
                frm.FormClosed += (s, ev) => { frm.Dispose(); frm = null; };
                frm.Added += (s, ev) => { frm.FillGrd(_fillSrcSpravFizL()); };
                //frm.FillGrd(_fillSrcSpravFizL());
                DialogResult dr = await frm.ShowDialogAsync(this);
                if (dr != DialogResult.OK)
                    idVals = null;

                if ((idVals?.Count() ?? 0) > 0)
                {
                    using (BLL.TkwContext tkwContext = new BLL.TkwContext())
                    {
                        int idClub = cbxClub.ID;
                        BLL.Club club = await Task.Run(() => tkwContext.Clubs.Where(w => w.Id == idClub).First());
                        foreach (var id in idVals)
                        {
                            BLL.User user = await Task.Run(() => tkwContext.Users.Where(w => w.Id == id).FirstOrDefault());
                            //BLL.UserClub uc = new BLL.UserClub { Club = club, User = user, Begin = DateTime.Now.Date };
                            BLL.UserClub uc = tkwContext.UserClubs.Create();
                            uc.Club = club;
                            uc.User = user;
                            uc.Begin = DateTime.Now.Date;
                            await Task.Run(() => tkwContext.UserClubs.Add(uc));
                        }
                        await Task.Run(() => tkwContext.SaveChanges()); 
                    }
                    OnAdded(new AddedEventArgs((idVals?.Count() ?? 0) == 0 ? 0 : idVals[0]));
                }
                await _fillGrdAsync();
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex) { new UiException("Непредвиденная ошибка при добвлении физического лица в клуб", ex); }
        }

        private void tsbSelect_Click(object sender, EventArgs e)
        {
            if (_mode.HasFlag(Mode.SelectMore))
            {
                if (grdUsers.SelectedRows.Count == 0)
                {
                    RetVals = new[] { ((BLL.UserClub)grdUsers.CurrentCell.OwningRow.DataBoundItem).Id };
                }
                else
                {
                    int cnt = grdUsers.SelectedRows.Count;
                    RetVals = new int[cnt];
                    for (int i = 0; i < cnt; i++)
                    {
                        RetVals[i] = ((BLL.UserClub)grdUsers.SelectedRows[i].DataBoundItem).Id;
                    }
                }
                DialogResult = DialogResult.OK;
            }
            else if (_mode.HasFlag(Mode.SelectOne))
            {
                if (grdUsers.SelectedRows.Count > 0)
                {
                    RetVal = ((BLL.UserClub)grdUsers.SelectedRows[0].DataBoundItem).Id;
                }
                else
                    RetVal = ((BLL.UserClub)grdUsers.CurrentCell.OwningRow.DataBoundItem).Id;
                DialogResult = DialogResult.OK;
            }
        }

        #endregion

        #region Private function

        private async Task _fillGrdAsync()
        {
            int idClub = cbxClub.ID;
            IList<BLL.UserClub> lst;
            context.UserClubs.Load();
            if (_compParam.HasValue)
            {
                //q = context.UserClubs.Where(w => w.Club.Id == idClub).ToList()
                lst = await Task.Run(() => context.UserClubs
                    .Where(w => w.Club.Id == idClub)
                    .Except(context.CompUsers.Where(w => w.Comp.Id == _compParam.Value && w.Actual).Select(s => s.UserClub))
                    .OrderBy(o => o.User.Fam).ThenBy(t => t.User.Name).ThenBy(t => t.User.Otch).ThenBy(t => t.Id)
                    .AsEnumerable().OrderBy(o => o.FIO).ToList());
            }
            else
            {
                lst = await Task.Run(() => context.UserClubs.Where(w => w.Club.Id == idClub).AsEnumerable().OrderBy(o => o.FIO).ToList());
                //BLL.UserClub uc = lst.Count > 0 ? lst[0] : null;
                //Type entityType;
                //if (uc != null)
                //{
                //    entityType = uc.GetType().BaseType;
                //    entityType = ObjectContext.GetObjectType(uc.GetType());
                //    IEnumerable<Type> types = ObjectContext.GetKnownProxyTypes();
                //}
            }
            IBindingList src = new BindingList<BLL.UserClub>(lst);
            bsUsers.DataSource = src;
            _setEnabledBtn(true);
        }

        private void _bindingByLoad()
        {
            Binding b = new Binding("Enabled", cbxClub, "SelectedValue");
            b.ControlUpdateMode = ControlUpdateMode.OnPropertyChanged;
            b.DataSourceUpdateMode = DataSourceUpdateMode.Never;
            b.Format += (s, e) =>
            {
                int val = (int)e.Value;
                e.Value = val > 0;
            };
            tsUser.DataBindings.Add(b);
        }

        private IBindingList _fillSrcSpravFizL()
        {
            //var q = from u in context.Users
            //    from uc in context.UserClubs
            //        where !uc.End.HasValue
            //        select uc.Member;
            //var quc = context.UserClubs.Where(w => !w.End.HasValue).Select(s => s.Member);
            var src = context.Users.Except(context.UserClubs.Select(s => s.User)).ToList().OrderBy(o => o.FIO).ToList();
            IBindingList res = new BindingList<BLL.User>(src);
            return res;
            //return context.Users.Except(quc).ToList();
        }

        private void _changeMode(Mode mode)
        {
            if (_capParam != string.Empty && this.Text == string.Empty)
            {
                this.Text = _capParam.Trim();
            }
            else
            {
                if ((_mode & Mode.SelectMore) == Mode.SelectMore)
                {
                    this.Text = "Выбор членов клуба";
                }
                else if ((_mode & Mode.SelectOne) == Mode.SelectOne)
                    this.Text = "Выбор члена клуба";
                else
                    this.Text = "Члены клуба";
                tsbSelect.Visible = _mode.HasFlag(Mode.SelectMore) || _mode.HasFlag(Mode.SelectOne);
                tsbAddUserClub.Visible = _mode.HasFlag(Mode.Add);
                tsbEditUserClub.Visible = _mode.HasFlag(Mode.Edit);
                grdUsers.MultiSelect = !_mode.HasFlag(Mode.SelectOne);
            }
        }
        private void _setEnabledBtn(bool val = true)
        {
            BLL.UserClub data = (BLL.UserClub)bsUsers.Current;
            if (val)
            {
                btnAddClub.Enabled = tsbAddUserClub.Enabled = _mode.HasFlag(Mode.Add);
                tsbEditUserClub.Enabled = _mode.HasFlag(Mode.Edit) && data != null;
                tsbSelect.Enabled = (_mode.HasFlag(Mode.SelectOne) || _mode.HasFlag(Mode.SelectMore)) && data != null;
                tsbChangePoyas.Enabled = data != null;
            }
            else
            {
                btnAddClub.Enabled = tsbAddUserClub.Enabled = tsbEditUserClub.Enabled = tsbSelect.Enabled = tsbChangePoyas.Enabled = val;
            }
        }

        #endregion

        #region Events

        public delegate void AddedHandler(object sender, AddedEventArgs e);
        public event AddedHandler Added;
        public class AddedEventArgs : EventArgs
        {
            public AddedEventArgs(int id)
            {
                _id = id;
            }

            int _id { get; set; }

        }
        protected void OnAdded(AddedEventArgs e)
        {
            if (Added != null)
                Added(this, e);
        }

        #endregion

        private void tsbEditUserClub_Click(object sender, EventArgs e)
        {

        }

        private async void tsbChangePoyas_Click(object sender, EventArgs e)
        {
            await _changePoyas(sender, e);
        }

        private async void grdUsers_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == ColumnCurPoyasName.Index)
                await _changePoyas(sender, e);
        }

        private async Task _changePoyas(object sender, EventArgs e)
        {
            try
            {
                Point point;
                if(e is DataGridViewCellMouseEventArgs)
                {
                    DataGridViewCellMouseEventArgs ev = (DataGridViewCellMouseEventArgs)e;
                    if (ev.Button != MouseButtons.Left)
                        return;
                    point = this.PointToScreen(PointToClient(new Point(ev.X, ev.Y)));
                    Point pc = grdUsers.GetCellDisplayRectangle(grdUsers.CurrentCell.ColumnIndex, grdUsers.CurrentCell.RowIndex, true).Location;
                    point = grdUsers.PointToScreen(Point.Empty);
                    point.Offset(pc);
                    //point = this.PointToScreen(new Point(pg.X + pc.X, pg.Y + pc.Y));
                    //point = this.PointToScreen(new Point(pg.X + pc.X, pg.Y + pc.Y));
                }
                else
                {
                    point = this.GetPosition(tsUser);
                    point = tsUser.PointToScreen(Point.Empty);
                    point = this.PointToScreen(tsUser.GetPointRelativeToForm());
                    point.Offset(tsbChangePoyas.Bounds.Location);
                }
                grdUsers.CellMouseDoubleClick -= grdUsers_CellMouseDoubleClick;
                tsbChangePoyas.Enabled = false;
                BLL.UserClub data = (BLL.UserClub)bsUsers.Current;
                if (data == null)
                    return;
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
                using (UI.Sprav.f_SpravPoyas_change frm = new f_SpravPoyas_change(point, poyas, src))
                {
                    DialogResult dr = await frm.ShowDialogAsync(this);
                    if (dr == DialogResult.OK)
                        newPoyas = frm.Poyas;
                }
                if (newPoyas == null || poyas.Equals(newPoyas))
                {
                    _setEnabledBtn(true);
                    return;
                }
                await Task.Run(() =>
                {
                    BLL.UserPoyas userPoyas = new BLL.UserPoyas { UserId = data.UserId, PoyasId = newPoyas.Id, Actual = true, Delivery = DateTime.Now };
                    data.User.UserPoyases.Add(userPoyas);
                    context.SaveChanges();
                    context = new BLL.TkwContext();
                });
                await _fillGrdAsync();
                grdUsers.CurrentCell = grdUsers.CurrentCell.OwningRow.Cells[ColumnCurPoyasName.Index];
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex) { new UiException("Непредвиденная ошибка при добвлении физического лица в клуб", ex).ShowMessage(this); }
            finally
            {
                grdUsers.CellMouseDoubleClick += grdUsers_CellMouseDoubleClick;
            }
        }

        private void f_SpravUserClub_MouseMove(object sender, MouseEventArgs e)
        {
#if _DEBUG
            this.Text = $"X={e.X}, Y={e.Y}";
#endif
        }

        private void grdUsers_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
#if _DEBUG
            this.Text = $"X={e.X}, Y={e.Y}";
#endif
        }
    }
}
