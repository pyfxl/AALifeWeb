using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Data.Infrastructure.Kendoui
{
    public class Group : Sort
    {
        public IEnumerable<Aggregator> Aggregates { get; set; }
    }
}
