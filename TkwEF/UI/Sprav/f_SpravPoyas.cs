using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TkwEF.Core.Extentions;

namespace TkwEF.UI.Sprav
{
    public partial class f_SpravPoyas : f_BUI
    {
        #region ctor

        public f_SpravPoyas()
        {
            InitializeComponent();
        }

        #endregion

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            tsbAdd.Enabled = false;
            try
            {
                int? id = null;
                using (f_SpravPoyas_edit frm = new Sprav.f_SpravPoyas_edit())
                {
                    DialogResult dr = frm.ShowDialog(this);
                    if (dr == DialogResult.OK)
                        id = frm.RetVal;
                    else if (dr == DialogResult.Abort)
                        id = 0;
                }
                if (id.HasValue)
                    _fillGrid(id);
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException("Неопределенная ошибка при создании пояса", ex).ShowMessage(this);
            }
            finally { tsbAdd.Enabled = true; }
        }

        private void _fillGrid(int? id)
        {
            //ObservableCollection<BLL.Poyas> src;
            ObservableCollection<BLL.Poyas> src;
            using (BLL.TkwContext context = new BLL.TkwContext())
            {
                src = new ObservableCollection<BLL.Poyas>(context.Poyases.OrderBy(o => o.Order).ToList());
            }
            this.InvokeIfNeededCore(() => { bsRows.DataSource = null; bsRows.DataSource = src; bsRows.RemoveFilter(); });
        }

        private void f_SpravPoyas_Shown(object sender, EventArgs e)
        {
            _fillGrid(null);
        }
    }
}
