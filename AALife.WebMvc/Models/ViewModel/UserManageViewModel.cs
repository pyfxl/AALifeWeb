using AALife.Data.Domain;
using System;
using System.Web.Script.Serialization;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class UserManageViewModel : UserViewModel
    {
        public string UserPassword { get; set; }

        public string UserTheme { get; set; }

        public string UserThemeName { get; set; }

        public byte UserLevel { get; set; }

        public string UserLevelName { get; set; }

        public byte Synchronize { get; set; }
        
    }
}