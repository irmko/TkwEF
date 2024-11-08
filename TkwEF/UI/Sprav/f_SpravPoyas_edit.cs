using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace TkwEF.UI.Sprav
{
    public partial class f_SpravPoyas_edit : TkwEF.UI.f_BDUI
    {
        #region enum

        private enum Mode { Add, Edit }

        #endregion

        #region Fields

        private readonly Mode _mode;
        private readonly int? _idPoyas = null;
        /// <summary>
        /// Возвращаемое значение
        /// </summary>
        public int? RetVal { get; private set; }

        #endregion

        #region ctor

        public f_SpravPoyas_edit()
        {
            InitializeComponent();
            _mode = Mode.Add;
            btnOk.Text = "Создать";
            btnOk.ImageIndex = 0;
        }
        public f_SpravPoyas_edit(int poyas) : this()
        {
            _mode = Mode.Edit;
            _idPoyas = poyas;
            btnOk.Text = "Изменить";
            btnOk.ImageIndex = 1;
        }

        #endregion

        private void f_SpravPoyas_edit_Shown(object sender, EventArgs e)
        {
            int ord;
            using (BLL.TkwContext context = new BLL.TkwContext())
            {
                System.Data.Entity.DbSet<BLL.Poyas> lst = context.Poyases;
                if (lst.Count() == 0)
                    ord = 0;
                else
                    ord = lst.Max(m => m.Order);
            }
            ord++;
            txtNum.Value = ord;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.None;
            if (txtName.Text.Trim().Length == 0)
                return;
            btnOk.Enabled = false;
            DialogResult = DialogResult.Abort;
            try
            {
                string name = txtName.Text.Trim();
                int ord = (int)txtNum.Value;
                if (_mode == Mode.Add)
                {
                        using (BLL.TkwContext context = new BLL.TkwContext())
                        {
                            BLL.Poyas poyas = new BLL.Poyas { Name = name, Order = ord };
                            context.Poyases.Add(poyas);
                            context.SaveChanges();
                            RetVal = poyas.Id;
                        }
                }
                else
                {
                    BLL.Poyas poyas;
                    using (BLL.TkwContext context = new BLL.TkwContext())
                    {
                        poyas = context.Poyases.Where(w => w.Id == _idPoyas).FirstOrDefault();
                        if (poyas == null)
                            throw new UiException("При изменении пояс не удалось определить в БД", new Exception(string.Format("Id_poyas = {0}", _idPoyas)), sendMail: true);
                        bool dirty = false;
                        if (poyas.Name != name)
                        {
                            poyas.Name = name;
                            dirty = true;
                        }
                        if (poyas.Order != ord)
                        {
                            List<BLL.Poyas> lst;
                            if (poyas.Order < ord)//Увеличение
                            {
                                lst = context.Poyases.Where(w => w.Order > poyas.Order && w.Order <= ord).ToList();
                                lst.Select(s => s.Order--);
                            }
                            else
                            {
                                lst = context.Poyases.Where(w => w.Order >= ord && w.Order < poyas.Order).ToList();
                                lst.Select(s => s.Order++);
                            }
                            dirty = true;
                        }
                        if (dirty)
                            context.SaveChanges();
                    }
                }
                DialogResult = DialogResult.OK;
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException(string.Format("Ошибка при {0} пояса", (_mode == Mode.Add ? "создании" : "изменении")), ex, sendMail: true);
            }
        }
    }
}
