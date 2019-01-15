
using AALife.Core.Configuration;

namespace AALife.Core.Domain.Common
{
    public class AdminAreaSettings : ISettings
    {
        /// <summary>
        /// Default grid page size
        /// </summary>
        public int DefaultGridPageSize { get; set; }

        /// <summary>
        /// A comma-separated list of available grid page sizes
        /// </summary>
        public string GridPageSizes { get; set; }

        public bool StoreClosed { get; set; }
        
        public string PageTitleSeparator { get; set; }

        public string DefaultTitle { get; set; }

        public string DefaultMetaKeywords { get; set; }

        public string DefaultMetaDescription { get; set; }

        public string StoreUrl { get; set; }
    }
}