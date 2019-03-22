using AALife.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Data.Domain
{
    /// <summary>
    /// Represents a permission record
    /// </summary>
    [Table("tab_UserPermission")]
    public partial class UserPermission : OrderEntity
    {
        /// <summary>
        /// 父Id
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [MaxLength(20)]
        public string AreaName { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [MaxLength(20)]
        public string ControllerName { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        [MaxLength(20)]
        public string ActionName { get; set; }

        /// <summary>
        /// Gets or sets the permission name
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 角色列表
        /// </summary>
        private ICollection<UserRole> _userRoles;

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        public virtual ICollection<UserRole> UserRoles
        {
            get { return _userRoles ?? (_userRoles = new List<UserRole>()); }
            protected set { _userRoles = value; }
        }

        /// <summary>
        /// 部门列表
        /// </summary>
        private ICollection<UserDeptment> _userDeptments;

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        public virtual ICollection<UserDeptment> UserDeptments
        {
            get { return _userDeptments ?? (_userDeptments = new List<UserDeptment>()); }
            protected set { _userDeptments = value; }
        }

        /// <summary>
        /// 父角色
        /// </summary>
        [ForeignKey("ParentId")]
        [JsonIgnore]
        public virtual UserPermission Parent { get; set; }

        /// <summary>
        /// 子角色列表
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserPermission> Children { get; set; }

    }
}
