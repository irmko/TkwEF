using System;
using System.Drawing;
using System.Windows.Forms;

namespace TkwEF.UI.Graf
{
    public partial class f_Graf_pobeditel_select_dlg : f_BDUI
    {
        #region Fields

        private readonly Point _locParam;
        public int? ReturnValue { get; private set; }

        #endregion

        #region ctor

        private f_Graf_pobeditel_select_dlg() : base()
        {
            InitializeComponent();
        }

        public f_Graf_pobeditel_select_dlg(BLL.CompUser red, BLL.CompUser blue, Point point) : this()
        {
            StartPosition = FormStartPosition.Manual;
            _locParam = point;
            if (red != null)
            {
                this.rdbRed.Text = red?.FamIO;
                rdbRed.Tag = red?.Id;
                toolTip1.SetToolTip(rdbRed, red?.FIO);
            }
            else
            {
                rdbRed.Visible = false;
            }

            if (blue != null)
            {
                this.rdbBlue.Text = blue?.FamIO;
                rdbBlue.Tag = blue?.Id;
                toolTip1.SetToolTip(rdbBlue, blue?.FIO);
            }
            else
            {
                rdbBlue.Visible = false;
            }
        }

        #endregion

        #region Handled

        private void f_Graf_pobeditel_select_dlg_Load(object sender, EventArgs e)
        {
            _setBtnTextDef();
        }

        #endregion

        private void rdb_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rdb = (RadioButton)sender;
            if (rdb.Checked)
                btnOk.Text = rdb.Text;
            else
                _setBtnTextDef();
        }

        private void _setBtnTextDef()
        {
            btnOk.Text = "Выберите победителя";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            btnOk.Enabled = false;
            if(!rdbRed.Checked && !rdbBlue.Checked)
            {
                btnOk.Enabled = true;
                return;
            }
            ReturnValue = rdbRed.Checked ? (int)rdbRed.Tag : (int)rdbBlue.Tag;
            DialogResult = DialogResult.OK;
        }

        #region Private function

        #endregion

        #region override

        protected override void CreateHandle()
        {
            base.CreateHandle();
            Location = _locParam;
        }

        #endregion
    }
}
