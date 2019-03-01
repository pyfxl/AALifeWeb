using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Data.Infrastructure.Kendoui
{
    public class GroupResult
    {
        //small letter properties are kendo js properties so please execuse the warnings
        //for more info check http://docs.telerik.com/kendo-ui/api/javascript/data/datasource#configuration-schema.groups
        public object value { get; set; }

        //public string selectorField { get; set; }

        public string field { get; set; }

        public int count { get; set; }

        public IEnumerable<Aggregator> aggregates { get; set; }

        public dynamic items { get; set; }

        public bool hasSubgroups { get; set; } // true if there are subgroups

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.value, this.count);
        }
    }
}
