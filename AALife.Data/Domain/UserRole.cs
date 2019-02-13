using AALife.Core;
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
        [MaxLength(10)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 系统角色
        /// </summary>
        public bool IsSystemRole { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string SystemName { get; set; }

        /// <summary>
        /// 可用否
        /// </summary>
        public byte Live { get; set; }

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
        /// 用户列表
        /// </summary>
        public virtual ICollection<UserTable> UserTables { get; set; }

    }
}
