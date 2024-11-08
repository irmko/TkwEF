using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using TkwEF.BLL;
using TkwEF.Core.Extentions;

namespace TkwEF.UI.Sprav
{
    public partial class f_SpravFizLitsa_edit : TkwEF.UI.f_BDUI
    {
        private enum Mode { Add, Edit }

        #region Fields

        private readonly Mode _mode;
        BLL.TkwContext cntx;
        private readonly int? _idUser = null;
        private User _user = null;
        /// <summary>
        /// Возвращаемое значение
        /// </summary>
        public int? RetVal { get; private set; }

        #endregion

        #region ctor

        public f_SpravFizLitsa_edit()
        {
            InitializeComponent();
            _mode = Mode.Add;
            cntx = new TkwContext();
        }
        ~f_SpravFizLitsa_edit()
        {
            cntx.Dispose();
        }
        public f_SpravFizLitsa_edit(int id)
            : this()
        {
            _mode = Mode.Edit;
            _idUser = id;
        }

        #endregion

        #region Handled

        private void f_SpravFizLitsa_edit_Load(object sender, System.EventArgs e)
        {
            if (_mode == Mode.Add)
            {
                Text = "Добавление физического лица";
                btnOk.Text = "Добавить";
                cbxPoyas.FillSrc();
            }
        }

