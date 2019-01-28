using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Data.Domain
{
    [Table("tab_CardTable")]
    public partial class CardTable : UserEntity
    {
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
        public decimal? MoneyStart { get; set; }

        /// <summary>
        /// 同步Id
        /// </summary>
        public int? CardId { get; set; }
    }
}
