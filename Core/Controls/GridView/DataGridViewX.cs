using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TkwEF.Core.Extentions;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Reflection;
using System.Collections;
using TkwEF.Core.UI;

namespace TkwEF.Core.Controls
{
    [ToolboxBitmap(@"~/Images/16x16/grid.png")]
    //[ToolboxBitmap(@"Y:\RDBS\SkyNET\SkyNET\Images\16x16\grid.png")]
    public partial class DataGridViewX_Base : DataGridView
    {
        #region Events

        /// <summary>
        /// Событие после завершения создания экземпляра
        /// </summary>
        public event EventHandler CreatoredEventHandled;
        /// <summary>
        /// Вызов события CreatoredEventHandled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnCreatoredEventHandled(object sender, EventArgs e)
        {
            //PropertiesXML.Save(GlobalClient.CurrentUser.Id, this);
            CreatoredEventHandled?.Invoke(sender, e);
        }

        #endregion

        #region Fields

        //20180515
        /// <summary>
        /// Возвращает или устанавливает значение является ли пользователь суперыизором ( присваивается из кода )
        /// </summary>
        [Browsable(false)]
        [DefaultValue(false)]
        [Description("Является ли пользователь суперыизором ( присваивается из кода )")]
        public bool IsSupervisor { get; set; }

        /// <summary>
        /// Возвращает или устанавливает значение нужно ли показывать детализацию по двойному клику по заголовкам или ячейке (требуется устанровить св-во IsSupervisor)
        /// </summary>
        [Browsable(true)]
        [DefaultValue(false)]
        [Description("Нужно ли показывать детализацию по двойному клику по заголовкам или ячейке (требуется устанровить св-во IsSupervisor)")]
        public bool ShowDetailsByDoubleClick { get; set; }
        //===================

        private bool _fillRowsNumber = true;
        private object _lock = new object();
        /// <summary>
        /// Возвращает предшевствующий активации активный контрол
        /// </summary>

        private bool _allowReadOnlyChangeBackColor = true;
        /// <summary>
        /// Будет ли производиться изменение фона доступных для редактирования ячеек
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [Description("Будет ли производиться изменение фона доступных для редактирования ячеек")]
        public bool AllowReadOnlyChangeBackColor
        {
            get { return _allowReadOnlyChangeBackColor; }
            set { _allowReadOnlyChangeBackColor = value; }
        }

        private bool _allowShowNegativeValue = true;
        /// <summary>
        /// Будет ли производиться изменение цвета шрифта, где значение меньше нуля
        /// </summary>
        [Browsable(true)]
        [DefaultValue(true)]
        [Description("Будет ли производиться изменение цвета шрифта, где значение меньше нуля")]
        public bool AllowShowNegativeValue
        {
            get { return _allowShowNegativeValue; }
            set { _allowShowNegativeValue = value; }
        }

        /// <summary>
        /// Нужно ли заполнять номера строк
        /// </summary>
        [DefaultValue(true)]
        [Description("Нужно ли заполнять номера строк")]
        public bool AllowRowsHeaderNumberFill
        {
            get { return _fillRowsNumber; }
            set { _fillRowsNumber = value; }
        }

        private bool _allowTabStop = false;
        /// <summary>
        /// Нужно ли остонавливаться на не редактируемых ячейках при нажатии клавиши TAB или ENTER
        /// </summary>
        [DefaultValue(false)]
        [Description("Нужно ли остонавливаться на не редактируемых ячейках при нажатии клавиши TAB или ENTER")]
        public bool AllowTabStop { get { return _allowTabStop; } set { _allowTabStop = value; } }

        #endregion

        #region ctor

        public DataGridViewX_Base()
        {
            InitializeComponent();
            //// и устанавливаем значение true при создании экземпляра класса
            //this.DoubleBuffered = true;
            // или с помощью метода SetStyle
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            _setStyle();
            OnCreatoredEventHandled(this, EventArgs.Empty);
        }

