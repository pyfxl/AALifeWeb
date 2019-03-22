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
        /// 父部门
        /// </summary>
        [ForeignKey("ParentId")]
        [JsonIgnore]
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
