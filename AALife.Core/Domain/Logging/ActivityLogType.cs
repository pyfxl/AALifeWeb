using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain.Logging
{
    /// <summary>
    /// Represents an activity log type record
    /// </summary>
    [Table("ActivityLogType")]
    public partial class ActivityLogType : BaseEntity
    {
        /// <summary>
        /// Gets or sets the system keyword
        /// </summary>
        [MaxLength(100)]
        public string SystemKeyword { get; set; }

        /// <summary>
        /// Gets or sets the display name
        /// </summary>
        [MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the activity log type is enabled
        /// </summary>
        public bool Enabled { get; set; }
    }
}
