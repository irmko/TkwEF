using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TkwEF.Core.Extentions;
using System.Data.Entity;
using System.Diagnostics;
using TkwEF.BLL;
using TkwEF.Helper;

namespace TkwEF.UI.Graf
{
    public partial class f_Graf : TkwEF.UI.f_BUI
    {
        #region Fields

        private int _comp;
        private List<BLL.CompGraf> _src
        {
            get
            {
                return ((IEnumerable<BLL.CompGraf>)bsGrafs.DataSource).ToList();
            }
        }

        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int colIndexFromMouuseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        private int colIndexOfItemUnderMouseToDrop;

        bool dialogResultOk = false;

        #endregion

        #region ctor

        public f_Graf(int comp) : base()
        {
            InitializeComponent();

            //if (src is List<BLL.CompGraf>)
            //    bsGrafs.DataSource = (List<BLL.CompGraf>)src;
            //else
            //    bsGrafs.DataSource = src.ToList();
            //if (_src.Count() > 0)
            //{
            //    BLL.CompGraf graf = _src.First();
            //    _comp = graf.RedUser != null ? graf.RedUser.Comp : graf.BlueUser?.Comp;
            //}
            _comp = comp;
        }

        #endregion

        #region Handled

        private void f_Graf_Load(object sender, EventArgs e)
        {
            _fillGrd();
            tscType.SelectedIndex = 0;
            _setBtnEnabled();
        }

