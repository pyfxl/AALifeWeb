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
    [Table("usr_UserTitle")]
    public partial class UserTitle : BaseEntity<Guid>
    {
        /// <summary>
        /// 职位名称
        /// </summary>
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 职位编码
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(200)]
        public string Notes { get; set; }

        #region 岗位

        /// <summary>
        /// 岗位
        /// </summary>
        private ICollection<UserPosition> _userPositions;

        /// <summary>
        /// 岗位
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserPosition> UserPositions
        {
            get { return _userPositions ?? (_userPositions = new List<UserPosition>()); }
            protected set { _userPositions = value; }
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
    }
}
