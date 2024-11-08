using System;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using TkwEF.Core.Extentions;
using TkwEF.BLL;

namespace TkwEF.UI.Comp
{
    public partial class f_Comp_edit : TkwEF.UI.f_BDUI
    {
        #region enum

        private enum Mode { Add, Edit }

        #endregion

        #region Fields

        private readonly Mode _mode;
        private readonly int? _id = null;
        /// <summary>
        /// Возвращаемое значение
        /// </summary>
        public int? RetVal { get; private set; }

        #endregion

        #region ctor

        public f_Comp_edit()
        {
            InitializeComponent();
            _mode = Mode.Add;
            btnOk.Text = "Создать";
            btnOk.ImageIndex = 0;
            this.Text = "Создание соревнования";
        }
        public f_Comp_edit(int id) : this()
        {
            _mode = Mode.Edit;
            _id = id;
            btnOk.Text = "Изменить";
            btnOk.ImageIndex = 1;
            this.Text = "Редактирование соревнования";

        }

        #endregion

        #region Handled

        private void f_Comp_edit_Shown(object sender, EventArgs e)
        {
            if (_mode == Mode.Add)
            {
                dtpBegin.Value = dtpEnd.Value = DateTime.Now.Date;
            }
            else
            {
                using (BLL.TkwContext context = new BLL.TkwContext())
                {
                    var record = context.Competitions.Where(w => w.Id == _id).FirstOrDefault();
                    if (record == null)
                        throw new UiException("Ошибка в определении соревнования для редактирования");
                    dtpBegin.Value = record.DateBegin;
                    dtpEnd.Value = record.DateEnd ?? record.DateBegin;
                    txtName.Text = record.Name;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            this.ValidateChildren();
            if (this.HasError(errProv))
                return;
            btnOk.Enabled = false;
            DialogResult = DialogResult.Abort;
            try
            {
                string name = txtName.Text.Trim();
                DateTime begin = dtpBegin.Value.Date, end = dtpEnd.Value;

                if (_mode == Mode.Add)
                {
                    BLL.Competition comp = new BLL.Competition { Name = name, DateBegin = begin, DateEnd = end };
                    using (BLL.TkwContext context = new BLL.TkwContext())
                    {
                        context.Competitions.Add(comp);
                        context.SaveChanges();
                    }
                    RetVal = comp.Id;
                }
                else
                {
                    //BLL.Poyas poyas;
                    //using (BLL.TkwContext context = new BLL.TkwContext())
                    //{
                    //    poyas = await Task.Factory.StartNew(() => context.Poyases.Where(w => w.Id == _idPoyas).FirstOrDefault());
                    //    if (poyas == null)
                    //        throw new UiException("При изменении пояс не удалось определить в БД", new Exception(string.Format("Id_poyas = {0}", _idPoyas)), sendMail: true);
                    //    bool dirty = false;
                    //    if (poyas.Name != name)
                    //    {
                    //        poyas.Name = name;
                    //        dirty = true;
                    //    }
                    //    if (poyas.Order != ord)
                    //    {
                    //        List<BLL.Poyas> lst;
                    //        if (poyas.Order < ord)//Увеличение
                    //        {
                    //            lst = await Task.Factory.StartNew(() => context.Poyases.Where(w => w.Order > poyas.Order && w.Order <= ord).ToList());
                    //            lst.Select(s => s.Order--);
                    //        }
                    //        else
                    //        {
                    //            lst = await Task.Factory.StartNew(() => context.Poyases.Where(w => w.Order >= ord && w.Order < poyas.Order).ToList());
                    //            lst.Select(s => s.Order++);
                    //        }
                    //        dirty = true;
                    //    }
                    //    if (dirty)
                    //        await context.SaveChangesAsync();
                    //}
                }
                DialogResult = DialogResult.OK;
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException(string.Format("Ошибка при {0} пояса", (_mode == Mode.Add ? "создании" : "изменении")), ex, sendMail: true);
            }
        }

        private void ctrl_Enter(object sender, EventArgs e)
        {
            errProv.SetError((Control)sender, "");
        }

        private void ctrl_Validated(object sender, EventArgs e)
        {
            errProv.Control_Validating(sender);
        }

        private void dtpBegin_CloseUp(object sender, EventArgs e)
        {
            if (dtpBegin.Value.Date > dtpEnd.Value.Date)
                dtpEnd.Value = dtpBegin.Value;
        }

        private void dtpEnd_CloseUp(object sender, EventArgs e)
        {
            if (dtpEnd.Value.Date < dtpBegin.Value.Date)
                dtpBegin.Value = dtpEnd.Value;
        }

        #endregion
    }
}
