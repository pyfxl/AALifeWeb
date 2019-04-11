using AALife.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace AALife.Data.Domain
{
    [Table("tab_UserDeptment")]
    public partial class UserDeptment : BaseEntity
    {
        /// <summary>
        /// 父Id
        /// </summary>
        public int? ParentId { get; set; }
        
        /// <summary>
        /// 部门名称
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
        /// 部门类别
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string Category { get; set; }

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

        /// <summary>
        /// 父部门
        /// </summary>
        [ForeignKey("ParentId")]
        public virtual UserDeptment Parent { get; set; }

        /// <summary>
        /// 子部门列表
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserDeptment> Children { get; set; }

        /// <summary>
        /// 用户权限
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserPermission> UserPermissions { get; set; }

    }
}
