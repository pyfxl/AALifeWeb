using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain
{
    [Table("tab_OAuthTable")]
    public partial class OAuthTable : BaseEntity
    {
        [Key]
        public int OAuthID { get; set; }

        /// <summary>
        /// OpenID
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string OpenID { get; set; }

        /// <summary>
        /// AccessToken
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string AccessToken { get; set; }

        /// <summary>
        /// 旧用户ID
        /// </summary>
        public int OldUserID { get; set; }

        /// <summary>
        /// 绑定否
        /// </summary>
        public int OAuthBound { get; set; }

        /// <summary>
        /// 来自
        /// </summary>
        [MaxLength(10)]
        public string OAuthFrom { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual UserTable User { get; set; }

    }
}
