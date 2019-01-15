using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain
{
    [Table("tab_ZhuanZhangTable")]
    public partial class ZhuanZhangTable : BaseEntity
    {
        [Key]
        public int ZhuanZhangID { get; set; }

        /// <summary>
        /// 转账来自
        /// </summary>
        public int ZhuanZhangFrom { get; set; }

        /// <summary>
        /// 转账给
        /// </summary>
        public int ZhuanZhangTo { get; set; }

        /// <summary>
        /// 转账日期
        /// </summary>
        public DateTime ZhuanZhangDate { get; set; }

        /// <summary>
        /// 转账金额
        /// </summary>
        public decimal ZhuanZhangMoney { get; set; }

        /// <summary>
        /// 可用否
        /// </summary>
        public byte ZhuanZhangLive { get; set; }

        /// <summary>
        /// 同步
        /// </summary>
        public byte Synchronize { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// 转账备注
        /// </summary>
        [MaxLength(100)]
        public string ZhuanZhangNote { get; set; }

        /// <summary>
        /// 同步ID
        /// </summary>
        public int? ZZID { get; set; }

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
