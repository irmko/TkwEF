using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using TkwEF.BLL;

namespace TkwEF.Controls
{
    public partial class ClubsCbx : TkwEF.Core.Controls.BaseCbx
    {
        #region ctor

        public ClubsCbx()
        {
            InitializeComponent();
        }

        public ClubsCbx(IContainer container)
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
        public new BLL.Club SelectedItem
        {
            get
            {
                if (base.SelectedItem == null)
                    return null;
                return (BLL.Club)base.SelectedItem;
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
            IEnumerable<BLL.Club> src;
            using (BLL.TkwContext context = new BLL.TkwContext())
            {
                context.Clubs.Load();
                src = context.Clubs.OrderBy(o=>o.Name).ToList();
            }
            _fillSrc(src);
        }
        public async Task FillSrcAsync()
        {
            await Task.Run(() => FillSrc());
        }

        #endregion

        private void _fillSrc(IEnumerable<Club> src)
        {
            List<Club> lst;
            if (!(src is List<Club>))
                lst = src.ToList();
            else
                lst = (List<Club>)src;
            if(AllowFirstRow && !string.IsNullOrEmpty(FirstRowText))
            {
                lst.Insert(0, new Club { Name = FirstRowText, Id = 0 });
            }
            this.DataSource = lst;
        }
    }
}
