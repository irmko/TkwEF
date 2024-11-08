using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TkwEF.BLL;
using TkwEF.Core.Extentions;

namespace TkwEF.UI.Sprav
{
    public partial class f_SpravClub : f_BUI
    {
        #region ctor

        public f_SpravClub()
        {
            InitializeComponent();
        }

        #endregion

        #region Handled

        private void f_SpravPoyas_Shown(object sender, EventArgs e)
        {
            _fillGrd(null);
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            tsbAdd.Enabled = false;
            try
            {
                int? id = null;
                using (f_SpravClub_edit frm = new Sprav.f_SpravClub_edit())
                {
                    DialogResult dr = frm.ShowDialog(this);
                    if (dr == DialogResult.OK)
                        id = frm.RetVal;
                    else if (dr == DialogResult.Abort)
                        id = 0;
                }
                OnAdded(new AddedEventArgs( id ?? 0));
                _fillGrd(id);
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException("Неопределенная ошибка при создании пояса", ex).ShowMessage(this);
            }
            finally { tsbAdd.Enabled = true; }
        }

        #endregion

        #region Private function

        private Task _fillGrdAsync(int? id)
        {
            return Task.Factory.StartNew(() =>
            {
                _fillGrd(id);
            });
        }

        private void _fillGrd(int? id)
        {
            List<BLL.Club> src = context.Clubs.OrderBy(o => o.Name).ToList();
                //edsRows.CreateView(context.Clubs.Where(w => w.Telefon.Length > 0));
            this.InvokeIfNeededCore(() =>
            {
                bsRows.DataSource = src;
            });
            //edsRows.Refresh();
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
    }
}
