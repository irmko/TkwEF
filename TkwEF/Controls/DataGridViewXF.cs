using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TkwEF.Core.Controls;

namespace TkwEF.Controls
{
    public partial class DataGridViewXF : DataGridViewXF_Base
    {
        public DataGridViewXF()
        {
            InitializeComponent();
        }

        public DataGridViewXF(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        //public new System.Windows.Forms.DataGridViewCellStyle AlternatingRowsDefaultCellStyle
        //{
        //    get
        //    {
        //        return base.AlternatingRowsDefaultCellStyle;
        //    }
        //    set
        //    {
        //        base.AlternatingRowsDefaultCellStyle = value;
        //    }
        //}
    }
}
