using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AALife.EF.ViewModel
{
    public class UserTableViewModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserNickName { get; set; }
        public string UserImage { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public string UserTheme { get; set; }
        public byte UserLevel { get; set; }
        public string UserFrom { get; set; }
        public string UserFromName { get; set; }
        public System.DateTime ModifyDate { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string UserCity { get; set; }
        public decimal UserMoney { get; set; }
        public string UserWorkDay { get; set; }
        public string UserWorkDayName { get; set; }
        public string UserFunction { get; set; }
        public int CategoryRate { get; set; }
        public byte Synchronize { get; set; }
        public Nullable<decimal> MoneyStart { get; set; }
        public Nullable<byte> IsUpdate { get; set; }
    }
}
