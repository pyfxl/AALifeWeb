using AALife.Data.Domain;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class UserDetailViewModel : UserViewModel
    {
        public string UserPassword { get; set; }

        public string UserTheme { get; set; }

        public string UserThemeName { get; set; }

        //public virtual UserSettings UserSettings { get; set; }
        
        public int JoinDay { get; set; }

        public int ItemCount { get; set; }

        public UserRoleListModel UserRoleLists { get; set; }

        public IList<UserDeptmentModel> UserDeptments { get; set; }

        public UserDetailViewModel()
        {
            UserRoleLists = new UserRoleListModel();
        }
    }
}