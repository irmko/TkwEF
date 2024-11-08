using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Collections.ObjectModel;
using System.Data.Entity;
using TkwEF.Helper;

namespace TkwEF.UI.Sprav
{
    public partial class f_SpravVesKat : TkwEF.UI.f_BUI
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

        public f_SpravVesKat()
        {
            InitializeComponent();
        }
        public f_SpravVesKat(IEnumerable<BLL.VesKat> src, string cap = "", Mode mode = Mode.Show) : this()
        {
            _mode = mode;
            _capParam = cap;
            _fillSrcParam(src);
            _changeMode(_mode);
        }

        #endregion

        #region Handled

        private void f_SpravVesKat_Shown(object sender, EventArgs e)
        {
            _fillGrid(null);
        }

        private async void tsbAdd_Click(object sender, EventArgs e)
        {
            tsbAdd.Enabled = false;
            try
            {
                int? id = null;
                //using (f_SpravVesKat_edit frm = new Sprav.f_SpravVesKat_edit())
                //{
                //    DialogResult dr = frm.ShowDialog(this);
                //    if (dr == DialogResult.OK)
                //        id = frm.RetVal;
                //    else if (dr == DialogResult.Abort)
                //        id = 0;
                //}
                f_SpravVesKat_edit frm = new Sprav.f_SpravVesKat_edit();
                DialogResult dr = await frm.ShowDialogAsync();
                if (dr == DialogResult.OK)
                    id = frm.RetVal;
                else if (dr == DialogResult.Abort)
                    id = 0;
                if (id.HasValue)
                {
                    OnAdded(new AddedEventArgs(id.Value));
                    _fillGrid(id);
                }
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException("Неопределенная ошибка при создании пояса", ex).ShowMessage(this);
            }
            finally { tsbAdd.Enabled = true; }
        }

        private void tsbSelect_Click(object sender, EventArgs e)
        {
            if (_mode.HasFlag(Mode.SelectMore))
            {
                if (grdVesKat.SelectedRows.Count == 0)
                {
                    RetVals = new[] { ((BLL.VesKat)grdVesKat.CurrentCell.OwningRow.DataBoundItem).Id };
                }
                else
                {
                    int cnt = grdVesKat.SelectedRows.Count;
                    RetVals = new int[cnt];
                    for (int i = 0; i < cnt; i++)
                    {
                        RetVals[i] = ((BLL.VesKat)grdVesKat.SelectedRows[i].DataBoundItem).Id;
                    }
                }
                DialogResult = DialogResult.OK;
            }
            else if (_mode.HasFlag(Mode.SelectOne))
            {
                if (grdVesKat.SelectedRows.Count > 0)
                    RetVal = ((BLL.VesKat)grdVesKat.SelectedRows[0].DataBoundItem).Id;
                else
                    RetVal = ((BLL.VesKat)grdVesKat.CurrentCell.OwningRow.DataBoundItem).Id;
                DialogResult = DialogResult.OK;
            }
        }

        #endregion

        #region Private function

        private void _fillGrid(int? id)
        {
            if (_srcParam == null)
            {
                ObservableCollection<BLL.VesKat> src;
                context.VesKates.Load();
                src = context.VesKates.Local;
                src = new ObservableCollection<BLL.VesKat>(src.OrderBy(p => p.Id));
                bsVesKat.DataSource = src;
            }
            else
            {
                bsVesKat.DataSource = _srcParam;
            }
        }
        /// <summary>
        /// Заполнение поля ресурса
        /// </summary>
        /// <param name="src"></param>
        private void _fillSrcParam(IEnumerable<BLL.VesKat> src)
        {
            if (!(src is IBindingList))
            {
                IList<BLL.VesKat> lst;
                if (!(src is IList<BLL.VesKat>))
                    lst = src.ToList();
                else
                    lst = (IList<BLL.VesKat>)src;
                _srcParam = new BindingList<BLL.VesKat>(lst);
            }
            else
                _srcParam = (IBindingList)src;
        }

        #endregion

        #region Public function

        /// <summary>
        /// Заполнение ресурса
        /// </summary>
        /// <param name="src"></param>
        public void FillGrd(IEnumerable<BLL.VesKat> src)
        {
            _fillSrcParam(src);
            _fillGrid(null);
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
                    this.Text = "Выбор весовых категорий";
                }
                else if ((_mode & Mode.SelectOne) == Mode.SelectOne)
                    this.Text = "Выбор весовой категории";
                else
                    this.Text = "Весовые категоии";
                tsbSelect.Visible = _mode.HasFlag(Mode.SelectMore) || _mode.HasFlag(Mode.SelectOne);
                tsbAdd.Visible = _mode.HasFlag(Mode.Add);
                grdVesKat.MultiSelect = !_mode.HasFlag(Mode.SelectOne);
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