        private void grdGrafs_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DataGridViewTextBoxCell)))
            {
                Point clientPoint = grdGrafs.PointToClient(new Point(e.X, e.Y));
                var ht = grdGrafs.HitTest(clientPoint.X, clientPoint.Y);
                if (ht.ColumnIndex < 0 || ht.RowIndex < 0)
                    e.Effect = DragDropEffects.None;
                else
                {
                    BLL.CompGraf data = (BLL.CompGraf)grdGrafs.Rows[ht.RowIndex].DataBoundItem;
                    if (data.AllowEditPobeditel())
                        e.Effect = DragDropEffects.Move;
                    else
                        e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void grdGrafs_DragDrop(object sender, DragEventArgs e)
        {
            Point clientPoint = grdGrafs.PointToClient(new Point(e.X, e.Y));
            var ht = grdGrafs.HitTest(clientPoint.X, clientPoint.Y);
            if (rowIndexFromMouseDown == ht.RowIndex
                || !colIndexFromMouuseDown.IN(ColumnBlueUserFIO.Index, ColumnRedUserFIO.Index)
                || !ht.ColumnIndex.IN(ColumnBlueUserFIO.Index, ColumnRedUserFIO.Index))
                return;
            rowIndexOfItemUnderMouseToDrop = ht.RowIndex;
            colIndexOfItemUnderMouseToDrop = ht.ColumnIndex;
            _changeUserInGraf();
        }

        private void _changeUserInGraf()
        {
            if (rowIndexFromMouseDown == rowIndexOfItemUnderMouseToDrop 
                || !colIndexFromMouuseDown.IN(ColumnBlueUserFIO.Index, ColumnRedUserFIO.Index)
                || !colIndexOfItemUnderMouseToDrop.IN(ColumnBlueUserFIO.Index, ColumnRedUserFIO.Index))
                return;
            BLL.CompGraf cgOut = _src[rowIndexFromMouseDown];
            BLL.CompGraf cntOut = context.CompGrafs.FirstOrDefault(f => f.Id == cgOut.Id);
            if (!cntOut.AllowEditPobeditel())
                return;
            string fioRed = cntOut.RedUserFIO;
            string fioBlue = cntOut.BlueUserFIO;
            if (cgOut.RedUser?.Id != cntOut?.RedUser?.Id || cgOut.BlueUser?.Id != cntOut?.BlueUser?.Id)
                throw new UiException("Данные были изменены\nОбновите данные и попробуйте снова.");
            //cgOut = tmp;

            BLL.CompGraf cgIn = _src[rowIndexOfItemUnderMouseToDrop];
            BLL.CompGraf cntIn = context.CompGrafs.FirstOrDefault(f => f.Id == cgIn.Id);
            if (!cntIn.AllowEditPobeditel())
                return;
            if (cgIn.RedUser?.Id != cntIn?.RedUser?.Id || cgIn.BlueUser?.Id != cntIn?.BlueUser?.Id)
                throw new UiException("Данные были изменены\nОбновите данные и попробуйте снова.");
            //cgIn = context.CompGrafs.FirstOrDefault(w => w.Id == cgIn.Id);

            fioRed = cntIn.RedUserFIO;
            fioBlue = cntIn.BlueUserFIO;

            string propOut = grdGrafs.Columns[colIndexFromMouuseDown].DataPropertyName;
            string propIn = grdGrafs.Columns[colIndexOfItemUnderMouseToDrop].DataPropertyName;

            PropertyInfo piOut;
            if (propOut == "BlueUserFIO")
            {
                piOut = cntOut.GetType().GetProperty("BlueUser");
            }
            else if (propOut == "RedUserFIO")
            {
                piOut = cntOut.GetType().GetProperty("RedUser");
            }
            else
                //throw new UiException("Не удалось определить название свойства для исходящей записи графика", new Exception(string.Format("PropOut={0}", propOut)));
                return;

            PropertyInfo piIn;
            if (propIn == "BlueUserFIO")
            {
                piIn = cntIn.GetType().GetProperty("BlueUser");
            }
            else if (propIn == "RedUserFIO")
            {
                piIn = cntIn.GetType().GetProperty("RedUser");
            }
            else
                //throw new UiException("Не удалось определить название свойства для входящей записи графика", new Exception(string.Format("PropIn={0}", propIn)));
                return;

            var valOut = piOut.GetValue(cntOut);
            var valIn = piIn.GetValue(cntIn);
            if ((valIn?.Equals(valOut) ?? true) && (valOut?.Equals(valIn) ?? true))
                return;
            piOut.SetValue(cntOut, valIn);
            piIn.SetValue(cntIn, valOut);
            context.Entry(cntIn).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            context.Entry(cntOut).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            _fillGrd();
            //grdGrafs.Refresh();
        }

        private void grdGrafs_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty && !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                    {
                        Debug.WriteLine(string.Format("{0}, RowIndex={1}, ColumnIndex={2};", DateTime.Now.ToLongTimeString(), e.RowIndex, e.ColumnIndex));
                        BLL.CompGraf data = (BLL.CompGraf)grdGrafs.Rows[e.RowIndex].DataBoundItem;
                        Debug.WriteLine(string.Format("AllowEditPobeditel={0}", data.AllowEditPobeditel()));
                        if (!data.AllowEditPobeditel())
                            return;
                    }
                    BLL.CompGraf cgOut = _src[rowIndexFromMouseDown];
                    if (cgOut.Id == 0 || !cgOut.AllowEditPobeditel())
                        return;
                    // Proceed with the drag and drop, passing in the list item.
                    DataGridViewCell cell = grdGrafs.Rows[rowIndexFromMouseDown].Cells[colIndexFromMouuseDown];
                    DragDropEffects dropEffect = grdGrafs.DoDragDrop(cell, DragDropEffects.Move);
                }
            }
        }

        private void grdGrafs_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            var ht = grdGrafs.HitTest(e.X, e.Y);
            rowIndexFromMouseDown = ht.RowIndex;
            colIndexFromMouuseDown = ht.ColumnIndex;
            if (rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;
                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2), e.Y - (dragSize.Height / 2)), dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void grdGrafs_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private async void tsbSaveGraf_Click(object sender, EventArgs e)
        {
            try
            {
                tsbSaveGraf.Enabled = false;
                if (MessageBox.Show(this, "Сохранить график", "Сохранение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == DialogResult.Cancel)
                    return;
                await _saveGrafs();
                _fillGrd();
                tsbSformirGraf.Visible = true;
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (Exception ex) { new UiException("Неопределенная ошибка при сохранении графика", ex).ShowMessage(this); }
            finally
            {
                _setBtnEnabled();
            }
        }

        private void grdGrafs_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == ColumnPobeditelFIO.Index)
            {
                BLL.CompGraf graf = (BLL.CompGraf)grdGrafs.Rows[e.RowIndex].DataBoundItem;
                if (graf.AllowEditPobeditel())
                {
                    e.CellStyle.BackColor = Core.UI.ClientProperties.Instance.GridCellBackColorNotReadOnly;
                }
                else
                {
                    e.CellStyle.BackColor = grdGrafs.Rows[e.RowIndex].Cells[0].InheritedStyle.BackColor;
                }
            }
        }

        private async void grdGrafs_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex != ColumnPobeditelFIO.Index)
                return;
            try
            {
                //using (BLL.TkwContext context = new BLL.TkwContext())
                //{
                BLL.CompGraf graf = (BLL.CompGraf)grdGrafs.Rows[e.RowIndex].DataBoundItem;
                if (!graf.AllowEditPobeditel())
                    return;
                graf = context.CompGrafs.Single(s => s.Id == graf.Id);
                if (graf.AllowEditPobeditel())
                {
                    Point point = grdGrafs.PointToScreen(grdGrafs.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true).Location);
                    int? res = null;
                    using (f_Graf_pobeditel_select_dlg frm = new f_Graf_pobeditel_select_dlg(graf.RedUser, graf.BlueUser, point))
                    {
                        frm.StartPosition = FormStartPosition.Manual;
                        frm.Location = point;
                        DialogResult dr = await frm.ShowDialogAsync(this);
                        if (dr == DialogResult.OK)
                            res = frm.ReturnValue;
                    }
                    if (res.HasValue)
                    {
                        //graf = await Task.Run(() => context.CompGrafs.First(f => f.Id == graf.Id));
                        //graf.Pobeditel = graf.RedUser.Id == res.Value ? graf.RedUser : graf.BlueUser;
                        //graf.GrafStatus = await Task.Run(() => context.GrafStatuses.First(w => w.Id == (int)BLL.GrafStatus.Statuses.Accept));
                        //await Task.Run(() => context.CompUsers.First(f => f.Id == graf.RedUserId).ResetAllowVesChange());
                        //await Task.Run(() => context.CompUsers.First(f => f.Id == graf.BlueUserId).ResetAllowVesChange());
                        await Task.Run(() =>
                        {
                            graf = context.CompGrafs.First(f => f.Id == graf.Id);
                            graf.Pobeditel = graf.RedUser.Id == res.Value ? graf.RedUser : graf.BlueUser;
                            //graf.GrafStatus = context.GrafStatuses.First(w => w.Id == (int)BLL.GrafStatus.Statuses.Accept);
                            graf.GrafStatusId = (int)BLL.GrafStatus.Statuses.Accept;
                            graf.End = DateTime.Now;
                            BLL.CompUser cuRed = context.CompUsers.FirstOrDefault(f => f.Id == graf.RedUserId);
                            cuRed?.ResetAllowVesChange();
                            BLL.CompUser cuBlue = context.CompUsers.FirstOrDefault(f => f.Id == graf.BlueUserId);
                            cuBlue?.ResetAllowVesChange();

                            context.SaveChanges();
                        });
                        await _fillGrdAsync(graf.Id);
                        dialogResultOk = true;
                    }
                }
                //}
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException(string.Format("Неопределенная ошибка при изменении победителя (строка {0})", e.RowIndex), ex).ShowMessage(this);
            }
        }

        private async void tsbAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                tsbAddUser.Enabled = false;
                int? id = null;
                using (f_Graf_create_one frm = new f_Graf_create_one(_comp))
                {
                    DialogResult dr = await frm.ShowDialogAsync();
                    if (dr == DialogResult.OK)
                        id = frm.RetVal;
                }
                await _fillGrdAsync(id);
            }
            catch(UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex) { new UiException("Неопределенная ошибка при добавлении нового пользователя в график", ex).ShowMessage(this); }
            finally
            {
                _setBtnEnabled();
            }
        }

        private async void tsbSformirGraf_Click(object sender, EventArgs e)
        {
            try
            {
                tsBtn.Enabled = false;
                List<CompGraf> compGrafs;
                compGrafs = await BLL.CompGraf.GetData(context, _comp, (BLL.CompGraf.TypeGraf)tscType.SelectedIndex + 1);
                bsGrafs.DataSource = compGrafs;
                tsbSformirGraf.Visible = false;
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex) { new UiException("Неожиданная ошибка при формировании графика соревнований", ex).ShowMessage(this); }
            finally
            {
                _setBtnEnabled();
            }
        }

        private async void tsbCancelGrafTmp_Click(object sender, EventArgs e)
        {
            try
            {
                tsBtn.Enabled = false;
                await _fillGrdAsync(null);
                tsbSformirGraf.Visible = true;
            }
            catch (UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex) { new UiException("Неожиданная ошибка при формировании графика соревнований", ex).ShowMessage(this); }
            finally
            {
                _setBtnEnabled();
            }
        }

        private void f_Graf_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dialogResultOk)
                DialogResult = DialogResult.OK;
        }

        #endregion

        #region Private function

        private void _setBtnEnabled()
        {
            try
            {
                tsBtn.SuspendLayout();
                this.SuspendLayout();

                IEnumerable<BLL.CompGraf> src = (IEnumerable<BLL.CompGraf>)bsGrafs.DataSource;
                tsbSaveGraf.Enabled = src.FirstOrDefault(f => f.Id == 0) != null;
                tsbAddUser.Enabled = true;
                tsbSformirGraf.Enabled = true;
                tsBtn.Enabled = true;

            }
            catch (Exception ex) { new UiException("Ошибка при обработке доступности кнопок графика", ex, true); }
            finally {
                tsBtn.ResumeLayout(false);
                tsBtn.PerformLayout();
                this.ResumeLayout(false);
                this.PerformLayout();
            }
        }
        private async Task _saveGrafs()
        {
            IEnumerable<BLL.CompGraf> src = (IEnumerable<BLL.CompGraf>)bsGrafs.DataSource;
            try
            {
                using (BLL.TkwContext tkwContext = new BLL.TkwContext())
                {
                    //var val = src.Where(w => context.Entry(w).State == System.Data.Entity.EntityState.Modified).ToList();
                    //var tmp = src.ToList();
                    //for (int i = 0; i < tmp.Count; i++)
                    //{
                    //    var v = context.Entry(tmp[i]).State;
                    //}

                    var lst = src.Where(w => w.Id == 0).ToList();
                    var statuses = await Task.Run(() => tkwContext.GrafStatuses.ToList());
                    BLL.GrafStatus status = statuses.First(w => w.Order == (int)BLL.GrafStatus.Statuses.New);
                    foreach (var record in lst)
                    {
                        BLL.CompUser redUser = null, blueUser = null;
                        if (record.RedUser != null)
                            redUser = tkwContext.CompUsers.FirstOrDefault(w => w.Id == record.RedUser.Id);
                        if (record.BlueUser != null)
                            blueUser = tkwContext.CompUsers.FirstOrDefault(w => w.Id == record.BlueUser.Id);
                        BLL.CompGraf cg = new BLL.CompGraf { Nomer = record.Nomer, RedUser = redUser, BlueUser = blueUser, GrafStatus = status, Begin = DateTime.Now, Etap = record.Etap };
                        tkwContext.CompGrafs.Add(cg);
                    }
                    tkwContext.SaveChanges();
                }
            }
            catch(UiException ex) { ex.ShowMessage(this); }
            catch (BllException ex) { new UiException(ex.Message, ex).ShowMessage(this); }
            catch (Exception ex)
            {
                new UiException($"Неопределенная ошибка при сохранении графика (код={_comp}).", ex).ShowMessage(this);
            }
        }
        private async Task _fillGrdAsync(int? id)
        {
            context.CompGrafs.Load();
            var val = context.CompGrafs.Local;
            var tmp = await Task.Run(() => context.CompGrafs.Where(w => w.RedUser != null ? w.RedUser.Comp.Id == _comp : w.BlueUser.Comp.Id == _comp).ToList());
            tmp = context.CompGrafs.Include("Pobeditel").Where(w => w.RedUser != null ? w.RedUser.Comp.Id == _comp : w.BlueUser.Comp.Id == _comp).ToList();
            BindingList<BLL.CompGraf> src = new BindingList<BLL.CompGraf>(tmp.ToList());
            bsGrafs.DataSource = src;
            if(id.HasValue)
            {
                bsGrafs.FindX("Id", id.Value);
            }
        }
        private void _fillGrd()
        {
            //context.CompGrafs.Load();
            var tmp = context.CompGrafs.Where(w => w.RedUser != null ? w.RedUser.Comp.Id == _comp : w.BlueUser.Comp.Id == _comp);
            BindingList<BLL.CompGraf> src = new BindingList<BLL.CompGraf>(tmp.ToList());
            bsGrafs.DataSource = src;
        }

        #endregion

        private void tsbSformirGraf_VisibleChanged(object sender, EventArgs e)
        {
            tsbCancelGrafTmp.Visible = !tsbSformirGraf.Visible;
        }
    }
}
