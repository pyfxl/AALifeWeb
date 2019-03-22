using AALife.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class UserRoleListModel
    {
        public UserRoleListModel()
        {
            this.UserRoles = new List<UserRole>();
            this.SelectedNames = new List<string>();
        }

        public IList<UserRole> UserRoles { get; set; }

        public IList<string> SelectedNames { get; set; }
    }
}