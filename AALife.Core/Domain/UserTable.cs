using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain
{
    [Table("tab_UserTable")]
    public partial class UserTable : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(20)]
        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(20)]
        [Required]
        [Display(Name = "密码")]
        public string UserPassword { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "昵称")]
        public string UserNickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "邮箱")]
        public string UserEmail { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "头像")]
        public string UserImage { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        [MaxLength(10)]
        [Display(Name = "样式")]
        public string UserTheme { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [Display(Name = "等级")]
        public byte UserLevel { get; set; }

        /// <summary>
        /// 来自
        /// </summary>
        [MaxLength(10)]
        [Required]
        [Display(Name = "来自")]
        public string UserFrom { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }

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

    }

}
