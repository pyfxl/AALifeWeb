using AALife.Data.Domain;
using System;
using System.Web.Script.Serialization;

namespace AALife.WebMvc.Models.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        public string UserName { get; set; }

        [ScriptIgnore]
        public string UserPassword { get; set; }

        public string UserNickName { get; set; }

        public string UserEmail { get; set; }

        public string UserImage { get; set; }

        public string UserTheme { get; set; }

        [ScriptIgnore]
        public byte UserLevel { get; set; }

        [ScriptIgnore]
        public string UserFrom { get; set; }

        [ScriptIgnore]
        public DateTime ModifyDate { get; set; }

        [ScriptIgnore]
        public DateTime CreateDate { get; set; }

        [ScriptIgnore]
        public byte Synchronize { get; set; }

        [ScriptIgnore]
        public byte Live { get; set; }

        [ScriptIgnore]
        public string Remark { get; set; }

        [ScriptIgnore]
        public string UserLevelName
        {
            get
            {
                return AALife.Data.Constant.UserLevelDic[UserLevel];
            }
        }

        [ScriptIgnore]
        public string UserFromName
        {
            get
            {
                return AALife.Data.Constant.UserFromDic[UserFrom];
            }
        }

        public string UserThemeName
        {
            get
            {
                return AALife.Data.Constant.UserThemeDic[UserTheme];
            }
        }

        public string UserNameFull { get; set; }

        public string UserImageFull { get; set; }

        [ScriptIgnore]
        public virtual UserSettings UserSettings { get; set; }
        
        public int JoinDay { get; set; }

        public int ItemCount { get; set; }

    }
}