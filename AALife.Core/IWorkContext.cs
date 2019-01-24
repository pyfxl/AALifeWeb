
using AALife.Core.Domain;

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
        UserTable CurrentUser { get; set; }

        /// <summary>
        /// Get or set value indicating whether we're in admin area
        /// </summary>
        bool IsAdmin { get; set; }
    }
}
