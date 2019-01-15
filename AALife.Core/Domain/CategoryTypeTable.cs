using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain
{
    [Table("tab_CategoryTypeTable")]
    public partial class CategoryTypeTable : BaseEntity
    {
        [Key]
        public int CategoryID { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string CategoryTypeName { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public int CategoryTypeID { get; set; }

        /// <summary>
        /// 可用否
        /// </summary>
        public byte CategoryTypeLive { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// 同步
        /// </summary>
        public byte Synchronize { get; set; }

        /// <summary>
        /// 预算金额
        /// </summary>
        public decimal CategoryTypePrice { get; set; }

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
