using System;
using System.Runtime.Serialization;
using System.Windows.Forms;
using TkwEF.Core.Helper;

namespace TkwEF.UI
{
    public partial class f_BUI_Base : Form
    {
        public f_BUI_Base()
        {
            InitializeComponent();
        }

        #region Override

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            try
            {
                PropertiesXML.Load(1, this);
            }
            catch { }
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                PropertiesXML.Save(1, this);
            }
            catch { }
            base.OnFormClosed(e);
        }

        #endregion
    }
}
