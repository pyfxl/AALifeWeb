using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SexSpider.Core.Filter
{
    public class NoSpaceFilter : IFilter
    {
        public string DoFilter(string str)
        {
            Regex reg = new Regex(@"(?n)(?<=(^|>)[^<>]*)\s(?=[^<>]*(<|$))");
            string result = reg.Replace(str, "").Replace("\n", "");

            return result;
        }
    }
}
