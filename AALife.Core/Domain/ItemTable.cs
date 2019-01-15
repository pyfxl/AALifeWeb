using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain
{
    [Table("tab_ItemTable")]
    public partial class ItemTable : BaseEntity
    {
        [Key]
        public int ItemID { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string ItemName { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public int CategoryTypeID { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal ItemPrice { get; set; }

        /// <summary>
        /// 购买日期
        /// </summary>
        public DateTime ItemBuyDate { get; set; }

        /// <summary>
        /// 推荐否
        /// </summary>
        public byte? Recommend { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// 同步
        /// </summary>
        public byte Synchronize { get; set; }

        /// <summary>
        /// 商品AppID
        /// </summary>
        public int? ItemAppID { get; set; }

        /// <summary>
        /// 区间ID
        /// </summary>
        public int? RegionID { get; set; }

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
        /// 专题ID
        /// </summary>
        public int? ZhuanTiID { get; set; }

        /// <summary>
        /// 卡ID
        /// </summary>
        public int CardID { get; set; }

        /// <summary>
        /// 商品备注
        /// </summary>
        [MaxLength(100)]
        public string Remark { get; set; }
        
        /// <summary>
        /// 可用否
        /// </summary>
        public byte ItemLive { get; set; }
        
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
