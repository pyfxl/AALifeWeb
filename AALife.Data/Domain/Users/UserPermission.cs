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
    [Table("usr_UserPermission")]
    public partial class UserPermission : OrderEntity<Guid>
    {
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
        /// 图标
        /// </summary>
        [MaxLength(50)]
        public string IconName { get; set; }

        /// <summary>
        /// 是否按钮
        /// </summary>
        public bool IsButton { get; set; }

        /// <summary>
        /// 是否页面
        /// </summary>
        public bool IsPage { get; set; }

        #region 角色

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

        #endregion

        #region 组织

        /// <summary>
        /// 组织列表
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

        #endregion

        #region 岗位

        /// <summary>
        /// 岗位列表
        /// </summary>
        private ICollection<UserPosition> _userPositions;

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        public virtual ICollection<UserPosition> UserPositions
        {
            get { return _userPositions ?? (_userPositions = new List<UserPosition>()); }
            protected set { _userPositions = value; }
        }

        #endregion

        #region 职位

        /// <summary>
        /// 职位
        /// </summary>
        private ICollection<UserTitle> _userTitles;

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        public virtual ICollection<UserTitle> UserTitles
        {
            get { return _userTitles ?? (_userTitles = new List<UserTitle>()); }
            protected set { _userTitles = value; }
        }

        #endregion

        #region 父子

        /// <summary>
        /// 父Id
        /// </summary>
        public Guid? ParentId { get; set; }

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

        #endregion

    }
}
