using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AALife.Core.Domain
{
    [Table("tab_UserTable")]
    public partial class UserTable : BaseEntity
    {
        [Key]
        public int UserID { get; set; }

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
        /// 修改日期
        /// </summary>
        [Display(Name = "修改日期")]
        public DateTime ModifyDate { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        [Display(Name = "创建日期")]
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 同步
        /// </summary>
        [Display(Name = "同步否")]
        public byte Synchronize { get; set; }

        /// <summary>
        /// 是否升级
        /// </summary>
        [Display(Name = "升级否")]
        public byte IsUpdate { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public virtual UserLevelTable UserLevelTable { get; set; }

        /// <summary>
        /// 来自
        /// </summary>
        public virtual UserFromTable UserFromTable { get; set; }

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
        /// 转账列表
        /// </summary>
        public virtual ICollection<ZhuanZhangTable> ZhuanZhangTables { get; set; }

        /// <summary>
        /// OAuth列表
        /// </summary>
        public virtual ICollection<OAuthTable> OAuthTables { get; set; }

    }

}
