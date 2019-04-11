using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using AALife.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Data.Services
{
    /// <summary>
    /// Permission service
    /// </summary>
    public partial class UserPermissionService : BaseService<UserPermission>, IUserPermissionService
    {
        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : user role ID
        /// {1} : permission system name
        /// </remarks>
        private const string PERMISSIONS_ALLOWED_KEY = "aalife.permission.allowed-{0}-{1}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string PERMISSIONS_PATTERN_KEY = "aalife.permission.";
        #endregion

        #region Fields

        private readonly IWorkContext _workContext;
        private readonly IUserRoleService _userRoleService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="permissionRecordRepository">Permission repository</param>
        /// <param name="userService">User service</param>
        /// <param name="workContext">Work context</param>
        /// <param name="localizationService">Localization service</param>
        /// <param name="languageService">Language service</param>
        /// <param name="cacheManager">Cache manager</param>
        public UserPermissionService(IRepository<UserPermission> permissionRepository,
            IWorkContext workContext,
            ICacheManager cacheManager,
            IDbContext dbContext,
            IUserRoleService userRoleService)
            : base(permissionRepository, cacheManager, dbContext)
        {
            this._workContext = workContext;
            this._userRoleService = userRoleService;
        }

        #endregion

        #region Methods
        
        public virtual string GetFormattedBreadCrumb(UserPermission permission, string separator = "--")
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            string result = string.Empty;

            var alreadyProcessedPermissionIds = new List<int>() { };

            while (permission != null && !alreadyProcessedPermissionIds.Contains(permission.Id)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = permission.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", permission.Name, separator, result);
                }

                alreadyProcessedPermissionIds.Add(permission.Id);

                permission = _repository.GetById(permission.ParentId);

            }

            return result;
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(UserPermission permission)
        {
            return Authorize(permission, _workContext.CurrentUser);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="user">User</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(UserPermission permission, UserTable user)
        {
            if (permission == null)
                return false;

            if (user == null)
                return false;

            //old implementation of Authorize method
            //var userRoles = user.UserRoles.Where(cr => cr.Active);
            //foreach (var role in userRoles)
            //    foreach (var permission1 in role.PermissionRecords)
            //        if (permission1.SystemName.Equals(permission.SystemName, StringComparison.InvariantCultureIgnoreCase))
            //            return true;

            //return false;

            return Authorize(permission.ActionName, permission.ControllerName, permission.AreaName, user);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string actionName, string controllerName, string areaName)
        {
            return Authorize(actionName, controllerName, areaName, _workContext.CurrentUser);
        }

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="user">User</param>
        /// <returns>true - authorized; otherwise, false</returns>
        public virtual bool Authorize(string actionName, string controllerName, string areaName, UserTable user)
        {
            if (user == null)
                return false;

            //roles
            var userRoles = user.UserRoles;
            foreach (var role in userRoles)
                if (role.UserPermissions.Count(a => a.ActionName == actionName && a.ControllerName == controllerName && a.AreaName == areaName) > 0)
                    //yes, we have such permission
                    return true;

            //deptments
            var userDeptments = user.UserDeptments;
            var userPositions = user.UserPositions;

            foreach (var dept in userDeptments)
                if (dept.UserPermissions.Count(a => a.ActionName == actionName && a.ControllerName == controllerName && a.AreaName == areaName) > 0)
                {
                    if (userPositions.SelectMany(p => p.UserPermissions.Where(a => a.ActionName == actionName && a.ControllerName == controllerName && a.AreaName == areaName)).Count() > 0)
                        //yes, we have such permission
                        return true;
                }

            //no permission found
            return false;
        }

        #endregion
    }
}