using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Service.Model.Common
{
    public class FilterModel
    {
        public string field { get; set; }

        public string @operator { get; set; }

        public string value { get; set; }

        public string logic { get; set; }

        public List<FilterModel> filters { get; set; }
    }
}
