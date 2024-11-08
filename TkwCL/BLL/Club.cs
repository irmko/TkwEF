using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TkwEF.BLL
{
    public class Club : BIZ
    {
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Address { get; set; }
        [MaxLength(15)]
        public string Telefon { get; set; }
        [MaxLength(1000)]
        public string Email { get; set; }

        public virtual ICollection<UserClub> UserClubs { get; set; }
    }
}
