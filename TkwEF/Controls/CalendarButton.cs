using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TkwEF.Controls
{
    public partial class CalendarButton : UserControl
    {
        #region Fields

        private MonthCalendar monthCalendar = null;

        public DateTime? SelectedDate
        {
            get
            {
                if (monthCalendar == null)
                    return null;
                return monthCalendar.SelectionStart;
            }
        }

        #endregion

        //public CalendarButton()
        //{
        //    InitializeComponent();
        //}
        public CalendarButton(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (monthCalendar == null)
            {
                _initCalendar();
                monthCalendar.Show();
            }
            else
                monthCalendar = null;
        }

        private void _initCalendar()
        {
            this.monthCalendar = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar.Location = new System.Drawing.Point(0, 0);
            this.monthCalendar.Name = "monthCalendar1";
            this.monthCalendar.TabIndex = 1;
            this.monthCalendar.MaxSelectionCount = 1;
            this.monthCalendar.DateSelected += MonthCalendar_DateSelected;
            // 
            // CalendarButton
            // 
            this.Controls.Add(this.monthCalendar);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void MonthCalendar_DateSelected(object sender, DateRangeEventArgs e)
        {
            DateSelected?.Invoke(sender, e);
        }

        #region Events

        public event DateRangeEventHandler DateSelected;

        #endregion
    }
}
