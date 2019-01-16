using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public class UserViewModel
    {
        public int UserID { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string UserNickName { get; set; }

        public string UserImage { get; set; }

        public string UserTheme { get; set; }

        public string UserLevel { get; set; }

        public string UserFrom { get; set; }

        public DateTime ModifyDate { get; set; }

        public DateTime CreateDate { get; set; }

        public byte Synchronize { get; set; }

        public byte IsUpdate { get; set; }

        public int UserWorkDay { get; set; }

        public string UserLevelName { get; set; }
        public string UserFromName { get; set; }

        public string UserThemeName
        {
            get
            {
                return Constant.ThemeDic[UserTheme];
            }
        }

        public string FullUserImage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(UserImage)) UserImage = "none.gif";
                return UserImage.StartsWith("http") ? UserImage : string.Format("http://www.fxlweb.com/Images/Users/{0}", UserImage);
            }
        }
    }
}