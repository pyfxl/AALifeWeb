using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SexSpider.Core.Filter
{
    public class GetSpanFilter : IFilter
    {
        public string DoFilter(string str)
        {
            str = Regex.Replace(str, "<span[^>]*>", "");
            str = Regex.Replace(str, "</span>", "");

            return str;
        }
    }
}
