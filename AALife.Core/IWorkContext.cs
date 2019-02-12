using AALife.Core.Domain.Configuration;
using AALife.Core.Domain.Customers;

namespace AALife.Core
{
    /// <summary>
    /// Work context
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// Gets or sets the current customer
        /// </summary>
        Customer CurrentCustomer { get; set; }

        /// <summary>
        /// Get or set value indicating whether we're in admin area
        /// </summary>
        bool IsAdmin { get; set; }

        /// <summary>
        /// site settings
        /// </summary>
        CommonSettings CommonSettings { get; set; }

    }
}
