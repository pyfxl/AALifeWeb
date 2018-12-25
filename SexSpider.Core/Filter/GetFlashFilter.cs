using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SexSpider.Core.Filter
{
    public class GetFlashFilter : IFilter
    {
        public string DoFilter(string str)
        {
            try
            {
                //str = str.Replace("'", "\"");
                //str = Regex.Replace(str, @"<.+?>", "");

                var ePos = str.IndexOf(".m3u8");
                var sPos = str.IndexOf("video=");
                var result = str.Substring(sPos+6, ePos-sPos-1);
                return result;
            }
            catch { }

            return "";
        }
    }
}
