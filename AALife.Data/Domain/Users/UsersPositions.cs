using AALife.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Data.Domain
{
    /// <summary>
    /// 用户岗位中间表
    /// </summary>
    [Table("usr_UsersPositions")]
    public partial class UsersPositions : BaseEntity<Guid>
    {
        /// <summary>
        /// 用户
        /// </summary>
        [JsonIgnore]
        public virtual UserTable User { get; set; }

        /// <summary>
        /// 岗位
        /// </summary>
        [JsonIgnore]
        public virtual UserPosition Position { get; set; }

        /// <summary>
        /// 是否主岗位
        /// </summary>
        public bool? IsMainPosition { get; set; }

        /// <summary>
        /// 是否负责人
        /// </summary>
        public bool? IsDeptmentLeader { get; set; }
    }

}
