using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TkwEF.BLL
{
    public class UserClub : BIZ
    {
        #region Fields
        
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ClubId { get; set; }
        public virtual Club Club { get; set; }
        
        [Required]
        public DateTime Begin { get; set; }
        public DateTime? End { get; set; }

        #region ByGrid

        [NotMapped]
        public string FIO { get { return User?.FIO ?? string.Empty; } }
        [NotMapped]
        public string FamIO { get { return User?.FamIO ?? string.Empty; } }
        [NotMapped]
        public int? AgeUser => User?.Age;
        public Poyas CurPoyas => User?.CurrentPoyas;
        public string CurPoyasName => User?.CurPoyasName;

        #endregion

        #endregion
    }
}
