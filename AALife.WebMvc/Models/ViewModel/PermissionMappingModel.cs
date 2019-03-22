using AALife.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AALife.WebMvc.Models.ViewModel
{
    public partial class PermissionMappingModel : BaseViewModel
    {
        public PermissionMappingModel()
        {
            AvailablePermissions = new List<PermissionRecordModel>();
            AvailableUserRoles = new List<UserRoleModel>();
            AvailableUserDeptments = new List<UserDeptmentModel>();
            Allowed = new Dictionary<int, IDictionary<int, bool>>();
        }
        public IList<PermissionRecordModel> AvailablePermissions { get; set; }
        public IList<UserRoleModel> AvailableUserRoles { get; set; }
        public IList<UserDeptmentModel> AvailableUserDeptments { get; set; }

        //[permission system name] / [customer role id] / [allowed]
        public IDictionary<int, IDictionary<int, bool>> Allowed { get; set; }
    }
}