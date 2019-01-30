using AALife.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string UserNickName { get; set; }

        public string UserEmail { get; set; }

        public string UserImage { get; set; }

        public string UserTheme { get; set; }

        public byte UserLevel { get; set; }

        public string UserFrom { get; set; }

        public DateTime ModifyDate { get; set; }

        public DateTime CreateDate { get; set; }

        public byte Synchronize { get; set; }

        public string UserLevelName
        {
            get
            {
                return Constant.UserLevelDic[UserLevel];
            }
        }

        public string UserFromName
        {
            get
            {
                return Constant.UserFromDic[UserFrom];
            }
        }

        public string UserThemeName
        {
            get
            {
                return Constant.ThemeDic[UserTheme];
            }
        }

        public string UserImageFull
        {
            get
            {
                if (string.IsNullOrWhiteSpace(UserImage)) UserImage = "none.gif";
                return UserImage.StartsWith("http") ? UserImage : string.Format("http://www.fxlweb.com/Images/Users/{0}", UserImage);
            }
        }

        public virtual UserSettings UserSettings { get; set; }

    }
}