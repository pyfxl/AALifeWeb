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
            AvailableUserPositions = new List<UserPositionModel>();
            Allowed = new Dictionary<Guid, IDictionary<Guid, bool>>();
        }
        public IList<PermissionRecordModel> AvailablePermissions { get; set; }
        public IList<UserRoleModel> AvailableUserRoles { get; set; }
        public IList<UserDeptmentModel> AvailableUserDeptments { get; set; }
        public IList<UserPositionModel> AvailableUserPositions { get; set; }

        //[permission system name] / [customer role id] / [allowed]
        public IDictionary<Guid, IDictionary<Guid, bool>> Allowed { get; set; }
    }
}