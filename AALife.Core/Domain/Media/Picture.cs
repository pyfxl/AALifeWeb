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
        /// �ļ���
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// �ļ���չ��
        /// </summary>
        public string FileExtName { get; set; }

        /// <summary>
        /// �ļ���С
        /// </summary>
        public int FileBytes { get; set; }

        /// <summary>
        /// �ϴ�����
        /// </summary>
        public DateTime UploadDate { get; set; }

        /// <summary>
        /// �û�Id
        /// </summary>
        public int UserId { get; set; }
    }
}
