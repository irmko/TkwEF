using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TkwEF.BLL;

namespace TkwEF.Controls
{
    public partial class PoyasCbx : Core.Controls.BaseCbx
    {
        #region ctor

        public PoyasCbx()
        {
            InitializeComponent();
        }

        public PoyasCbx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        #endregion

        #region Fields

        /// <summary>
        /// Получает или задает значение свойства
        /// </summary>
        [DefaultValue(0)]
        [Description("Получает или задает значение свойства")]
        public new int SelectedValue
        {
            get
            {
                if (!(base.SelectedValue is int))
                    return 0;
                return (int)base.SelectedValue;
            }
            set { base.SelectedValue = value; }
        }

        /// <summary>
        /// Получает или задает значение свойства
        /// </summary>
        [DefaultValue(0)]
        [Description("Получает или задает значение свойства")]
        public new BLL.Poyas SelectedItem
        {
            get
            {
                if (base.SelectedItem == null)
                    return null;
                return (BLL.Poyas)base.SelectedItem;
            }
            set
            {
                base.SelectedItem = value;
            }
        }

        #endregion

        #region Public function

        public void FillSrc()
        {
            IEnumerable<BLL.Poyas> src;
            using (BLL.TkwContext context = new BLL.TkwContext())
            {
                src = context.Poyases.OrderBy(o => o.Order).ToList();
            }
            FillSrc(src);
        }

        public void FillSrc(IEnumerable<Poyas> src)
        {
            List<Poyas> lst;
            if (!(src is List<Poyas>))
                lst = src.ToList();
            else
                lst = (List<Poyas>)src;
            if (AllowFirstRow && !string.IsNullOrEmpty(FirstRowText))
            {
                lst.Insert(0, new Poyas { Name = FirstRowText, Id = 0, Order = -1 });
            }
            if (this.DisplayMember == "Id")
                this.DisplayMember = "Name";
            ValueMember = "Id";
            this.DataSource = lst;
        }

        #endregion
    }
}
