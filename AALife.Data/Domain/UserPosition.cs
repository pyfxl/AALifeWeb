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
    public partial class UserPosition : BaseEntity
    {
        /// <summary>
        /// 岗位名称
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(200)]
        public string Notes { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public int? ParentId { get; set; }

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

        /// <summary>
        /// 部门列表
        /// </summary>
        private ICollection<UserDeptment> _deptments;

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserDeptment> Deptments
        {
            get { return _deptments ?? (_deptments = new List<UserDeptment>()); }
            protected set { _deptments = value; }
        }

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

        /// <summary>
        /// 用户权限
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserPermission> UserPermissions { get; set; }

    }
}
