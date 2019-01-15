using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain
{
    [Table("tab_ZhuanTiTable")]
    public partial class ZhuanTiTable : BaseEntity
    {
        [Key]
        public int ZhuanTiID { get; set; }

        /// <summary>
        /// 专题名称
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string ZhuanTiName { get; set; }

        /// <summary>
        /// 专题图片
        /// </summary>
        [MaxLength(200)]
        public string ZhuanTiImage { get; set; }

        /// <summary>
        /// 可用否
        /// </summary>
        public byte ZhuanTiLive { get; set; }

        /// <summary>
        /// 同步
        /// </summary>
        public byte Synchronize { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// 同步ID
        /// </summary>
        public int? ZTID { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [JsonIgnore]
        public virtual UserTable User { get; set; }
    }
}
