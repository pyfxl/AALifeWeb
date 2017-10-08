using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.EF.ViewModel
{
    public class QueryModel
    {
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public string key { get; set; }

        public int? userId { get; set; }
    }
}
