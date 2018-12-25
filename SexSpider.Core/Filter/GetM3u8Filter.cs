using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SexSpider.Core.Filter
{
    public class GetM3u8Filter : IFilter
    {
        public string DoFilter(string str)
        {
            try
            {
                str = str.Replace("'", "\"");
                str = Regex.Replace(str, @"<.+?>", "");

                var pos = str.IndexOf(".m3u8");
                var fStr = str.Substring(0, pos);
                var result = fStr.Substring(fStr.LastIndexOf("\"") + 1);
                return result.Replace("\\", "") + ".m3u8";
            }
            catch { }

            return "";
        }
    }
}
