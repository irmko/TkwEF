using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TkwEF.Core.Extentions;
using System.Globalization;
using System.Runtime.InteropServices;

namespace TkwEF.Core.Controls
{
    //[StructLayout(LayoutKind.Sequential)]
    [ToolboxBitmap(typeof(WatermarkTextBox), "ListView.ico")]
    public partial class WatermarkTextBoxNumeric : TextBox
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region ctor

        public WatermarkTextBoxNumeric()
        {

            InitializeComponent();

        }

        public WatermarkTextBoxNumeric(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

        }

        #endregion

        #region Fields

        #region Numeric

        private bool _allowNegative = false;
        [Description("Разрешено ли отрицательное число")]
        [DefaultValue(false)]
        public bool AllowNegative
        {
            get { return _allowNegative; }
            set { _allowNegative = value; }
        }

        private bool _allowPoint = false;
        [Description("Разрешено ли исполюзовать точку")]
        [DefaultValue(false)]
        public bool AllowPoint
        {
            get { return _allowPoint; }
            set { _allowPoint = value; }
        }

        private bool _allowFormated = true;
        [Description("Разрешено ли форматировать число с разделителями")]
        [DefaultValue(true)]
        public bool AllowFormated
        {
            get { return _allowFormated; }
            set { _allowFormated = value; }
        }
        /// <summary>
        /// Устанавливает или возвращает значение
        /// </summary>
        public decimal? Value
        {
            get
            {
                if (this.Text.IsNullOrEmpty())
                    return null;
                return decimal.Parse(this.Text.Trim(), NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowLeadingSign);
            }
            set
            {
                Text = _formated(value);
            }
        }

        private int _countAfterPoint = 2;
        /// <summary>
        /// Количество знаков после запятой
        /// </summary>
        [Description("Количество знаков после запятой")]
        [DefaultValue(2)]
        public int CountAfterPoint
        {
            get { return _countAfterPoint; }
            set { _countAfterPoint = value; }
        }

        private bool _cutValue = false;
        [Description("Нужно ли обрезать число, если количество десятичных знаков превысило значение из свойства CountAfterPoint")]
        [DefaultValue(false)]
        public bool CutValue
        {
            get { return _cutValue; }
            set { _cutValue = value; }
        }

        private bool _isSelectText = false;
        [Description("Нужно ли выделять текст при активации контрола")]
        [DefaultValue(false)]
        public bool IsSelectText
        {
            get { return _isSelectText; }
            set { _isSelectText = value; }
        }

        #endregion

        #region Watermark

        private string textWatermark = "- Введите число -";
        private Color watermarkColor = Color.Gray;
        private Color foreColor = SystemColors.WindowText;
        private bool empty;
        [Browsable(true)]
        [DefaultValue(typeof(Color), "WindowText")]
        public new Color ForeColor
        {
            get { return foreColor; }
            set
            {
                foreColor = value;
                if (!empty)
                    base.ForeColor = value;
            }
        }
        [Browsable(true)]
        [DefaultValue(typeof(Color), "Gray")]
        public Color WatermarkColor
        {
            get { return watermarkColor; }
            set
            {
                watermarkColor = value;
                if (empty)
                {
                    base.ForeColor = watermarkColor;
                }
            }
        }
        [Browsable(true)]
        [DefaultValue(typeof(string), "- Введите число -")]
        public string TextWatermark
        {
            get
            {
                return textWatermark;
            }
            set
            {
                textWatermark = value;
                if (base.Text.IsNullOrEmpty())
                {
                    empty = true;
                    base.Text = textWatermark;
                    base.ForeColor = watermarkColor;
                }
                Invalidate();
            }
        }
        public override string Text
        {
            get
            {
                if (empty)
                    return "";
                return base.Text;
            }
            set
            {
                if (value.IsNullOrEmpty())
                {
                    empty = true;
                    base.ForeColor = watermarkColor;
                    base.Text = textWatermark;
                }
                else
                {
                    empty = false;
                    decimal val;
                    value = value.Replace(',', '.');
                    //bool res = decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val); //20130823
                    if (!decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out val))
                        decimal.TryParse(value, out val);
                    if (AllowFormated)
                        value = val.ToString("#,##0.######");
                    else
                        value = value.ToString();
                    base.ForeColor = foreColor;
                    base.Text = value;
                }
            }
        }

        private void WatermarkTextBox_Enter(object sender, EventArgs e)
        {
            if (empty)
            {
                empty = false;
                base.ForeColor = foreColor;
                base.Text = "";
            }
        }
        private void WatermarkTextBox_Leave(object sender, EventArgs e)
        {
            if (base.Text == "" || base.Text.Equals(TextWatermark))
            {
                empty = true;
                base.ForeColor = watermarkColor;
                base.Text = textWatermark;
            }
            else
            {
                empty = false;
            }
        }
        private void WatermarkTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.IsCifiriInText(e, AllowPoint, CountAfterPoint, AllowNegative);
            this.IsReturnInText(e);
        }

        #endregion

        #endregion

        #region Private function

        private string _formated(decimal? val)
        {
            if (!val.HasValue)
                return string.Empty;
            else
            {
                if (_cutValue)
                {
                    string ch = val.Value.ToString();
                    int i = ch.IndexOf(',');
                    if (i >= 0)
                    {
                        int len = ch.Length - i - 1;
                        if (len > _countAfterPoint)
                        {
                            ch = ch.Substring(0, ch.Length - (len - _countAfterPoint));
                        }
                    }
                    return ch;
                }
                else
                    return val.Value.ToString(string.Format("0{0}", AllowPoint && CountAfterPoint > 0 ? ".".PadRight(CountAfterPoint + 1, '#') : ""));
            }
        }

        #endregion
    }
}
