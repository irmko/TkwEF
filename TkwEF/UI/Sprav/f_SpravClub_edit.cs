using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using TkwEF.Core.Extentions;
using System.Linq;
using System.Threading.Tasks;

namespace TkwEF.UI.Sprav
{
    public partial class f_SpravClub_edit : TkwEF.UI.f_BDUI
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

        public f_SpravClub_edit()
        {
            InitializeComponent();
            _mode = Mode.Add;
            btnOk.Text = "Создать";
            btnOk.ImageIndex = 0;
            txtTelefon.MaskInputRejected += txtTelefon_MaskInputRejected;
        }

        public f_SpravClub_edit(int id) : this()
        {
            _mode = Mode.Edit;
            _id = id;
            btnOk.Text = "Изменить";
            btnOk.ImageIndex = 1;
        }

        #endregion

        #region Handled

        private void f_SpravClub_edit_Load(object sender, EventArgs e)
        {
            if (_mode == Mode.Add)
                this.Text = "Добавление клуба";
            else if (_mode == Mode.Edit)
                this.Text = "Редактирование клуба";
            else
                this.Text = "Неизвестный режим работы";
            txtName.Focus();
        }

        private async void btnOk_Click(object sender, EventArgs e)
        {
            this.ValidateChildren();
            if (this.HasError(errProv))
            {
                DialogResult = DialogResult.None;
                return;
            }
            btnOk.Enabled = false;
            try
            {
                string name = txtName.Text.Trim(), address = txtAddress.Text.Trim(), email = txtEmail.Text.Trim(), telefon = txtTelefon.Text.Trim();
                if (_mode == Mode.Add)
                {
                    using (BLL.TkwContext tkwContext = new BLL.TkwContext())
                    {
                        BLL.Club club = new BLL.Club { Name = name, Address = address, Email = email, Telefon = telefon };
                        var tmp = await Task.Run(() => tkwContext.Clubs.Where(w => w.Name.Equals(club.Name)).FirstOrDefault());
                        if(tmp!=null)
                        {
                            throw new UiException(string.Format("Клуб {0} уже существует в БД", club.Name));
                        }
                        await Task.Run(() => tkwContext.Clubs.Add(club));
                        await Task.Run(() => tkwContext.SaveChanges());
                        RetVal = club.Id;
                    }
                }
                //else
                //{
                //    BLL.Poyas poyas;
                //    using (BLL.TkwContext context = new BLL.TkwContext())
                //    {
                //        poyas = await Task.Factory.StartNew(() => context.Poyases.Where(w => w.Id == _id).FirstOrDefault());
                //        if (poyas == null)
                //            throw new UiException("При изменении пояс не удалось определить в БД", new Exception(string.Format("Id_poyas = {0}", _id)), sendMail: true);
                //        bool dirty = false;
                //        if (poyas.Name != name)
                //        {
                //            poyas.Name = name;
                //            dirty = true;
                //        }
                //        if (poyas.Order != ord)
                //        {
                //            List<BLL.Poyas> lst;
                //            if (poyas.Order < ord)//Увеличение
                //            {
                //                lst = await Task.Factory.StartNew(() => context.Poyases.Where(w => w.Order > poyas.Order && w.Order <= ord).ToList());
                //                lst.Select(s => s.Order--);
                //            }
                //            else
                //            {
                //                lst = await Task.Factory.StartNew(() => context.Poyases.Where(w => w.Order >= ord && w.Order < poyas.Order).ToList());
                //                lst.Select(s => s.Order++);
                //            }
                //            dirty = true;
                //        }
                //        if (dirty)
                //            await context.SaveChangesAsync();
                //    }
                //}
                DialogResult = DialogResult.OK;
            }
            catch (UiException ex) { ex.ShowMessage(this); DialogResult = DialogResult.Abort; }
            catch (Exception ex)
            {
                new UiException(string.Format("Ошибка при {0} пояса", (_mode == Mode.Add ? "создании" : "изменении")), ex, sendMail: true);
                DialogResult = DialogResult.Abort;
            }
        }

        private void txtTelefon_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            toolTip1.ToolTipTitle = "Ошибка ввода";
            toolTip1.Show("Допустим ввод только цифр", txtTelefon, txtTelefon.Location, 5000);
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            if (txtEmail.Text.Length == 0)
                return;
            string patern = @"(^\S+@\S+\.\S{2,4}$)|.{0}";
            patern = Core.BLL.BIZ_Base.EmailPattern;
            Regex regex = new Regex(patern);
            bool res = regex.IsMatch(txtEmail.Text);
            errProv.SetError(txtEmail, res ? "" : "Неверно указан адрес электронной почты");
        }

        private void txt_Validated(object sender, EventArgs e)
        {
            errProv.Control_Validating(sender);
        }

        private void txtTelefon_Validated(object sender, EventArgs e)
        {
            if (txtTelefon.MaskCompleted || txtTelefon.Text.Length == 0)
                errProv.SetError(txtTelefon, "");
            else
                errProv.SetError(txtTelefon, "Значение заполнено не полностью");
        }

        #endregion

        private void txtName_Enter(object sender, EventArgs e)
        {
            errProv.SetError(txtName, "");
        }
    }
}
