using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Domain.Configuration
{
    /// <summary>
    /// 系统参数
    /// </summary>
    public partial class Parameter : OrderEntity
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
        public int ParentId { get; set; }

        /// <summary>
        /// 叶子节点
        /// </summary>
        public bool? IsLeaf { get; set; }

        /// <summary>
        /// 默认
        /// </summary>
        public bool? IsDefault { get; set; }

    }
}
