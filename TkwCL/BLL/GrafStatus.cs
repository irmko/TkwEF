using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TkwEF.BLL
{
    public class GrafStatus : BIZ
    {
        public enum Statuses { New = 1, Accept };

        [Required]
        public string Name { get; set; }
        /// <summary>
        /// Последовательность
        /// </summary>
        [Required]
        public int Order { get; set; }

        public virtual ICollection<CompGraf> CompGrafs { get; set; }
    }
}
