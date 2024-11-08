using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TkwEF.BLL
{
    public  class Roles : BIZ
    {
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }

    }
}
