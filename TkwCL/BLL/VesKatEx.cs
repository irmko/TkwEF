using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TkwEF.BLL
{
    public partial class VesKat
    {
        #region Public function

        public bool InRange(decimal val)
        {
            if (val >= BeginVes && val < EndVes)
                return true;
            return false;
        }

        #endregion
    }
}
