using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TkwEF.BLL
{
    public class Poyas : BIZ
    {
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }
        [Required]
        public int Order { get; set; }

        public virtual ICollection<CompUser> CompUsers { get; set; }
        public virtual ICollection<UserPoyas> UserPoyases { get; set; }

        #region override

        public override string ToString()
        {
            return string.Format("Пояс {0} ( Id = {1} )", Name, Id);
        }

        #endregion
    }
}
