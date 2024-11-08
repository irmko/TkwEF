using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TkwEF.Core.Extentions;

namespace TkwEF.BLL
{
    /// <summary>
    /// Участники соревнования
    /// </summary>
    public partial class CompUser : BIZ
    {
        #region Fields

        [Required]
        public int CompId { get; set; }
        public virtual Competition Comp { get; set; }

        [Required]
        public int UserClubId { get; set; }
        public virtual UserClub UserClub { get; set; }

        public decimal? Ves { get; set; }

        public int? UserPoyasId { get; set; }
        public virtual UserPoyas UserPoyas { get; set; }

        [Required]
        [DefaultValue("true")]
        public bool Actual { get; set; }

        #region ByGrid

        [NotMapped]
        public string FamIO => this.UserClub?.User?.FamIO ?? string.Empty;
        [NotMapped]
        public string FIO => this.UserClub?.User?.FIO ?? string.Empty;
        [NotMapped]
        public int? AgeUser => this.UserClub?.User?.Age;
        public string ClubName => this.UserClub?.Club?.Name ?? string.Empty;

        private CompVesKat compVesKat = null;
        public string VesToString
        {
            get
            {
                if (compVesKat == null)
                    compVesKat = GetVesKat();
                if (compVesKat == null)
                    return string.Empty;
                return compVesKat.ToString();
            }
        }
        public Poyas CurPoyas => UserClub?.CurPoyas;
        public string CurPoyasName => UserClub?.CurPoyasName;

        #endregion

        #endregion

        #region ctor

        public CompUser()
        {
            Actual = true;
        }

        #endregion

        #region Public

        public CompVesKat GetVesKat()
        {
            if (!this.Ves.HasValue)
                return null;
            return this.Comp.CompVesKats.FirstOrDefault(w => w.Actual && this.Ves.Value >= w.BeginVes && this.Ves.Value < w.EndVes);
        }

        #endregion
    }
}
