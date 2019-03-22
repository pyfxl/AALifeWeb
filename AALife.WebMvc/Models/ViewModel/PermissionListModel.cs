using AALife.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class PermissionListModel
    {
        public PermissionListModel()
        {
            this.UserPermissions = new List<UserPermission>();
            this.GrantedPermissionNames = new List<string>();
        }

        public IList<UserPermission> UserPermissions { get; set; }

        public IList<string> GrantedPermissionNames { get; set; }
    }
}