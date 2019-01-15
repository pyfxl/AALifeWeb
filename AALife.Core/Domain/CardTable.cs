using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain
{
    [Table("tab_CardTable")]
    public partial class CardTable : BaseEntity
    {
        [Key]
        public int CardID { get; set; }

        /// <summary>
        /// 卡名称
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string CardName { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [MaxLength(50)]
        public string CardNumber { get; set; }

        /// <summary>
        /// 卡图片
        /// </summary>
        [MaxLength(200)]
        public string CardImage { get; set; }

        /// <summary>
        /// 卡余额
        /// </summary>
        public decimal CardMoney { get; set; }

        /// <summary>
        /// 卡可用
        /// </summary>
        public byte CardLive { get; set; }

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
        public int CDID { get; set; }

        /// <summary>
        /// 钱包初始
        /// </summary>
        public decimal MoneyStart { get; set; }

        /// <summary>
        /// 首页显示
        /// </summary>
        public byte? CardShow { get; set; }

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
