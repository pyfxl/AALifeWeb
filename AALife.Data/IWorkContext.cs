using AALife.Core.Domain.Common;
using AALife.Data.Domain;

namespace AALife.Data
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

        /// <summary>
        /// user settings
        /// </summary>
        UserSettings UserSettings { get; set; }

        /// <summary>
        /// site settings
        /// </summary>
        SiteSettings SiteSettings { get; set; }

    }
}
