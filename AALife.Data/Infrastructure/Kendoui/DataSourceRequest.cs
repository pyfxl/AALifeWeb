using System.Collections.Generic;

namespace AALife.Data.Infrastructure.Kendoui
{
    public class DataSourceRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public Filter Filter { get; set; }

        public IEnumerable<Sort> Sort { get; set; }

        public DataSourceRequest()
        {
            this.Page = 1;
            this.PageSize = 50;
        }
    }
}
