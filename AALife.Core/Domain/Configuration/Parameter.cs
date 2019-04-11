using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        /// 名称
        /// </summary>
        public string Name { get; set; }

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
        /// 默认值
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// 系统属性
        /// </summary>
        public bool? IsSystem { get; set; }

        /// <summary>
        /// 参数说明
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// 父亲
        /// </summary>
        [ForeignKey("ParentId")]
        [JsonIgnore]
        public virtual Parameter Parent { get; set; }

        /// <summary>
        /// 儿子
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Parameter> Children { get; set; }

    }
}
