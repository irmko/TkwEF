using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace TkwEF.Core.Controls
{
    public partial class DataGridViewXF_Base : Controls.DataGridViewX_Base
    {
        #region ctor

        public DataGridViewXF_Base() : base()
        {
            InitializeComponent();
            this.UpdateStyles();
        }

        public DataGridViewXF_Base(IContainer container) : base(container)
        {
            container.Add(this);

            InitializeComponent();
            this.UpdateStyles();
        }

        #endregion

        #region Classes

        class FilterStatus
        {
            // имя колонки
            public string columnName { get; set; }
            // имя свойства источника данных
            public string dataPropertyName { get; set; }
            // значение ячейки
            public string valueString { get; set; }
            // состояние фильтра
            public bool check { get; set; }
        }
        public class ColumnFilterClickedEventArg : EventArgs
        {
            public int ColumnIndex { get; private set; }
            public Rectangle ButtonRectangle { get; private set; }
            public ColumnFilterClickedEventArg(int colIndex, Rectangle btnRect)
            {
                this.ColumnIndex = colIndex;
                this.ButtonRectangle = btnRect;
            }
        }
        public class DataGridFilterHeader : DataGridViewColumnHeaderCell
        {
            #region Fields

            // состояние кнопки
            ComboBoxState currentState = ComboBoxState.Normal;
            Point cellLocation;
            Rectangle buttonRect;

            bool _allowFilter;

            #endregion

            #region ctor

            public DataGridFilterHeader() : base() { }
            public DataGridFilterHeader(DataGridViewColumnHeaderCell hcell, bool allowFilter) : this()
            {
                this.ContextMenuStrip = hcell.ContextMenuStrip;
                this.ErrorText = hcell.ErrorText;
                this.Style = hcell.Style;
                this.Tag = hcell.Tag;
                this.ToolTipText = hcell.ToolTipText;
                this.Value = hcell.Value;
                this.ValueType = hcell.ValueType;

                this._allowFilter = allowFilter && ComboBoxRenderer.IsSupported;

                //PropertyInfo[] propList = this.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
                //PropertyInfo[] hcList = hcell.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);
                //PropertyInfo res;
                //foreach (PropertyInfo pi in hcList)
                //{
                //    res = propList.First(f => f.Name == pi.Name);
                //    if (res.CanWrite)
                //    {
                //        object val = pi.GetValue(hcell, null);
                //        res.SetValue(this, val, null);
                //    }
                //}

                //PropertyInfo piE = this.GetType().GetProperty("ErrorText");
                //PropertyInfo ownerE = hcell.GetType().GetProperty("ErrorText");
                //piE.SetValue(this, string.Empty, null);
            }

            #endregion

            public event EventHandler<ColumnFilterClickedEventArg> FilterButtonClicked;
            // расширим заголовок на 20 пикселей что бы вставить туда кнопку
            protected override void OnDataGridViewChanged()
            {
                if (_allowFilter)
                {
                    try
                    {
                        Padding dropDownPadding = new Padding(0, 0, 20, 0);
                        this.Style.Padding = Padding.Add(this.InheritedStyle.Padding, dropDownPadding);
                    }
                    catch { }
                }
                base.OnDataGridViewChanged();
            }
            // рисуем кнопку
            protected override void Paint(Graphics graphics,
                                      Rectangle clipBounds,
                                      Rectangle cellBounds,
                                      int rowIndex,
                                      DataGridViewElementStates dataGridViewElementState,
                                      object value,
                                      object formattedValue,
                                      string errorText,
                                      DataGridViewCellStyle cellStyle,
                                      DataGridViewAdvancedBorderStyle advancedBorderStyle,
                                      DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds,
                       cellBounds, rowIndex,
                       dataGridViewElementState, value,
                       formattedValue, errorText,
                       cellStyle, advancedBorderStyle, paintParts);
                if (_allowFilter)
                {
                    int width = 20;
                    buttonRect = new Rectangle(cellBounds.X + cellBounds.Width - width, cellBounds.Y, width, cellBounds.Height);
                    cellLocation = cellBounds.Location;
                    if (ComboBoxRenderer.IsSupported)
                        ComboBoxRenderer.DrawDropDownButton(graphics, buttonRect, currentState);
                }
            }
            // анимация нажатия
            protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
            {
                if (this.IsMouseOverButton(e.Location))
                    currentState = ComboBoxState.Pressed;
                base.OnMouseDown(e);
            }
            protected override void OnMouseUp(DataGridViewCellMouseEventArgs e)
            {
                if (this.IsMouseOverButton(e.Location))
                {
                    currentState = ComboBoxState.Normal;
                    this.OnFilterButtonClicked();
                }
                base.OnMouseUp(e);
            }
            private bool IsMouseOverButton(Point e)
            {
                Point p = new Point(e.X + cellLocation.X, e.Y + cellLocation.Y);
                if (p.X >= buttonRect.X && p.X <= buttonRect.X + buttonRect.Width &&
                p.Y >= buttonRect.Y && p.Y <= buttonRect.Y + buttonRect.Height)
                {
                    return true;
                }
                return false;
            }
            // активируем событие
            protected virtual void OnFilterButtonClicked()
            {
                this.FilterButtonClicked?.Invoke(this, new ColumnFilterClickedEventArg(this.ColumnIndex, this.buttonRect));
            }
        }

        #endregion

        #region Fields

        // в этом списке будет состяние фильтров
        List<FilterStatus> Filter = new List<FilterStatus>();
        // элементы всплывающего меню
        //   TextBox для поиска значений
        TextBox textBoxCtrl = new TextBox();
        //   DateTimePicker для поиска даты
        DateTimePicker DateTimeCtrl = new DateTimePicker();
        //   CheckedListBox для выбора фильтров
        CheckedListBox CheckCtrl = new CheckedListBox();
        //   кнопки "Применить" и "Очистить"
        Button ApplyButtonCtrl = new Button();
        Button ClearFilterCtrl = new Button();
        //   и всплывающее меню
        ToolStripDropDown popup = new ToolStripDropDown();

        // надписи в меню
        string StrFilter = "";
        string ButtonCtrlText = "Применить";
        string ClearFilterCtrlText = "Очистить";
        string CheckCtrlAllText = "Все";

        // из события получаем номер колонки
        private int columnIndex { get; set; }

        bool _allowFilter = true;
        [Browsable(true)]
        [DefaultValue(true)]
        [Description("Будет ли производиться фильтрация в заголовке")]
        public bool AllowFilter
        {
            get { return _allowFilter && ComboBoxRenderer.IsSupported; }
            set { _allowFilter = value; }
        }

        #endregion

        #region Handled

        // при добавлении колонок в DataGridView заменяем заголовки на новые
        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            try
            {
                if (AllowFilter)
                {
                    var header = new DataGridFilterHeader(e.Column.HeaderCell, allowFilter: AllowFilter);
                    header.FilterButtonClicked += new EventHandler<ColumnFilterClickedEventArg>(header_FilterButtonClicked);
                    e.Column.HeaderCell = header;
                }
            }
            //20171115
            catch { }
            //catch (Exception ex)
            //{
            //    MailRdbs.SendMailFromRobotToRazrab(curUser: null, subject: "Ошибка в DataGridViewXF (OnColumnAdded)", body: ex.ToString());
            //}
            //--------------
            base.OnColumnAdded(e);
        }

        // показываем меню при нажатии на кнопку фильтрации
        void header_FilterButtonClicked(object sender, ColumnFilterClickedEventArg e)
        {
            // размеры меню
            int widthTool = GetWhithColumn(e.ColumnIndex) + 50;
            if (widthTool < 110) widthTool = 110;

            columnIndex = e.ColumnIndex;

            textBoxCtrl.Clear();
            CheckCtrl.Items.Clear();
            // создаём элементы контекстного меню и переопределяем события
            textBoxCtrl.Size = new System.Drawing.Size(widthTool, 30);
            textBoxCtrl.TextChanged -= textBoxCtrl_TextChanged;
            textBoxCtrl.TextChanged += textBoxCtrl_TextChanged;

            DateTimeCtrl.Size = new System.Drawing.Size(widthTool, 30);
            DateTimeCtrl.Format = DateTimePickerFormat.Custom;
            DateTimeCtrl.CustomFormat = "dd.MM.yyyy";
            DateTimeCtrl.TextChanged -= DateTimeCtrl_TextChanged;
            DateTimeCtrl.TextChanged += DateTimeCtrl_TextChanged;

            CheckCtrl.ItemCheck -= CheckCtrl_ItemCheck;
            CheckCtrl.ItemCheck += CheckCtrl_ItemCheck;
            CheckCtrl.CheckOnClick = true;
            // получаем список сохранённых фильтров для CheckListBox
            GetChkFilter();

            CheckCtrl.MaximumSize = new System.Drawing.Size(widthTool, GetHeightTable() - 120);
            CheckCtrl.Size = new System.Drawing.Size(widthTool, (CheckCtrl.Items.Count + 1) * 18);

            ApplyButtonCtrl.Text = ButtonCtrlText;
            ApplyButtonCtrl.Size = new System.Drawing.Size(widthTool, 30);
            ApplyButtonCtrl.Click -= ApplyButtonCtrl_Click;
            ApplyButtonCtrl.Click += ApplyButtonCtrl_Click;

            ClearFilterCtrl.Text = ClearFilterCtrlText;
            ClearFilterCtrl.Size = new System.Drawing.Size(widthTool, 30);
            ClearFilterCtrl.Click -= ClearFilterCtrl_Click;
            ClearFilterCtrl.Click += ClearFilterCtrl_Click;
            // теперь заполняем контекстное меню
            popup.Items.Clear();
            popup.AutoSize = true;
            popup.Margin = Padding.Empty;
            popup.Padding = Padding.Empty;

            ToolStripControlHost host1 = new ToolStripControlHost(textBoxCtrl);
            host1.Margin = Padding.Empty;
            host1.Padding = Padding.Empty;
            host1.AutoSize = false;
            host1.Size = textBoxCtrl.Size;

            ToolStripControlHost host2 = new ToolStripControlHost(CheckCtrl);
            host2.Margin = Padding.Empty;
            host2.Padding = Padding.Empty;
            host2.AutoSize = false;
            host2.Size = CheckCtrl.Size;

            ToolStripControlHost host3 = new ToolStripControlHost(ApplyButtonCtrl);
            host3.Margin = Padding.Empty;
            host3.Padding = Padding.Empty;
            host3.AutoSize = false;
            host3.Size = ApplyButtonCtrl.Size;

            ToolStripControlHost host4 = new ToolStripControlHost(ClearFilterCtrl);
            host4.Margin = Padding.Empty;
            host4.Padding = Padding.Empty;
            host4.AutoSize = false;
            host4.Size = ClearFilterCtrl.Size;

            ToolStripControlHost host5 = new ToolStripControlHost(DateTimeCtrl);
            host5.Margin = Padding.Empty;
            host5.Padding = Padding.Empty;
            host5.AutoSize = false;
            host5.Size = DateTimeCtrl.Size;
            // если в столбце даты, то в качестве поля поиске DateTimePicker,
            // иначе TextBox
            switch (this.Columns[columnIndex].ValueType?.ToString())
            {
                case "System.DateTime":
                case "System.Nullable`1[System.DateTime]":
                    DateTimeCtrl.CustomFormat = this.Columns[columnIndex].InheritedStyle.Format;
                    popup.Items.Add(host5);
                    break;
                default:
                    popup.Items.Add(host1);
                    break;
            }
            popup.Items.Add(host2);
            popup.Items.Add(host3);
            popup.Items.Add(host4);

            popup.Show(this, e.ButtonRectangle.X, e.ButtonRectangle.Bottom);
            host2.Focus();
        }

        private int GetWhithColumn(int e)
        {
            return this.Columns[e].Width;
        }
        // быстрый поиск по тексту
        void textBoxCtrl_TextChanged(object sender, EventArgs e)
        {
            //(this.DataSource as DataTable).DefaultView.RowFilter = string.Format("convert([" + this.Columns[columnIndex].Name + "], 'System.String') LIKE '%{0}%'", textBoxCtrl.Text);
            (this.DataSource as BindingSource).Filter = string.Format("{0} LIKE '%{1}%'", this.Columns[columnIndex].DataPropertyName, textBoxCtrl.Text);
        }
        // быстрый поиск по дате
        void DateTimeCtrl_TextChanged(object sender, EventArgs e)
        {
            (this.DataSource as DataTable).DefaultView.RowFilter = string.Format("convert([" + this.Columns[columnIndex].Name + "], 'System.String') LIKE '%{0}%'", DateTimeCtrl.Text);
        }
        // первый CheckBox "выбрать всё", соответственно заполняем все
        // CheckBox'ы как первый
        void CheckCtrl_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == 0)
            {
                if (e.NewValue == CheckState.Checked)
                {
                    for (int i = 1; i < CheckCtrl.Items.Count; i++)
                        CheckCtrl.SetItemChecked(i, true);
                }
                else
                {
                    for (int i = 1; i < CheckCtrl.Items.Count; i++)
                        CheckCtrl.SetItemChecked(i, false);
                }
            }
        }
        // заполнение CheckListBox
        private void GetChkFilter()
        {
            List<FilterStatus> CheckList = new List<FilterStatus>();
            List<FilterStatus> CheckListSort = new List<FilterStatus>();
            // добавляем то уже сохранено
            foreach (FilterStatus val in Filter)
            {
                if (this.Columns[columnIndex].Name == val.columnName)
                {
                    CheckList.Add(new FilterStatus() { columnName = "", valueString = val.valueString, check = val.check });
                }
            }
            // и смотрим всё что осталось в колонке
            List<string> cells = GetDataColumns(columnIndex);
            foreach (string ValueCell in cells)
            {
                int index = CheckList.FindIndex(item => item.valueString == ValueCell);
                if (index == -1)
                {
                    CheckList.Add(new FilterStatus { valueString = ValueCell, check = true });
                }
            }
            // выводим в CheckedListBox
            CheckCtrl.Items.Add(CheckCtrlAllText, CheckState.Indeterminate);
            // сортировка
            // Int32 сортируется как 1,12,2,21
            // приведём сортировку к 1,2,12,21
            switch (this.Columns[columnIndex].ValueType?.ToString())
            {
                case "System.Int32":
                    CheckListSort = CheckList.OrderBy(x => Int32.Parse(x.valueString)).ToList();
                    foreach (FilterStatus val in CheckListSort)
                    {
                        if (val.check == true)
                            CheckCtrl.Items.Add(val.valueString, CheckState.Checked);
                        else
                            CheckCtrl.Items.Add(val.valueString, CheckState.Unchecked);
                    }
                    break;
                // даты тоже сортируем отдельно
                case "System.DateTime":
                case "System.Nullable`1[System.DateTime]":
                    CheckListSort = CheckList.OrderBy(x => string.IsNullOrEmpty(x.valueString) ? DateTime.MinValue : DateTime.Parse(x.valueString)).ToList();
                    string dateFormat = Columns[columnIndex].InheritedStyle.Format;
                    foreach (FilterStatus val in CheckListSort)
                    {
                        if (val.check == true)
                            CheckCtrl.Items.Add(string.IsNullOrEmpty(val.valueString) ? string.Empty : DateTime.Parse(val.valueString).ToString(dateFormat), CheckState.Checked);
                        else
                            CheckCtrl.Items.Add(DateTime.Parse(string.IsNullOrEmpty(val.valueString) ? string.Empty : val.valueString).ToString(dateFormat), CheckState.Unchecked);
                    }
                    break;
                // всё что осталось - просто текст
                default:
                    CheckListSort = CheckList.OrderBy(x => x.valueString).ToList();
                    foreach (FilterStatus val in CheckListSort)
                    {
                        if (val.check == true)
                            CheckCtrl.Items.Add(val.valueString, CheckState.Checked);
                        else
                            CheckCtrl.Items.Add(val.valueString, CheckState.Unchecked);
                    }
                    break;
            }
        }
        // получаем список значений в колонке, сортируем и убираем дубли
        private List<string> GetDataColumns(int e)
        {
            List<string> ValueCellList = new List<string>();
            string Value;

            foreach (DataGridViewRow row in this.Rows)
            {
                Value = (row.Cells[e]?.Value ?? string.Empty).ToString();

                if (!ValueCellList.Contains(Value))
                    ValueCellList.Add((row.Cells[e]?.Value ?? string.Empty).ToString());
            }
            return ValueCellList;
        }

        private int GetHeightTable()
        {
            return this?.Height ?? 0;
        }
        // кнопка применить фильтры
        void ApplyButtonCtrl_Click(object sender, EventArgs e)
        {
            StrFilter = "";
            SaveChkFilter();
            ApllyFilter();
            popup.Close();
        }
        // сохраняем состояние фильтров
        private void SaveChkFilter()
        {
            string col = this.Columns[columnIndex].Name;
            string propName = this.Columns[columnIndex].DataPropertyName;
            string itemChk;
            bool statChk;

            Filter.RemoveAll(x => x.columnName == col);

            for (int i = 1; i < CheckCtrl.Items.Count; i++)
            {
                itemChk = CheckCtrl.Items[i]?.ToString();
                statChk = CheckCtrl.GetItemChecked(i);
                Filter.Add(new FilterStatus() { columnName = col, dataPropertyName = propName, valueString = itemChk, check = statChk });
            }
        }
        // ну и показываем в табличке всё что насортировали
        private void ApllyFilter()
        {
            IEnumerable<string> dpList = Filter.GroupBy(g => g.dataPropertyName).Select(s => s.Key);
            foreach (string dp in dpList)
            {
                if (StrFilter.Trim().Length > 0)
                    StrFilter += " AND ";
                StrFilter += "(";
                string fltrOR = string.Empty;
                foreach (FilterStatus val in Filter.Where(w => w.dataPropertyName == dp))
                {
                    if (val.check)
                    {
                        if (fltrOR.Length == 0)
                        {
                            fltrOR += (val.dataPropertyName + " = '" + val.valueString + "' ");
                        }
                        else
                        {
                            fltrOR += (" OR " + val.dataPropertyName + " = '" + val.valueString + "' ");
                        }
                        //20170725
                        //if (StrFilter.Length == 0)
                        //{
                        //    StrFilter = StrFilter + (val.dataPropertyName + " NOT LIKE '" + val.valueString + "' ");
                        //}
                        //else
                        //{
                        //    StrFilter = StrFilter + (" AND " + val.dataPropertyName + " NOT LIKE '" + val.valueString + "' ");
                        //}
                        // Было
                        //if (StrFilter.Length == 0)
                        //{
                        //    StrFilter = StrFilter + ("[" + val.dataPropertyName + "] <> '" + val.valueString + "' ");
                        //}
                        //else
                        //{
                        //    StrFilter = StrFilter + (" AND [" + val.dataPropertyName + "] <> '" + val.valueString + "' ");
                        //}
                    }
                }
                StrFilter += string.Format("{0})", fltrOR.Trim());
            }
            //(this.DataSource as DataTable).DefaultView.RowFilter = StrFilter;
            BindingSource bs = ((BindingSource)this.DataSource);
            bool hasRaise = bs.RaiseListChangedEvents;
            if (hasRaise)
                bs.RaiseListChangedEvents = false;
            bs.RemoveFilter();
            if (StrFilter.Trim().Length > 0)
                bs.Filter = StrFilter;
            if (hasRaise)
            {
                bs.RaiseListChangedEvents = true;
                bs.ResetBindings(false);
            }
        }
        // кнопка очистить фильтры
        void ClearFilterCtrl_Click(object sender, EventArgs e)
        {
            Filter.Clear();
            StrFilter = "";
            ApllyFilter();
            popup.Close();
        }

        #endregion
    }
}
