using System;

namespace AALife.WebMvc.Models.Query
{
    public class BaseQuery
    {
        public Guid? userId { get; set; }

        public DateTime? startDate { get; set; }

        public DateTime? endDate { get; set; }

        public string keyWords { get; set; }
    }
}