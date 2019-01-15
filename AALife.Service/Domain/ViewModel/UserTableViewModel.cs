using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AALife.Service.Domain.ViewModel
{
    public class UserTableViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }

        [Display(Name="昵称")]
        public string UserNickName { get; set; }
        public string UserImage { get; set; }

        [Display(Name = "手机")]
        public string UserPhone { get; set; }

        [Display(Name = "邮箱")]
        public string UserEmail { get; set; }

        [Display(Name = "主题")]
        public string UserTheme { get; set; }
        public byte UserLevel { get; set; }
        public string UserFrom { get; set; }
        public System.DateTime ModifyDate { get; set; }

        [Display(Name = "创建日期")]
        public System.DateTime CreateDate { get; set; }
        public string UserCity { get; set; }
        public decimal UserMoney { get; set; }
        public string UserWorkDay { get; set; }
        public string UserFunction { get; set; }
        public int CategoryRate { get; set; }
        public byte Synchronize { get; set; }
        public Nullable<decimal> MoneyStart { get; set; }
        public Nullable<byte> IsUpdate { get; set; }

        [Display(Name = "来自")]
        public string UserFromName { get; set; }

        [Display(Name = "工作日")]
        public string UserWorkDayName { get; set; }

        public int JoinDay { get; set; }
        public int ItemCount { get; set; }
    }
}
