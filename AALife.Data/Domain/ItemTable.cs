using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Data.Domain
{
    [Table("tab_ItemTable")]
    public partial class ItemTable : UserEntity
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string ItemName { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal ItemPrice { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime ItemBuyDate { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public int? ItemAppId { get; set; }

        /// <summary>
        /// 推荐否
        /// </summary>
        public byte? Recommend { get; set; }

        /// <summary>
        /// 区间Id
        /// </summary>
        public int? RegionId { get; set; }

        /// <summary>
        /// 区间类型
        /// </summary>
        [MaxLength(10)]
        public string RegionType { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        [MaxLength(10)]
        [Required]
        public string ItemType { get; set; }

        /// <summary>
        /// 类别同步Id
        /// </summary>
        public int? CategoryTypeSyncId { get; set; }

        /// <summary>
        /// 专题同步Id
        /// </summary>
        public int? ZhuanTiSyncId { get; set; }

        /// <summary>
        /// 钱包同步Id
        /// </summary>
        public int? CardSyncId { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public int? CategoryTypeId { get; set; }

        [JsonIgnore]
        [ForeignKey("CategoryTypeId")]
        public virtual CategoryTypeTable CategoryTypeTable { get; set; }

        /// <summary>
        /// 专题
        /// </summary>
        public int? ZhuanTiId { get; set; }

        [JsonIgnore]
        [ForeignKey("ZhuanTiId")]
        public virtual ZhuanTiTable ZhuanTiTable { get; set; }

        /// <summary>
        /// 钱包
        /// </summary>
        public int? CardId { get; set; }

        [JsonIgnore]
        [ForeignKey("CardId")]
        public virtual CardTable CardTable { get; set; }

    }
}
