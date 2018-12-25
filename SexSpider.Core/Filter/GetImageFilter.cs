using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SexSpider.Core.Filter
{
    public class GetImageFilter : IFilter
    {
        public string DoFilter(string str)
        {
            var dic = new Dictionary<string, string>();
            MatchCollection mc = Regex.Matches(str, @"([^\s=]+)=(['""\s]?)([^'""]+)\2(?=\s|$|>)");
            foreach (Match m in mc)
            {
                try
                {
                    dic.Add(m.Groups[1].Value, m.Groups[3].Value);
                }
                catch { }
            }

            var attrs = new List<string> { "file", "data-original", "data-src", "zoomfile", "src" };

            foreach (var s in attrs)
            {
                if (dic.ContainsKey(s)) return dic[s];
            }

            return "";
        }
        
    }
}
