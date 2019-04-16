using AALife.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Data.Domain
{
    [Table("tab_UserTable")]
    public partial class UserTable : BaseEntity<Guid>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string UserPassword { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        [MaxLength(10)]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50)]
        public string UserNickName { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [MaxLength(20)]
        [Required]
        public string UserCode { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [MaxLength(20)]
        public string FirstName { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [MaxLength(50)]
        public string UserPhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(200)]
        public string UserEmail { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(200)]
        public string UserImage { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [MaxLength(10)]
        public string UserTheme { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public byte UserLevel { get; set; }

        /// <summary>
        /// 来自
        /// </summary>
        [MaxLength(10)]
        [Required]
        public string UserFrom { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(100)]
        public string Remark { get; set; }
        
        /// <summary>
        /// 同步
        /// </summary>
        public byte Synchronize { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 商品列表
        /// </summary>
        public virtual ICollection<ItemTable> ItemTables { get; set; }

        /// <summary>
        /// 类别列表
        /// </summary>
        public virtual ICollection<CategoryTypeTable> CategoryTypeTables { get; set; }

        /// <summary>
        /// 卡列表
        /// </summary>
        public virtual ICollection<CardTable> CardTables { get; set; }

        /// <summary>
        /// 专题列表
        /// </summary>
        public virtual ICollection<ZhuanTiTable> ZhuanTiTables { get; set; }

        /// <summary>
        /// OAuth列表
        /// </summary>
        public virtual ICollection<OAuthTable> OAuthTables { get; set; }

        /// <summary>
        /// 转账列表
        /// </summary>
        public virtual ICollection<ZhuanZhangTable> ZhuanZhangTables { get; set; }

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

        #region 部门

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
    }

}
