using System;
using System.Windows.Forms;

namespace TkwEF.UI
{
    public partial class f_BDUI : TkwEF.UI.f_BUI_Base
    {
        public f_BDUI() : base()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!this.Modal)
                Close();
        }
    }
}
