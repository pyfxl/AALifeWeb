using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Data.Infrastructure.Kendoui
{
    public class GroupSelector<TElement>
    {
        public Func<TElement, object> Selector { get; set; }

        public string Field { get; set; }

        public IEnumerable<Aggregator> Aggregates { get; set; }
    }
}
