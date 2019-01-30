using AALife.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Data.Domain
{
    [Table("tab_UserTable")]
    public partial class UserTable : BaseEntity
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
        /// 可用否
        /// </summary>
        public byte Live { get; set; }

        /// <summary>
        /// 同步
        /// </summary>
        public byte Synchronize { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(100)]
        public string Remark { get; set; }

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

    }

}
