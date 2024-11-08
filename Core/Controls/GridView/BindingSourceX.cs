using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TkwEF.Core.Controls
{
    public partial class BindingSourceX : System.Windows.Forms.BindingSource
    {
        public BindingSourceX()
        {
            InitializeComponent();
        }

        public BindingSourceX(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
