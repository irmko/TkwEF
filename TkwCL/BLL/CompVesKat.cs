using System.ComponentModel.DataAnnotations;

namespace TkwEF.BLL
{
    public partial class CompVesKat : BIZ
    {
        public int CompId { get; set; }
        [Required]
        public virtual Competition Comp { get; set; }

        public int VesKatId { get; set; }
        [Required]
        public virtual VesKat VesKat { get; set; }
        [Required]
        public bool Actual { get; set; }

        public int? BeginVes { get { return VesKat?.BeginVes; } }
        public int? EndVes { get { return VesKat?.EndVes; } }

        public CompVesKat()
        {
            Actual = true;
        }

        #region override

        public override string ToString()
        {
            return string.Format("{0}-{1}", (BeginVes ?? 0), (EndVes ?? 0));
        }

        #endregion
    }
}