        private async void f_SpravFizLitsa_edit_Shown(object sender, EventArgs e)
        {
            if (_mode == Mode.Edit)
            {
                int maxPoyas = 0;
                List<Poyas> lst = null;
                try
                {
                    Task<BLL.User> tu = Task.Run(async () =>
                    {
                        var result = await Task.Run(() => cntx.Users.Where(w => w.Id == _idUser.Value).FirstOrDefault());
                        if (result.UserPoyases.Count > 0)
                            maxPoyas = result.UserPoyases.Max(m => m?.Poyas?.Order ?? 0);
                        cntx.UserPoyases.Load();
                        cntx.Poyases.Load();
                        lst = cntx.Poyases.Where(w => w.Order >= maxPoyas).ToList();
                        return result;
                    });

                    await tu;
                    _user = tu.Result;

                    cbxPoyas.FillSrc(lst);
                    Text = string.Format("Редактирование {0}", _user?.FIO);
                    txtFam.Text = _user.Fam;
                    txtName.Text = _user.Name;
                    txtOtch.Text = _user.Otch;
                    dtpDate.Value = _user.Birghtday.Value;
                    txtEmail.Text = _user.Email;
                    txtTel.Text = _user.Telefon;
                    btnOk.Text = "Изменить";
                }
                catch (UiException ex) { ex.ShowMessage(this); DialogResult = DialogResult.Abort; }
                catch (Exception ex)
                {
                    new UiException("Ошибка при загрузке формы редактирования физического лица"
                        , new Exception(string.Format("Id_user={0}", _idUser), ex)).ShowMessage(this);
                    DialogResult = DialogResult.Abort;
                }
                cbxPoyas.SelectedValue = await Task.Run(() => _user?.CurrentPoyas?.Id ?? 0);
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (_mode.IN(Mode.Add, Mode.Edit))
                {
                    this.DialogResult = DialogResult.Abort;
                    btnOk.Enabled = false;
                    txtFam.Validated += ctrl_Validate;
                    txtName.Validated += ctrl_Validate;
                    cbxPoyas.Validated += ctrl_Validate;
                    this.ValidateChildren();
                    if (this.HasError(errProv))
                    {
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                    using (BLL.TkwContext cntx = new TkwContext())
                    {
                        BLL.User user = null;
                        if (_mode == Mode.Add)
                        {
                            _fillUserByForm(ref user);
                            BLL.User ver = cntx.Users.Where(w => w.Fam == user.Fam && w.Name == user.Name
                                && (w.Otch != null ? w.Otch.Equals(user.Otch != null ? user.Otch : "") : true)
                                //&& (w.Birghtday.HasValue ? w.Birghtday.Value.Equals(user.Birghtday.HasValue ? user.Birghtday.Value : DateTime.MinValue) : true)
                                ).FirstOrDefault();
                            if (ver != null)
                            {
                                throw new UiException(string.Format("{0} уже существует в базе данных", user.FIO));
                            }
                            cntx.Users.Add(user);
                        }
                        else if (_mode == Mode.Edit)
                        {
                            user = cntx.Users.FirstOrDefault(f => f.Id == (_idUser ?? 0));
                            _fillUserByForm(ref user);
                            //BLL.User ver = cntx.Users.Where(w => w.Id == _idUser
                            //    && w.Fam == user.Fam && w.Name == user.Name
                            //    && (w.Otch != null ? w.Otch.Equals(user.Otch != null ? user.Otch : "") : true)
                            //    //&& (w.Birghtday.HasValue ? w.Birghtday.Value.Equals(user.Birghtday.HasValue ? user.Birghtday.Value : DateTime.MinValue) : true)
                            //    ).FirstOrDefault();
                        }
                        UserPoyas userPoyas = cntx.UserPoyases.FirstOrDefault(f => f.User.Id == _idUser && f.Poyas.Id == cbxPoyas.ID);
                        if (userPoyas == null)
                        {
                            BLL.Poyas poyas = cntx.Poyases.First(f => f.Id == cbxPoyas.ID);
                            userPoyas = new UserPoyas { User = user, Poyas = poyas, Delivery = DateTime.Now.Date };
                            cntx.UserPoyases.Add(userPoyas);
                            //cntx.Entry(userPoyas).State = EntityState.Added;
                            //PropertyInfo[] infos = userPoyas.GetType().GetProperties();
                            //foreach (var info in infos)
                            //{
                            //    bool isModif = cntx.Entry(userPoyas).Property(info.Name).IsModified;
                            //}
                            //cntx.Entry(userPoyas).Property(p => p.Poyas).IsModified = true;
                        }
                        RetVal = cntx.SaveChanges();
                    }
                }
                else
                    throw new UiException("Неизвестный режим при работе со справочником физических лиц");

                this.DialogResult = DialogResult.OK;
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex) { new UiException("Неожиданная ошибка при сохранении нового физического лица", ex).ShowMessage(this); }
            finally
            {
                txtFam.Validated -= ctrl_Validate;
                txtName.Validated -= ctrl_Validate;
                cbxPoyas.Validated -= ctrl_Validate;
                btnOk.Enabled = true;
            }
        }

        private void ctrl_Validate(object sender, System.EventArgs e)
        {
            errProv.Control_Validating(sender);
        }

        private void ctrl_Enter(object sender, EventArgs e)
        {
            errProv.SetError((Control)sender, "");
        }

        #endregion

        #region Private function

        private void _fillUserByForm(ref User user)
        {
            if (user == null)
                user = new User
                {
                    Fam = txtFam.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    Otch = txtOtch.Text.Trim(),
                    Birghtday = dtpDate.Value.Date,
                    Telefon = txtTel.Text.Trim(),
                    Email = txtEmail.Text.Trim()
                };
            else
            {
                if (user.Fam != txtFam.Text.Trim())
                    user.Fam = txtFam.Text.Trim();
                if (user.Name != txtName.Text.Trim())
                    user.Name = txtName.Text.Trim();
                if ((user.Otch ?? string.Empty) != txtOtch.Text.Trim())
                    user.Otch = txtOtch.Text.Trim();
                if ((user.Telefon ?? string.Empty) != txtTel.Text.Trim())
                    user.Telefon = txtTel.Text.Trim();
                if ((user.Email ?? string.Empty) != txtEmail.Text.Trim())
                    user.Email = txtEmail.Text.Trim();
                if ((user.Birghtday ?? DateTime.MinValue).Date != dtpDate.Value.Date)
                    user.Birghtday = dtpDate.Value.Date;
            }
        }

        #endregion

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete)
                e.Handled = true;
        }

        private void calendarButton1_DateSelected(object sender, DateRangeEventArgs e)
        {
            MessageBox.Show(e.Start.ToShortDateString());
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {

        }

        private void f_SpravFizLitsa_edit_FormClosing(object sender, FormClosingEventArgs e)
        {
            Debug.Assert(!this.IsDisposed, "При начале закрытия окна, оно уже уничтожено)");
        }
    }
}
