using System;

namespace AALife.Core.Domain.Media
{
    /// <summary>
    /// Represents a picture
    /// </summary>
    public partial class Picture : BaseEntity
    {
        /// <summary>
        /// Gets or sets the picture mime type
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the picture is new
        /// </summary>
        public bool IsNew { get; set; }

        /// <summary>
        /// file name
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// file ext name
        /// </summary>
        public string FileExtName { get; set; }

        /// <summary>
        /// file size
        /// </summary>
        public int FileBytes { get; set; }

        /// <summary>
        /// update date
        /// </summary>
        public DateTime UploadDate { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int UserId { get; set; }

    }
}
