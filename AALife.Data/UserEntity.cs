using AALife.Core;
using AALife.Data.Domain;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace AALife.Data
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class UserEntity : BaseEntity
    {
        /// <summary>
        /// 可用否
        /// </summary>
        public byte Live { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public byte? Rank { get; set; }

        /// <summary>
        /// 同步
        /// </summary>
        public byte Synchronize { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(100)]
        public string Remark { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [MaxLength(200)]
        public string Image { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        [JsonIgnore]
        public virtual UserTable User { get; set; }

    }

}
