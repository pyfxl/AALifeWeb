using AALife.Core;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Data.Domain
{
    [Table("tab_OAuthTable")]
    public partial class OAuthTable : BaseEntity
    {
        /// <summary>
        /// OpenId
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string OpenId { get; set; }

        /// <summary>
        /// AccessToken
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string AccessToken { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 旧用户Id
        /// </summary>
        public int OldUserId { get; set; }

        /// <summary>
        /// 绑定否
        /// </summary>
        public int OAuthBound { get; set; }

        /// <summary>
        /// 来自
        /// </summary>
        [MaxLength(10)]
        public string OAuthFrom { get; set; }

    }
}
