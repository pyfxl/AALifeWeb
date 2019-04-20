using AALife.Data.Domain;
using System;

namespace AALife.Data.Services
{
    public static class UserExtensions
    {
        public static string UserImageFull(this UserTable user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(user.UserImage)) user.UserImage = "user.gif";

            return user.UserImage.StartsWith("http") ? user.UserImage : string.Format("http://www.fxlweb.com/Images/Users/{0}", user.UserImage);
        }

        public static string UserNameFull(this UserTable user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return string.IsNullOrWhiteSpace(user.FirstName) ? user.UserName : user.FirstName;
        }
    }
}
