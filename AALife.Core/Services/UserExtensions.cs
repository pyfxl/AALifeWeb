using AALife.Core.Domain;
using System;

namespace AALife.Core.Services
{
    public static class UserExtensions
    {
        public static string FullUserImage(this UserTable user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(user.UserImage)) user.UserImage = "none.gif";

            return user.UserImage.StartsWith("http") ? user.UserImage : string.Format("http://www.fxlweb.com/Images/Users/{0}", user.UserImage);
        }

        public static string FullUserName(this UserTable user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return string.IsNullOrWhiteSpace(user.UserNickName) ? user.UserName : user.UserNickName;
        }
    }
}
