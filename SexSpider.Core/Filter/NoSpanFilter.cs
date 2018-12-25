using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SexSpider.Core.Filter
{
    public class NoSpanFilter : IFilter
    {
        public string DoFilter(string str)
        {
            return Regex.Replace(str, "<span[^>]*>.*</span>", "");
        }
    }
}
