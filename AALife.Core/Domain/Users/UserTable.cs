using AALife.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain.Users
{
    public partial class UserTable : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string UserPassword { get; set; }

        /// <summary>
        /// 密码盐
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string UserNickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string UserImage { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string UserTheme { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public byte UserLevel { get; set; }

        /// <summary>
        /// 来自
        /// </summary>
        public string UserFrom { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// 同步否
        /// </summary>
        public int Synchronize { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

}
