using AALife.Core;
using System.ComponentModel.DataAnnotations;

namespace AALife.Data.Domain.Messages
{
    /// <summary>
    /// Represents a message template
    /// </summary>
    public partial class MessageTemplate : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the subject
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body
        /// </summary>
        [Required]
        [MaxLength(1000)]
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the template is active
        /// </summary>
        public bool IsActive { get; set; }

    }
}
