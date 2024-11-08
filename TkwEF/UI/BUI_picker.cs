using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TkwEF.Core;
using TkwEF.Core.Extentions;

namespace TkwEF.UI
{
    public partial class BUI_picker : Form
    {
        #region Fields

        protected readonly Control _ctrlParam = null;
        protected readonly BLL.BIZ _dataParam;
        protected readonly IEnumerable<BLL.BIZ> _lstParam;
        protected readonly Point _pointParam;

        #endregion

        private BUI_picker() : base()
        {
            InitializeComponent();
        }
        public BUI_picker(Control sender) : this()
        {
            if (sender == null) throw new UiException("Неопределен контрол для формы отображения кнопок");
            _ctrlParam = sender;
            //this.Location = _getPoint(this);
        }
        public BUI_picker(Control sender, BLL.BIZ data) : this(sender)
        {
            _dataParam = data;
            //this.Location = _getPoint(this);
        }
        public BUI_picker(Control sender, IEnumerable<BLL.BIZ> lst) : this(sender)
        {
            _lstParam = lst;
        }
        public BUI_picker(Control sender, BLL.BIZ data, IEnumerable<BLL.BIZ> lst) : this(sender)
        {
            _dataParam = data;
            _lstParam = lst;
        }
        public BUI_picker(Point point, BLL.BIZ data, IEnumerable<BLL.BIZ> lst) : this()
        {
            _pointParam = point;
            _dataParam = data;
            _lstParam = lst;
        }

        #region Handled

        private void f_SmetaStroySprav_picker_Deactivate(object sender, EventArgs e)
        {
            _closeForm();
        }

        //protected void btnAll_Click(object sender, EventArgs e)
        //{
        //    Button btn = (Button)sender;
        //    btn.Enabled = false;
        //    Hide();
        //    Invalidate();
        //    Application.DoEvents();
        //    OnPickerClick(btn);
        //}

        #endregion

        #region Private function

        protected virtual Point _getPoint()
        {
            return this.GetPosition(_ctrlParam);
        }

        protected virtual void _closeForm()
        {
            Close();
        }

        #endregion

        #region Event

        public event EventHandler<UtilityCore.UtilsEvent.EventArgs<Button>> PickerClick;
        protected virtual void OnPickerClick(Button ctrl)
        {
            PickerClick?.Invoke(this, new UtilityCore.UtilsEvent.EventArgs<Button>(ctrl));
        }

        #endregion
    }
}
