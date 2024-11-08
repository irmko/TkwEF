using System;
using System.ComponentModel;
using System.Windows.Forms;
using TkwEF.Core.Extentions;

namespace TkwEF.Core.Controls
{
    [DefaultEvent("SelectedValueChanged")]
    public partial class BaseCbx : ComboBox
    {
        #region ctor

        public BaseCbx()
        {
            InitializeComponent();
        }

        public BaseCbx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region Fields

        /// <summary>
        /// Получает значение выбранного ID
        /// </summary>
        [DefaultValue(0)]
        [Description("Получает значение выбранного ID")]
        public int ID
        {
            get
            {
                int result;
                if (int.TryParse(this.SelectedValue.ToString(), out result))
                    return result;
                return 0;
            }
        }

        private bool _adjustWidthDropDown = true;
        [DefaultValue(true)]
        [Description("Нужно ли подгонять ширину выпадающего списка под максимальено длинную строки")]
        public bool AdjustWidthDropDown
        {
            get { return _adjustWidthDropDown; }
            set { _adjustWidthDropDown = value; }
        }

        private bool _allowFirstRow = true;
        /// <summary>
        /// Выводить первую дополнительную строку
        /// </summary>
        [DefaultValue(true)]
        [Description("Выводить первую дополнительную строку")]
        public bool AllowFirstRow
        {
            get { return _allowFirstRow; }
            set { _allowFirstRow = value; }
        }

        [DefaultValue(ComboBoxStyle.DropDownList)]
        [Description("Возвращает или задает значение, указывающее стиль поля со списком")]
        public new ComboBoxStyle DropDownStyle
        {
            get { return base.DropDownStyle; }
            set { base.DropDownStyle = value; }
        }

        private string _firstRowText = "- Выберите значение -";
        [DefaultValue("- Выберите значение -")]
        [Description("Текст, выводимый в первой строке")]
        public string FirstRowText
        {
            get { return _firstRowText; }
            set { _firstRowText = value; }
        }

        ///// <summary>
        ///// Получает значение выбранного ID
        ///// </summary>
        //[DefaultValue(0)]
        //[Description("Получает значение выбранного ID")]
        //public abstract int ID { get; set; }

        private int _maxWidthDropDown;
        [DefaultValue(0)]
        [Description("Максимальная ширина допустимая при подгонке ширины выпадающего списка (AdjustWidthDropDown). Если значение 0, то ограничение не действует")]
        public int MaxWidthDropDown
        {
            get { return _maxWidthDropDown; }
            set { _maxWidthDropDown = value; }
        }

        //private bool _needGoNextControl;
        ///// <summary>
        ///// Нужно ли после выбора значения переводить фокус на следующий контрол после выбора значения из списка
        ///// </summary>
        //[DefaultValue(false)]
        //[Description("Нужно ли после выбора значения переводить фокус на следующий контрол после выбора значения из списка")]
        //public bool NeedGoNextControl
        //{
        //    get { return _needGoNextControl; }
        //    set { _needGoNextControl = value; }
        //}

        private Control _nextControl;
        /// <summary>
        /// Контрол для фокусировки после изменения значения
        /// </summary>
        [DefaultValue(false)]
        [Description("Контрол для фокусировки после изменения значения")]
        public Control NextControl
        {
            get { return _nextControl; }
            set { _nextControl = value; }
        }

        //private ComboBoxStyle _dropDownStyle = ComboBoxStyle.DropDownList;
        //[DefaultValue(ComboBoxStyle.DropDownList)]
        //[RefreshProperties(RefreshProperties.Repaint)]
        //public new ComboBoxStyle DropDownStyle { get { return _dropDownStyle; } set { _dropDownStyle = value; } }

        #endregion

        //#region Public Abstract

        //protected abstract void _fillSrc();

        //#endregion

        #region Override

        protected override void OnDropDown(EventArgs e)
        {
            this.SetDropDownWidth(this.AdjustWidthDropDown, this.MaxWidthDropDown, 0);
            base.OnDropDown(e);
        }

        protected override void OnSelectedValueChanged(EventArgs e)
        {
            base.OnSelectedValueChanged(e);
            if (NextControl != null)
                NextControl.Focus();
        }

        #endregion

        private void cbx_DropDown(object sender, EventArgs e)
        {
            this.SetDropDownWidth(this.AdjustWidthDropDown, this.MaxWidthDropDown, 0);
        }
    }
}
