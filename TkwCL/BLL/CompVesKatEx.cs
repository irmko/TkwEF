using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TkwEF.BLL
{
    public partial class CompVesKat
    {
        #region Public Function

        /// <summary>
        /// Входит в диапазон весовой категории
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool InRange(decimal val)
        {
            return VesKat.InRange(val);
        }

        #endregion
    }
}
