using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TkwEF.BLL
{
    public class AgeKat : BIZ
    {
        [Required]
        public int BeginAge { get; set; }
        [Required]
        public int EndAge { get; set; }
        
        #region override

        public override string ToString()
        {
            return $"Id={Id}, Begin={BeginAge}, End={EndAge}";
        }

        #endregion

        #region Public function

        public bool InRange(decimal val)
        {
            if (val >= BeginAge && val < EndAge)
                return true;
            return false;
        }

        #endregion
    }
}
