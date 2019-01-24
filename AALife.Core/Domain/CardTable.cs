using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain
{
    [Table("tab_CardTable")]
    public partial class CardTable : UserEntity
    {
        /// <summary>
        /// 同步Id
        /// </summary>
        [Required]
        public int CardId { get; set; }

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
        /// 钱包初始
        /// </summary>
        public decimal MoneyStart { get; set; }

    }
}
