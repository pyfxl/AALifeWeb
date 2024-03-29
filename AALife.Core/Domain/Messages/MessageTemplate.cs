﻿using System.ComponentModel.DataAnnotations;

namespace AALife.Core.Domain.Messages
{
    /// <summary>
    /// Represents a message template
    /// </summary>
    public partial class MessageTemplate : BaseEntity
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the template is active
        /// </summary>
        public bool IsActive { get; set; }

    }
}
