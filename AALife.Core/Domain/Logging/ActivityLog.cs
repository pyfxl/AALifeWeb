using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain.Logging
{
    /// <summary>
    /// Represents an activity log record
    /// </summary>
    [Table("ActivityLog")]
    public partial class ActivityLog : BaseEntity
    {
        /// <summary>
        /// Gets or sets the activity log type identifier
        /// </summary>
        public int ActivityLogTypeId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the activity comment
        /// </summary>
        [Required]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets the activity log type
        /// </summary>
        public virtual ActivityLogType ActivityLogType { get; set; }

        /// <summary>
        /// Gets the customer
        /// </summary>
        public virtual UserTable User { get; set; }

        /// <summary>
        /// Gets or sets the ip address
        /// </summary>
        [MaxLength(200)]
        public virtual string IpAddress { get; set; }
    }
}
