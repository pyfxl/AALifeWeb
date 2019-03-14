using AALife.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class ParameterViewModel : BaseViewModel
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 键
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 叶子节点
        /// </summary>
        public bool? IsLeaf { get; set; }

        /// <summary>
        /// 默认
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public byte? Rank { get; set; }

        /// <summary>
        /// 排序字符
        /// </summary>
        public string OrderNo { get; set; }

    }
}
