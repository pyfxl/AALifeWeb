using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SexSpider.Core.Filter
{
    public class GetJpgFilter : IFilter
    {
        public string DoFilter(string str)
        {
            // ?后面所有内容
            return Regex.Replace(str, @"\?.*?([^""]+)", "");

            //try
            //{
            //    str = str.Replace("'", "\"");

            //    var pos = str.IndexOf(".jpg");
            //    var fStr = str.Substring(0, pos);
            //    var result = fStr.Substring(fStr.LastIndexOf("\"") + 1);
            //    return result.Replace("\\", "") + ".jpg";
            //}
            //catch { }

            //return "";
        }
    }
}
