using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TkwEF.BLL
{
    public partial class CompGraf : BIZ
    {
        #region Fields

        public int Nomer { get; set; }

        public int RedUserId { get; set; }
        public virtual CompUser RedUser { get; set; }

        public int? BlueUserId { get; set; }
        public virtual CompUser BlueUser { get; set; }
        
        public int GrafStatusId { get; set; }
        public virtual GrafStatus GrafStatus { get; set; }

        public DateTime? Begin { get; set; }
        public DateTime? End { get; set; }

        public int? PobeditelId { get; set; }
        public virtual CompUser Pobeditel { get; set; }
        [Required]
        [DefaultValue(0)]
        public int Etap { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool Actual { get; set; }

        #endregion

        #region ctor

        public CompGraf()
        {
            Actual = true;
        }

        #endregion
    }
}
