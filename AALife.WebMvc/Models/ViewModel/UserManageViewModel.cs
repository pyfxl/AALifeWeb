using AALife.Data.Domain;
using System;
using System.Web.Script.Serialization;

namespace AALife.WebMvc.Models.ViewModel
{
    public class UserManageViewModel : UserViewModel
    {
        public string UserPassword { get; set; }

        public string UserTheme { get; set; }

        public string UserThemeName
        {
            get
            {
                return AALife.Data.Constant.UserThemeDic[UserTheme];
            }
        }

        public byte UserLevel { get; set; }

        public string UserLevelName
        {
            get
            {
                return AALife.Data.Constant.UserLevelDic[UserLevel];
            }
        }

        public byte Synchronize { get; set; }
        
    }
}