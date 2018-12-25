using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SexSpider.Core.Filter
{
    public interface IFilter
    {
        string DoFilter(string str);
    }
}
