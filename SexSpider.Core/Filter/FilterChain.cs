using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SexSpider.Core.Filter
{
    public class FilterChain
    {
        private readonly IList<IFilter> list;

        public FilterChain()
        {
            this.list = new List<IFilter>();
        }

        public void AddFilter(IFilter filter)
        {
            this.list.Add(filter);
        }

        public string DoFilter(string str)
        {
            foreach(var filter in list)
            {
                str = filter.DoFilter(str);
            }

            return str;
        }
        
        public int Count()
        {
            return this.list.Count();
        }
    }
}
