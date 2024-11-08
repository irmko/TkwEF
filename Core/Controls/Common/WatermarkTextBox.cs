using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TkwEF.Core.Extentions;

namespace TkwEF.Core.Controls
{
    [ToolboxBitmap(typeof(WatermarkTextBox), "ListView.ico")]
    public partial class WatermarkTextBox : TextBox
    {
        #region ctor

        public WatermarkTextBox()
        {
            InitializeComponent();
        }

        public WatermarkTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        private string watermarkText = "- Введите значение -";
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
        [DefaultValue(typeof(string), "- Введите значение -")]
        public string WatermarkText
        {
            get
            {
                return watermarkText;
            }
            set
            {
                watermarkText = value;
                if (base.Text.Length == 0)
                {
                    empty = true;
                    base.Text = watermarkText;
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
                if (value == "")
                {
                    empty = true;
                    base.ForeColor = watermarkColor;
                    base.Text = watermarkText;
                }
                else
                {
                    empty = false;
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
        private void WatermarkTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.IsReturnInText(e);
        }
        private void WatermarkTextBox_Leave(object sender, EventArgs e)
        {
            if (base.Text == "" || base.Text.Equals(watermarkText))
            {
                empty = true;
                base.ForeColor = watermarkColor;
                base.Text = watermarkText;
            }
            else
            {
                empty = false;
            }
        }
        protected override void DefWndProc(ref Message m)
        {
            //int MY_TEST = new IntPtr(13).ToInt32();
            base.DefWndProc(ref m);
            //if(m.Msg == MY_TEST && m.LParam == )
            //{
            //    if (base.Text == "" || base.Text.Equals(watermarkText))
            //    {
            //        empty = true;
            //        base.ForeColor = watermarkColor;
            //        base.Text = watermarkText;
            //    }
            //    else
            //    {
            //        empty = false;
            //    }
            //}
        }
    }
}
