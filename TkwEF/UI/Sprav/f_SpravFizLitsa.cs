using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using TkwEF.Helper;
using System.Threading;

namespace TkwEF.UI.Sprav
{
    public partial class f_SpravFizLitsa : TkwEF.UI.f_BUI
    {
        #region Enum

        [Flags]
        public enum Mode { Show = 0x00, SelectOne = 0x01, SelectMore = 0x02, Add = 0x04, Edit = 0x08, Delete = 0x16 }

        #endregion

        #region Fields

        private Mode _mode { get; set; }
        private readonly string _capParam = string.Empty;
        /// <summary>
        /// Если не пустой, то заполняется из него
        /// </summary>
        private IBindingList _srcParam = null;
        public int? RetVal { get; set; }
        /// <summary>
        /// При множественном выборе
        /// </summary>
        public int[] RetVals { get; set; }

        #endregion

        #region ctor

        public f_SpravFizLitsa()
            : base()
        {
            InitializeComponent();
            _mode = Mode.Show;
        }

        public f_SpravFizLitsa(Mode mode, string cap = "", IBindingList src = null) : this()
        {
            _mode = mode;
            _srcParam = src;
            _capParam = cap;
            _changeMode(_mode);
        }

        #endregion

        #region Handled

        private void f_SpravFizLitsa_Load(object sender, EventArgs e)
        {
            _setControls();
        }
        private void f_SpravFizLitsa_Shown(object sender, EventArgs e)
        {
            try
            {
                _fillGrid(null);
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException("Непредвиденная ошибка при загрузке формы справочника физических лиц", ex).ShowMessage(this);
            }
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            tsbAdd.Enabled = false;
            try
            {
                int? id = null;
                using (f_SpravFizLitsa_edit frm = new Sprav.f_SpravFizLitsa_edit())
                {
                    DialogResult dr = frm.ShowDialog(this);
                    if (dr == DialogResult.OK)
                        id = frm.RetVal;
                    else if (dr == DialogResult.Abort)
                        id = 0;
                }
                if (id.HasValue)
                {
                    OnAdded(new AddedEventArgs(id.Value));
                    _fillGrid(id);
                }
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException("Неопределенная ошибка при создании физлица", ex).ShowMessage(this);
            }
            finally { _setControls(); }
        }
        private async void tsbEdit_Click(object sender, EventArgs e)
        {
            tsbEdit.Enabled = false;
            try
            {
                BLL.User user = (BLL.User)bsUsers.Current;
                int? id = null;
                //using (f_SpravFizLitsa_edit frm = new Sprav.f_SpravFizLitsa_edit(user.Id))
                //{
                //    DialogResult dr = frm.ShowDialog(this);
                //    if (dr == DialogResult.OK)
                //        id = frm.RetVal;
                //    else if (dr == DialogResult.Abort)
                //        id = 0;
                //}
                ////============================
                //f_SpravFizLitsa_edit frm = new Sprav.f_SpravFizLitsa_edit(user.Id);
                //DialogResult dr = DialogResult.None;
                //frm.FormClosed += (s, ev) =>
                //{
                //    if (dr == DialogResult.OK)
                //        id = frm.RetVal;
                //    else if (dr == DialogResult.Abort)
                //        id = 0;
                //};
                //frm.BeginInvoke(new Action(() => dr = frm.ShowDialog(this)));
                ////============================
                ///

                f_SpravFizLitsa_edit frm = new f_SpravFizLitsa_edit(user.Id);

                CancellationToken token = new CancellationToken();
                var ctsDialog = CancellationTokenSource.CreateLinkedTokenSource(token);
                var t = await frm.ShowDialogAsync();

                if (id.HasValue)
                {
                    OnAdded(new AddedEventArgs(id.Value));
                    _fillGrid(id);
                }
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException("Неопределенная ошибка при редактировании физлица", ex).ShowMessage(this);
            }
            finally { _setControls(); }

        }
        private void tsbSelect_Click(object sender, EventArgs e)
        {
            if (_mode.HasFlag(Mode.SelectMore))
            {
                if (grdUsers.SelectedRows.Count == 0)
                {
                    RetVals = new[] { ((BLL.User)grdUsers.CurrentCell.OwningRow.DataBoundItem).Id };
                }
                else
                {
                    int cnt = grdUsers.SelectedRows.Count;
                    RetVals = new int[cnt];
                    for (int i = 0; i < cnt; i++)
                    {
                        RetVals[i] = ((BLL.User)grdUsers.SelectedRows[i].DataBoundItem).Id;
                    }
                }
                DialogResult = DialogResult.OK;
            }
            else if(_mode.HasFlag(Mode.SelectOne))
            {
                if (grdUsers.SelectedRows.Count > 0)
                {
                    RetVal = ((BLL.User)grdUsers.SelectedRows[0].DataBoundItem).Id;
                }
                else
                    RetVal = ((BLL.User)grdUsers.CurrentCell.OwningRow.DataBoundItem).Id;
                DialogResult = DialogResult.OK;
            }
        }

        #endregion

        #region Private functioin

        private void _fillGrid(int? id)
        {
            if (_srcParam == null)
            {
                bsUsers.DataSource = new BindingList<BLL.User>(context.Users.ToList().OrderBy(o => o.FIO).ToList());
            }
            else
            {
                bsUsers.DataSource = _srcParam;
            }
        }
        private void _setControls()
        {
            tsbAdd.Enabled = _mode.HasFlag(Mode.Add);
            tsbEdit.Enabled = _mode.HasFlag(Mode.Edit);
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
                    this.Text = "Выбор физических лиц";
                }
                else if ((_mode & Mode.SelectOne) == Mode.SelectOne)
                    this.Text = "Выбор физического лица";
                else
                    this.Text = "Физические лица";
                tsbSelect.Visible = _mode.HasFlag(Mode.SelectMore) || _mode.HasFlag(Mode.SelectOne);
                tsbAdd.Visible = _mode.HasFlag(Mode.Add);
                tsbEdit.Visible = _mode.HasFlag(Mode.Edit);
                grdUsers.MultiSelect = !_mode.HasFlag(Mode.SelectOne);
            }
        }

        #endregion

        #region Public function

        public void FillGrd(IBindingList src)
        {
            _srcParam = src;
            _fillGrid(null);
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

        public event EventHandler Edited;
        protected void OnEdited(object sender, EventArgs e)
        {
            Edited?.Invoke(sender, e);
        }

        public event EventHandler Deleted;
        protected void OnDeleted(object sender, EventArgs e)
        {
            Deleted.Invoke(sender, e);
        }

        #endregion
    }
}