        public DataGridViewX_Base(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            //// и устанавливаем значение true при создании экземпляра класса
            //this.DoubleBuffered = true;
            // или с помощью метода SetStyle
            this.SetStyle(ControlStyles.DoubleBuffer |
                ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            _setStyle();
            OnCreatoredEventHandled(this, EventArgs.Empty);
        }

        #endregion

        #region Override

        // Убрал 20140221
        //protected override bool ProcessDialogKey(Keys keyData)
        //{
        //if (AllowTabStop)
        //{
        //    if (keyData == Keys.Enter || keyData == Keys.Tab)
        //    {
        //        MyProcessTabKey(Keys.Tab);
        //        return true;
        //    }
        //}
        //    return base.ProcessDialogKey(keyData);
        //}

        // Убрал 20140221
        //protected override bool ProcessDataGridViewKey(KeyEventArgs e)
        //{
        //if (AllowTabStop)
        //{
        //    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
        //    {
        //        MyProcessTabKey(Keys.Tab);
        //        return true;
        //    }
        //}
        //    try
        //    {
        //        return base.ProcessDataGridViewKey(e);
        //    }
        //    catch (InvalidOperationException) { return true; }
        //}

        //Убрал 20140221
        //protected bool MyProcessTabKey(Keys keyData)
        //{
        //bool retValue = base.ProcessTabKey(keyData);
        //this.SuspendLayout();
        ////Thread t = Thread.CurrentThread;
        //while (this.CurrentCell.ReadOnly)
        //{
        //    //t.Join(1000);
        //    retValue = base.ProcessTabKey(keyData);
        //}
        ////t.Join(1000);
        //this.ResumeLayout(false);
        //return retValue;
        //}

        //public new DataGridViewCell CurrentCell
        //{
        //    get { return base.CurrentCell; }
        //    private set
        //    {
        //        base.CurrentCell = value;
        //    }
        //}
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }
        //[DebuggerNonUserCode]
        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;
            try
            {
                if (Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText != string.Empty)
                    Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Empty;
                _paintCell(e);
            }
            catch (Exception ex) { Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = ex.Message; }
            finally
            {
            }
        }
        protected override void OnCellDoubleClick(DataGridViewCellEventArgs e)
        {
            if (IsSupervisor && ShowDetailsByDoubleClick && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string caption = string.Format("{0} ( RowIndex={1}; ColumnIndex={2} )", this.Name, e.RowIndex, e.ColumnIndex);

                MessageBox.Show(this, string.Format("Grid ReadOnly = {0}", this.ReadOnly), caption);
                MessageBox.Show(this, string.Format("Row ReadOnly = {0}", this.Rows[e.RowIndex].ReadOnly), caption);
                MessageBox.Show(this, string.Format("Column ReadOnly = {0}", this.Columns[e.ColumnIndex].ReadOnly), caption);
                MessageBox.Show(this, string.Format("Cell ReadOnly = {0}", this[e.ColumnIndex, e.RowIndex].ReadOnly), caption);

                if (MessageBox.Show(this, "Показать стили", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    MessageBox.Show(this, string.Format("Cell InheritedStyle = {0}", this[e.ColumnIndex, e.RowIndex].InheritedStyle), caption);
                    MessageBox.Show(this, string.Format("Cell Style = {0}", this[e.ColumnIndex, e.RowIndex].Style), caption);
                    MessageBox.Show(this, string.Format("Row InheritedStyle = {0}", this.Rows[e.RowIndex].InheritedStyle), caption);
                    MessageBox.Show(this, string.Format("Row DefaultCellStyle = {0}", this.Rows[e.RowIndex].DefaultCellStyle), caption);
                    MessageBox.Show(this, string.Format("RowsDefaultCellStyle = {0}", this.RowsDefaultCellStyle), caption);
                    MessageBox.Show(this, string.Format("Column InheritedStyle = {0}", this.Columns[e.ColumnIndex].InheritedStyle), caption);
                    MessageBox.Show(this, string.Format("Column DefaultCellStyle = {0}", this.Columns[e.ColumnIndex].DefaultCellStyle), caption);
                    MessageBox.Show(this, string.Format("Grid DefaultCellStyle = {0}", this.DefaultCellStyle), caption);
                }

                MessageBox.Show(this, "DataPropertyName = " + this.Columns[e.ColumnIndex].DataPropertyName);
                BindingSource bs = this.DataSource as BindingSource;
                if (bs == null) return;
                if (MessageBox.Show(this, "Показать поля привязянные к таблице", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    IEnumerable enumerable = bs.DataSource as IEnumerable;
                    if (enumerable == null) return;
                    Type t = enumerable.AsQueryable().ElementType;

                    //var newEnumerable = typeof(Enumerable).GetMethods().Where(m => m.Name == "ToList").First().MakeGenericMethod(t).Invoke(null, new[] { enumerable });

                    PropertyInfo[] fiList = t.GetProperties();
                    foreach (PropertyInfo fi in fiList)
                    {
                        if (MessageBox.Show(this, fi.Name + "\nПродолжить", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                            break;
                    }
                }
            }
            base.OnCellDoubleClick(e);
        }
        protected override void OnCellMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseDoubleClick(e);
            //if (e.RowIndex < 0 && e.ColumnIndex < 0 && GlobalClient.CurrentUser.IsSupervisor)
            //{
            //    MessageBox.Show(this, string.Format("DefaultCellStyle:\n{0}", DefaultCellStyle.ToString()), "");
            //}
        }
        protected override void OnCellMouseClick(DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && (e.RowIndex < 0 || e.ColumnIndex < 0))
                return;
            base.OnCellMouseClick(e);
        }
        protected override void OnCurrentCellDirtyStateChanged(EventArgs e)
        {
            base.OnCurrentCellDirtyStateChanged(e);
            if (this.IsCurrentCellInEditMode && this.CurrentCell.RowIndex >= 0 && this.CurrentCell.ColumnIndex >= 0)
            {
                DataGridViewColumn clmn = Columns[CurrentCell.ColumnIndex];
                if ((clmn is DataGridViewCheckBoxColumn) && !CurrentCell.ReadOnly)
                    this.EndEdit();
            }
        }
        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            // 20160425 Без нее, ошибка при загрузки Справочника номенклатур
            base.OnDataBindingComplete(e);
            if (this.AllowRowsHeaderNumberFill && RowHeadersVisible)
            {
                SizeF textSize;
                if (this.Rows.Count.ToString().Length > 2)
                    textSize = TextRenderer.MeasureText(this.Rows.Count.ToString(), this.RowHeadersDefaultCellStyle.Font);
                else
                    textSize = TextRenderer.MeasureText("11", this.RowHeadersDefaultCellStyle.Font);
                RowHeadersWidth = (int)textSize.Width + 15;
            }
        }
        protected override void OnRowHeaderMouseDoubleClick(DataGridViewCellMouseEventArgs e)
        {
            base.OnRowHeaderMouseDoubleClick(e);
            if (IsSupervisor && ShowDetailsByDoubleClick && e.Button == MouseButtons.Right)
            {
                MessageBox.Show(this, string.Format("RowsDefaultCellStyle:\n{0}", this.RowsDefaultCellStyle.ToString()), "");
                MessageBox.Show(this, string.Format("Rows[{1}].HeaderCell.Style:\n{0}", this.Rows[e.RowIndex].HeaderCell.Style.ToString(), e.RowIndex), "");
                MessageBox.Show(this, string.Format("Rows[{1}].DefaultCellStyle:\n{0}", this.Rows[e.RowIndex].DefaultCellStyle.ToString(), e.RowIndex), "");
            }
        }
        protected override void OnRowPostPaint(DataGridViewRowPostPaintEventArgs e)
        {
            base.OnRowPostPaint(e);
            if (e.RowIndex < 0) return;
            if (this.AllowRowsHeaderNumberFill && RowHeadersVisible)
                this.DrawNumberRows(e);
            //_postPaintRowCells(e.RowIndex);
        }

        #endregion

        /// <summary>
        /// Убрал 20140221
        /// Сделал для устранения ошибки "Operation is not valid because it results in a reentrant call to the SetCurrentCellAddressCore function"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void DataGridViewX_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //lock (_lock)
        //{
        //    if (e.ColumnIndex >= 0 && e.RowIndex >= 0 && (CurrentCell == null || CurrentCell.RowIndex != e.RowIndex || CurrentCell.ColumnIndex != e.ColumnIndex))
        //        this.CurrentCell = this[e.ColumnIndex, e.RowIndex]; 
        //}
        //}

        #region Private

        private void _setStyle()
        {
            this.DefaultCellStyle = new DataGridViewCellStyle(this.DefaultCellStyle.Clone());
            this.DefaultCellStyle.BackColor = GlobalClient.Properties.GridCellBackColorReadOnly;
            this.DefaultCellStyle.ForeColor = GlobalClient.Properties.GridCellForeColor;
            this.DefaultCellStyle.SelectionBackColor = GlobalClient.Properties.GridSelectionBackColorCell;
            this.DefaultCellStyle.SelectionForeColor = GlobalClient.Properties.GridSelectionForeColorCell;

            this.ColumnHeadersDefaultCellStyle.BackColor = GlobalClient.Properties.GridHeaderBackColor;
            this.ColumnHeadersDefaultCellStyle.ForeColor = GlobalClient.Properties.GridHeaderForeColor;
            this.ColumnHeadersDefaultCellStyle.SelectionBackColor = GlobalClient.Properties.GridHeaderSelectionBackColor;
            this.ColumnHeadersDefaultCellStyle.SelectionForeColor = GlobalClient.Properties.GridHeaderSelectionForeColor;
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            this.EnableHeadersVisualStyles = false;
            Invalidate();
        }

        private void _postPaintRowCells(int ri)
        {
            if (ri < 0) return;
            if (this.AllowReadOnlyChangeBackColor || AllowShowNegativeValue)
            {
                foreach (DataGridViewCell cell in this.Rows[ri].Cells)
                {
                    if (AllowReadOnlyChangeBackColor)
                    {
                        if (!cell.ReadOnly)
                        {
                            cell.Style.BackColor = UI.GlobalClient.Properties.GridCellBackColorNotReadOnly;
                            cell.Style.SelectionBackColor = UI.GlobalClient.Properties.GridSelectedCellBackColorNotReadOnly;
                        }
                        else
                        {
                            cell.Style.BackColor =
                                Columns[cell.ColumnIndex].DefaultCellStyle != null && Columns[cell.ColumnIndex].DefaultCellStyle.BackColor != null ?
                                Columns[cell.ColumnIndex].DefaultCellStyle.BackColor :
                                DefaultCellStyle.BackColor;
                            cell.Style.SelectionBackColor = DefaultCellStyle.SelectionBackColor;
                        }
                    }
                    if (AllowShowNegativeValue)
                    {
                        if (cell.Value is int)
                        {
                            if (((int)cell.Value) < 0) cell.Style.ForeColor = GlobalClient.Properties.GridCellErrorForeColor; else cell.Style.ForeColor = GlobalClient.Properties.GridCellForeColor;
                        }
                        else if (cell.Value is decimal)
                        {
                            if (((decimal)cell.Value) < 0) cell.Style.ForeColor = GlobalClient.Properties.GridCellErrorForeColor; else cell.Style.ForeColor = GlobalClient.Properties.GridCellForeColor;
                        }
                        else
                            cell.Style.ForeColor = GlobalClient.Properties.GridCellForeColor;
                    }
                }
                //Invalidate();
            }
        }
        /// <summary>
        /// Окрашивание ячейки
        /// </summary>
        /// <param name="ri">Индекс строки</param>
        /// <param name="ci">Индекс колонки</param>
        private void _paintCell(DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || (!AllowReadOnlyChangeBackColor && !AllowShowNegativeValue)) return;
            DataGridViewCell cell = this[e.ColumnIndex, e.RowIndex];
            if (AllowReadOnlyChangeBackColor)
            {
                if (!cell.ReadOnly)
                {
                    //20160506 В форме f_Kassa_analyz_fin вызывает непрорисовку и не видно диалоговых сообщений
                    //cell.Style.ForeColor = UI.GlobalClient.Properties.GridCellForeColorNotReadOnly;
                    //cell.Style.SelectionBackColor = UI.GlobalClient.Properties.GridSelectedCellBackColorNotReadOnly;
                    //20170313
                    e.CellStyle.BackColor = GlobalClient.Properties.GridCellBackColorNotReadOnly;
                    e.CellStyle.SelectionBackColor = GlobalClient.Properties.GridSelectedCellBackColorNotReadOnly;
                    e.CellStyle.ForeColor = UI.GlobalClient.Properties.GridCellForeColorNotReadOnly;
                    ////////if (e.FormattedValue is String)
                    ////////{
                    ////////    TextRenderer.DrawText(e.Graphics,
                    ////////        (string)e.FormattedValue,
                    ////////        this.Font,
                    ////////        e.CellBounds, SystemColors.GrayText);
                    ////////}
                    ////////e.PaintBackground(e.CellBounds, true);
                    ////////using (Brush gridBrush = new SolidBrush(System.Drawing.Color.Blue), backColorBrush = new SolidBrush(this[e.ColumnIndex, e.RowIndex].Style.BackColor))
                    ////////{
                    ////////    using (Pen gridLinePen = new Pen(gridBrush, 2))
                    ////////    {
                    ////////        e.Graphics.DrawLine(gridLinePen,
                    ////////                            e.CellBounds.Left + ((e.CellBounds.Right - e.CellBounds.Left) / 2), e.CellBounds.Top,
                    ////////                            e.CellBounds.Left + ((e.CellBounds.Right - e.CellBounds.Left) / 2), e.CellBounds.Top + ((e.CellBounds.Bottom - e.CellBounds.Top) / 3));
                    ////////    }
                    ////////}
                    ////////e.Handled = true;

                    //e.PaintBackground(e.CellBounds, true);
                    //e.PaintContent(e.CellBounds);
                    //Rectangle rc = e.CellBounds;
                    //rc.Inflate(-2, -2);
                    //rc.Offset(-1, -1);
                    //if (((int)e.State & (int)DataGridViewElementStates.Selected) > 0)
                    //    e.Graphics.DrawRectangle(Pens.White, rc);
                    //else
                    //    e.Graphics.DrawRectangle(Pens.Black, rc);
                    //e.Handled = true;
                }
            }
            if (AllowShowNegativeValue)
            {
                //20170210
                //if (cell.Value is int)
                //{
                //    if (((int)cell.Value) < 0) cell.Style.ForeColor = GlobalClient.Properties.GridCellError; else cell.Style.ForeColor = GlobalClient.Properties.GridCellForeColor;
                //}
                //else if (cell.Value is decimal)
                //{
                //    if (((decimal)cell.Value) < 0) cell.Style.ForeColor = GlobalClient.Properties.GridCellError; else cell.Style.ForeColor = GlobalClient.Properties.GridCellForeColor;
                //}
                //else
                //    cell.Style.ForeColor = GlobalClient.Properties.GridCellForeColor;
                //20170210
                if (cell.Value is int)
                {
                    if (((int)cell.Value) < 0)
                        cell.Style.ForeColor = GlobalClient.Properties.GridCellErrorForeColor;
                }
                else if (cell.Value is decimal)
                {
                    if (((decimal)cell.Value) < 0)
                        e.CellStyle.ForeColor = GlobalClient.Properties.GridCellErrorForeColor;
                }
            }
        }

        #endregion

        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {
            //20161118
            this.CurrentCell.ErrorText = string.Empty;
            e.Control.BackColor = e.CellStyle.BackColor = GlobalClient.Properties.GridCellBackColorNotReadOnly;
            //e.CellStyle.SelectionForeColor = GlobalClient.Properties.GridSelectionBackColorCell;
            base.OnEditingControlShowing(e);
        }
        private void DataGridViewX_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                Rows[e.RowIndex].Cells[e.ColumnIndex].ErrorText = string.Format("Ошибка: {0}", e.Exception.Message);
        }

        public void SetReadOnlyNot(DataGridViewColumn[] clmnsNotReadOnly)
        {
            this.ReadOnly = false;
            foreach (DataGridViewColumn column in this.Columns)
                column.ReadOnly = clmnsNotReadOnly.FirstOrDefault(f => f.Equals(column)) == null;
        }

        private void DataGridViewX_MouseHover(object sender, EventArgs e)
        {
            //this.Focus();
        }
    }
}
