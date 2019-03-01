using AALife.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Data.Domain
{
    [Table("tab_UserRole")]
    public partial class UserRole : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string SystemName { get; set; }

        /// <summary>
        /// 用户列表
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserTable> UserTables { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<PermissionRecord> PermissionRecords { get; set; }

    }
}
