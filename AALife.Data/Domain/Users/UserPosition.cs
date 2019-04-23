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
    [Table("usr_UserPosition")]
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
        
        #region 用户岗位

        /// <summary>
        /// 用户岗位
        /// </summary>
        private ICollection<UsersPositions> _usersPositions;

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        public virtual ICollection<UsersPositions> UsersPositions
        {
            get { return _usersPositions ?? (_usersPositions = new List<UsersPositions>()); }
            protected set { _usersPositions = value; }
        }

        #endregion

        #region 组织

        /// <summary>
        /// 部门列表
        /// </summary>
        [ForeignKey("Deptment")]
        public Guid DeptmentId { get; set; }

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        [JsonIgnore]
        public virtual UserDeptment Deptment { get; set; }

        #endregion

        #region 职位

        /// <summary>
        /// 职位
        /// </summary>
        [ForeignKey("Title")]
        public Guid TitleId { get; set; }

        /// <summary>
        /// Gets or sets discount usage history
        /// </summary>
        public virtual UserTitle Title { get; set; }

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
        public virtual UserPosition Parent { get; set; }

        /// <summary>
        /// 子部门列表
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<UserPosition> Children { get; set; }

        #endregion

    }
}
