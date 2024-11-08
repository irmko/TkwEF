using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TkwEF.UI
{
    public partial class f_BUI : TkwEF.UI.f_BUI_Base
    {
        protected internal BLL.TkwContext context = new BLL.TkwContext();

        public f_BUI()
        {
            InitializeComponent();
        }

        ~f_BUI()
        {
            context.Dispose();
            context = null;
        }
    }
}
