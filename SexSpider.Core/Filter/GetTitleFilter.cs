using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SexSpider.Core.Filter
{
    public class GetTitleFilter : IFilter
    {
        public string DoFilter(string str)
        {
            var dic = new Dictionary<string, string>();
            MatchCollection mc = Regex.Matches(str, @"([^\s=]+)=(['""\s]?)([^'""]+)\2(?=\s|$|>)");
            foreach(Match m in mc)
            {
                try
                {
                    dic.Add(m.Groups[1].Value, m.Groups[3].Value);
                }
                catch { }
            }

            return dic.Where(a => a.Key == "title" || a.Key == "alt").Select(a => a.Value).FirstOrDefault();
        }
    }
}
