using AALife.Data.Domain;
using System;
using System.Web.Script.Serialization;

namespace AALife.WebMvc.Models.ViewModel
{
    public class UserDetailViewModel : UserViewModel
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

        public virtual UserSettings UserSettings { get; set; }
        
        public int JoinDay { get; set; }

        public int ItemCount { get; set; }

    }
}