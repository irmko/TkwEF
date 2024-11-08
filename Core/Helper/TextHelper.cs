using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TkwEF.Core.Helper
{
    public static class TextHelper
    {
        public static string EncodeTextToHtml(this string content)
        {
            //content = HttpUtility.HtmlEncode(content);
            //20170621
            //content = content.Replace(" ", "&nbsp;&nbsp;").Replace("\n", "<br>");
            content = content.Replace(": ", ":&nbsp;").Replace("\n", "<br />");
            return content;
        }
    }
}
