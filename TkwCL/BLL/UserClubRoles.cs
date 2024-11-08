using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TkwEF.BLL
{
    public class UserClubRoles : BIZ
    {
        public int UserClubId { get; set; }
        [Required]
        public virtual UserClub UserClub { get; set; }

        public int RolesId { get; set; }
        [Required]
        public Roles Roles { get; set; }

        [Required]
        public DateTime DateBegin { get; set; }
        [Required]
        [DefaultValue(true)]
        public bool Actual { get; set; }
        [Required]
        [Timestamp]
        public byte[] Timestamp { get; set; }
    }
}
