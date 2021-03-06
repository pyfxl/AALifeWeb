﻿using AALife.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace AALife.Data.Domain
{
    [Table("usr_UserDeptment")]
    public partial class UserDeptment : BaseEntity<Guid>
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 部门编码
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 部门类型
        /// </summary>
        [MaxLength(10)]
        public string OrgType { get; set; }

        /// <summary>
        /// 部门等级
        /// </summary>
        public int? OrgLevel { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(200)]
        public string Notes { get; set; }

        #region 用户

        /// <summary>
        /// 用户列表
        /// </summary>
        private ICollection<UserTable> _users;

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserTable> Users
        {
            get { return _users ?? (_users = new List<UserTable>()); }
            protected set { _users = value; }
        }

        #endregion

        #region 岗位

        /// <summary>
        /// 岗位列表
        /// </summary>
        private ICollection<UserPosition> _positions;

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserPosition> Positions
        {
            get { return _positions ?? (_positions = new List<UserPosition>()); }
            protected set { _positions = value; }
        }

        #endregion

        #region 权限

        /// <summary>
        /// 用户权限
        /// </summary>
        private ICollection<UserPermission> _userPermissions;

        /// <summary>
        /// 用户权限
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserPermission> UserPermissions
        {
            get { return _userPermissions ?? (_userPermissions = new List<UserPermission>()); }
            protected set { _userPermissions = value; }
        }

        #endregion

        #region 父子

        /// <summary>
        /// 父Id
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 父部门
        /// </summary>
        [ForeignKey("ParentId")]
        //[JsonIgnore]
        public virtual UserDeptment Parent { get; set; }

        /// <summary>
        /// 子部门列表
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserDeptment> Children { get; set; }

        #endregion

    }
}
