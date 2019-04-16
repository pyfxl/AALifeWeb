using AALife.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Data.Domain
{
    [Table("tab_UserPosition")]
    public partial class UserPosition : BaseEntity<Guid>
    {
        /// <summary>
        /// 岗位名称
        /// </summary>
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 岗位编码
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string Code { get; set; }

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

        #region 组织

        /// <summary>
        /// 部门列表
        /// </summary>
        [ForeignKey("Deptment")]
        public Guid? DeptmentId { get; set; }

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        [JsonIgnore]
        public virtual UserDeptment Deptment { get; set; }

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
        public virtual UserPosition Parent { get; set; }

        /// <summary>
        /// 子部门列表
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserPosition> Children { get; set; }

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
    }
}
