using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TkwEF.Core
{
    public static class Utils<T>
    {
        public static List<T> GetControls(Form frm)
        {
            List<T> res = new List<T>();

            return res;
        }
        private static Control _getControl(Control c, T t)
        {
            if(c.GetType().Equals(t))
            {
                return c;
            }
            else if(c.HasChildren)
            {
                return _getControl(c, t);
            }
            else
            {
                if (c is T)
                    return c;
                return null;
            }
        }
    }
}
