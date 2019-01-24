﻿using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain
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
        /// 类别Id
        /// </summary>
        public int? CategoryTypeId { get; set; }

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
        /// 商品AppId
        /// </summary>
        public int? ItemAppId { get; set; }

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
        /// 专题Id
        /// </summary>
        public int? ZhuanTiId { get; set; }

        /// <summary>
        /// 卡Id
        /// </summary>
        public int CardId { get; set; }

    }
}
