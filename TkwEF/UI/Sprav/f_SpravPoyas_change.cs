using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TkwEF.Core.Extentions;
using TkwEF.BLL;

namespace TkwEF.UI.Sprav
{
    public partial class f_SpravPoyas_change : Form
    {
        #region Fields

        private readonly BLL.Poyas _dataParam;
        private readonly IEnumerable<BLL.Poyas> _lstParam;
        public Poyas Poyas { get { return cbxPoyas.SelectedItem; } }

        #endregion

        //public f_SpravPoyas_change(Point point, BLL.Poyas poyas, IEnumerable<BLL.Poyas> src) : base()
        public f_SpravPoyas_change(Point point, Poyas poyas, IEnumerable<Poyas> src) : base()
        {
            InitializeComponent();
            if (point != null && point != Point.Empty)
                this.Location = point;
            _dataParam = poyas;
            _lstParam = src;
        }

        private void f_SpravPoyas_change_Load(object sender, System.EventArgs e)
        {
            cbxPoyas.FillSrc((List<Poyas>)_lstParam);
            cbxPoyas.SelectedItem = (Poyas)_dataParam;
        }

        private void f_SpravPoyas_change_Shown(object sender, System.EventArgs e)
        {
            cbxPoyas.DroppedDown = true;
        }
    }
}
