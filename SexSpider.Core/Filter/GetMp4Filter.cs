using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SexSpider.Core.Filter
{
    public class GetMp4Filter : IFilter
    {
        public string DoFilter(string str)
        {
            try
            {
                str = str.Replace("'", "\"");
                str = Regex.Replace(str, @"<.+?>", "");

                var pos = str.IndexOf(".mp4");
                var fStr = str.Substring(0, pos);
                var eStr = str.Substring(pos);
                var fResult = fStr.Substring(fStr.LastIndexOf("\"") + 1);
                var eResult = eStr.Substring(0, eStr.IndexOf("\""));
                return fResult.Replace("\\", "") + eResult;
            }
            catch { }

            return "";
        }
    }
}
