using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TkwEF.UI
{
    public partial class f_Main : f_BUI
    {
        private volatile object _lock = new object();
        bool filling = false;

        public f_Main()
        {
            InitializeComponent();
        }

        #region Handled

        private void f_Main_Shown(object sender, EventArgs e)
        {
            _fillGrdComp();
            bsComp.CurrentChanged += bsComp_CurrentChanged;
            bsComp_CurrentChanged(sender, e);
        }

        private void smiClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void msiSpravFizLitas_Click(object sender, EventArgs e)
        {
            Sprav.f_SpravFizLitsa frm = null;
            try
            {
                msiSpravFizLitas.Enabled = false;
                frm = new Sprav.f_SpravFizLitsa(Sprav.f_SpravFizLitsa.Mode.Add | Sprav.f_SpravFizLitsa.Mode.Edit);
                frm.FormClosed += (s, ev) =>
                {
                    this.BringToFront();
                    msiSpravFizLitas.Enabled = true;
                };
                frm.Show(this);
            }
            catch (UiException ex)
            {
                ex.ShowMessage(this);
            }
            catch (Exception ex)
            {
                new UiException(string.Format("Неопределенная ошибка при работе с формой {0}", frm == null ? "Н/Д" : frm.Text), ex).ShowMessage(this);
            }
        }
        private void msiSpravPoyas_Click(object sender, EventArgs e)
        {
            Util<Sprav.f_SpravPoyas>.OperForm(this, (ToolStripItem)sender);
        }

        private void msiVesKat_Click(object sender, EventArgs e)
        {
            Util<Sprav.f_SpravVesKat>.OperForm(this, (ToolStripItem)sender);
        }

        private void msiSpravClub_Click(object sender, EventArgs e)
        {
            Util<Sprav.f_SpravClub>.OperForm(this, (ToolStripItem)sender);
        }

        private void msiClubUsers_Click(object sender, EventArgs e)
        {
            Util<Sprav.f_SpravUserClub>.OperForm(this, (ToolStripItem)sender);
        }

        private void tsbAddComp_Click(object sender, EventArgs e)
        {
            tsComp.Enabled = false;
            Comp.f_Comp frm = (Comp.f_Comp)Util<Comp.f_Comp>.OperForm(this, null);
            frm.FormClosed += async (s, ev) =>
            {
                _fillGrdComp();
                await _fillGrdCompUsers();
                tsComp.Enabled = true;
            };
        }

        private void tsbEditComp_Click(object sender, EventArgs e)
        {
            tsComp.Enabled = false;
            int? id = ((BLL.Competition)bsComp.Current)?.Id;
            if(!id.HasValue)
            {
                tsComp.Enabled = true;
                return;
            }
            Comp.f_Comp frm = new Comp.f_Comp(id.Value);
            frm.FormClosed += async (s, ev) =>
            {
                if(frm.IsDisposed || frm.Disposing || grdComp.Disposing || grdComp.IsDisposed)
                {

                }
                _fillGrdComp();
                await _fillGrdCompUsers();
                tsComp.Enabled = true;
                this.BringToFront();
            };
            frm.Show(this);
        }

        #endregion

        #region Private function

        private void _fillGrdComp()
        {
            var lst = context.Competitions.Where(w => w.Actual).OrderByDescending(o => o.DateBegin).ToList();
            bsComp.DataSource = lst;
        }
        private async Task _fillGrdCompUsers()
        {
            if (bsComp.Current == null)
            {
                bsCompUsers.DataSource = null;
                return;
            }
            BLL.Competition comp = null;
            try
            {
                List<BLL.CompUser> lst = null;
                lock (_lock)
                {
                    if (filling)
                        return;
                    filling = true;
                    comp = (BLL.Competition)bsComp.Current;
                }
                lst = await Task.Run(() => context.CompUsers.Where(w => (w.Comp == null ? 0 : w.Comp.Id) == comp.Id && w.Actual)
                    .AsEnumerable().OrderBy(o => o.FIO).ToList());
                //lst = await Task.Run(async () =>
                //{
                //    using (BLL.TkwContext context = new BLL.TkwContext())
                //    {
                //        return context.CompUsers.Where(w => (w.Comp == null ? 0 : w.Comp.Id) == comp.Id).ToList();
                //    } 
                //});
                lock (_lock)
                {
                    bsCompUsers.DataSource = new BindingList<BLL.CompUser>(lst);
                    bsCompUsers.DataMember = null;
                    filling = false;
                }
            }
            catch (UiException ex) { filling = false; ex.ShowMessage(this); }
            catch (Exception ex)
            {
                filling = false;
                new UiException("Необработанная ошибка при изменении текущего значения соревнования"
                    , new Exception(string.Format("Id_comp = {0}", comp?.Id ?? 0), ex)).ShowMessage(this);
            }
        }

        #endregion

        #region Private class

        private static class Util<T> where T : Form
        {
            public static Form OperForm(Form parent, ToolStripItem ctrl)
            {
                T frm = null;
                try
                {
                    if (ctrl != null)
                        ctrl.Enabled = false;
                    frm = Activator.CreateInstance<T>();
                    frm.FormClosed += (s, ev) =>
                    {
                        parent.BringToFront();
                        if (ctrl != null)
                            ctrl.Enabled = true;
                    };
                    frm.Show(parent);
                    return frm;
                }
                catch (UiException ex)
                {
                    ex.ShowMessage(parent);
                    return null;
                }
                catch (Exception ex)
                {
                    new UiException(string.Format("Неопределенная ошибка при работе с формой {0}", frm == null ? "Н/Д" : frm.Text), ex).ShowMessage(parent);
                    return null;
                }
            }
        }

        #endregion

        private async void bsComp_CurrentChanged(object sender, EventArgs e)
        {
            await _fillGrdCompUsers();
        }
    }
}
