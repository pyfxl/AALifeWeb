using AALife.WebMvc.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.Query
{
    public class BaseQuery
    {
        public int? userId { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public string keyWords { get; set; }
    }
}