using System.Collections.Generic;

namespace AALife.Data.Infrastructure.Kendoui
{
    /// <summary>
    /// Describes a Kendo Datasource request.
    /// </summary>
    public class DataSourceRequest
    {
        /// <summary>
        /// Specifies how many items to page.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Specifies how many items to size.
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Specifies how many items to take.
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// Specifies how many items to skip.
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Specifies the requested sort order.
        /// </summary>
        public IEnumerable<Sort> Sort { get; set; }

        /// <summary>
        /// Specifies the requested grouping.
        /// </summary>
        public IEnumerable<Group> Group { get; set; }

        /// <summary>
        /// Specifies the requested filter.
        /// </summary>
        public Filter Filter { get; set; }

        /// <summary>
        /// Specifies the requested aggregators.
        /// </summary>
        public IEnumerable<Aggregator> Aggregate { get; set; }

    }
}
