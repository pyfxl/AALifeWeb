using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Data.Domain
{
    [Table("tab_ZhuanZhangTable")]
    public partial class ZhuanZhangTable : UserEntity
    {
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
        /// 同步Id
        /// </summary>
        public int? ZhuanZhangId { get; set; }
    }
}
