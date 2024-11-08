using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TkwEF.Core.Extentions;

namespace TkwEF.UI.Graf
{
    public partial class f_Graf_create_one : UI.f_BDUI
    {
        #region Fields

        private readonly int _compParam;
        private BLL.TkwContext context = new BLL.TkwContext();

        public int RetVal { get; private set; }

        #endregion

        #region ctor

        public f_Graf_create_one(int comp)
            : base()
        {
            InitializeComponent();
            _compParam = comp;
        }

        ~f_Graf_create_one()
        {
            if (context != null)
            {
                context.Dispose();
                context = null;
            }
        }

        #endregion

        private async void f_Graf_create_one_Shown(object sender, EventArgs e)
        {
            await _fillGrdAsync();
        }

        private void txt_Enter(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control == null)
                return;
            Color color;
            TextBox txt, txtNotActive;
            if (control.Equals(txtRed))
            {
                color = Color.LightCoral;
                txt = txtRed;
                txtNotActive = txtBlue;
            }
            else
            {
                color = Color.LightBlue;
                txt = txtBlue;
                txtNotActive = txtRed;
            }
            this.Tag = txt; 
            txt.BackColor = color;
            txtNotActive.BackColor= default(Color);
        }

        private async Task _fillGrdAsync()
        {
            try
            {
                List<BLL.CompUser> users = await Task.Run(() => context.CompUsers.Where(w => w.CompId == _compParam).AsEnumerable()
                    .Where(w => !w.Id.IN(((txtRed.Tag as int?) ?? 0), ((txtBlue.Tag as int?) ?? 0)))
                    .OrderBy(o => o.FIO).ToList());
                bsRows.DataSource = users;
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BLL.BllException ex)
            {
                new UiException(ex.Message, ex).ShowMessage(this);
            }
            catch (Exception)
            {
                new UiException("Неожиданная ошибка при формировании ресурса для ручного добавления графика", new Exception($"Id_comp = {_compParam}")).ShowMessage(this);
            }
        }

        private async void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Enabled = false;
                int? red = txtRed.Tag as int?;
                int? blue = txtBlue.Tag as int?;
                if (red == null || blue == null)
                    return;
                List<BLL.CompGraf> lst = await Task.Run(() =>
                    context.CompGrafs.Where(w => (w.RedUser != null ? w.RedUser.CompId : (w.BlueUserId ?? 0)) == _compParam && w.Actual).ToList()
                );
                int etap = lst.Max(m => m?.Etap) ?? 1;
                int maxNomer = (lst.Max(m => m?.Nomer) ?? 0) + 1;
                BLL.CompGraf graf = new BLL.CompGraf { RedUserId = red.Value, BlueUserId = blue.Value, Begin = DateTime.Now, Etap = etap, GrafStatusId = (int)BLL.GrafStatus.Statuses.New, Nomer = maxNomer, Actual = true };
                context.CompGrafs.Add(graf);
                RetVal = await context.SaveChangesAsync();
                DialogResult = DialogResult.OK;
            }
            catch (UiException ex) { ex.ShowMessage(this); DialogResult = DialogResult.Abort; }
            catch (BLL.BllException ex)
            {
                new UiException(ex.Message, ex).ShowMessage(this);
                DialogResult = DialogResult.Abort;
            }
            catch (Exception)
            {
                new UiException("Неожиданная ошибка при выборе участника для ручного добавления графика", new Exception($"Id_comp = {_compParam}")).ShowMessage(this);
                DialogResult = DialogResult.Abort;
            }
            finally { btnSave.Enabled = true; }
        }

        private async void grdRows_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                grdRows.CellMouseDoubleClick -= grdRows_CellMouseDoubleClick;
                BLL.CompUser user = (BLL.CompUser)grdRows.Rows[e.RowIndex].DataBoundItem;
                if (user == null)
                    return;
                TextBox txt = this.Tag as TextBox;
                if (txt == null)
                    return;
                txt.Text = user.FIO;
                txt.Tag = user.Id;
                await _fillGrdAsync();
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BLL.BllException ex)
            {
                new UiException(ex.Message, ex).ShowMessage(this);
            }
            catch (Exception)
            {
                new UiException("Неожиданная ошибка при выборе участника для ручного добавления графика", new Exception($"Id_comp = {_compParam}")).ShowMessage(this);
            }
            finally { grdRows.CellMouseDoubleClick += grdRows_CellMouseDoubleClick; }
        }
    }
}
