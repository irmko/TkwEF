using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TkwEF.BLL
{
    /// <summary>
    /// Соревнование
    /// </summary>
    public partial class Competition : BIZ
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        [Required]
        [DefaultValue("true")]
        public bool Actual { get; set; }

        public virtual List<CompUser> CompUsers { get; set; }
        public virtual List<CompVesKat> CompVesKats { get; set; }

        public Competition()
        {
            Actual = true;
        }
    }
}
