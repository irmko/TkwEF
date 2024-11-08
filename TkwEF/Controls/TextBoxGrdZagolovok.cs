using System.ComponentModel;
using System.Windows.Forms;

namespace TkwEF.Controls
{
    public partial class TextBoxGrdZagolovok : TextBox
    {
        public TextBoxGrdZagolovok()
        {
            InitializeComponent();
        }

        public TextBoxGrdZagolovok(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
