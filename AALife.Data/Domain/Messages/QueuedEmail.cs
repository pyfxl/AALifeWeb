using AALife.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace AALife.Data.Domain.Messages
{
    /// <summary>
    /// Represents an email item
    /// </summary>
    public partial class QueuedEmail : BaseEntity
    {
        /// <summary>
        /// Gets or sets the From property (email address)
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string From { get; set; }

        /// <summary>
        /// Gets or sets the FromName property
        /// </summary>
        [MaxLength(20)]
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets the To property (email address)
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the ToName property
        /// </summary>
        [MaxLength(20)]
        public string ToName { get; set; }

        /// <summary>
        /// Gets or sets the subject
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body
        /// </summary>
        [MaxLength(1000)]
        [Required]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the date and time of item creation in UTC
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the sent date and time
        /// </summary>
        public DateTime? SentDate { get; set; }

    }
}
