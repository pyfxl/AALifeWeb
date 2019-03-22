using AALife.Core.Services;
using AALife.Data.Domain;
using System.Collections.Generic;

namespace AALife.Data.Services
{
    /// <summary>
    /// Permission service interface
    /// </summary>
    public partial interface IUserPermissionService : IBaseService<UserPermission>
    {
        /// <summary>
        /// 获取多级目录
        /// </summary>
        /// <param name="permission"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        string GetFormattedBreadCrumb(UserPermission permission, string separator = "--");

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(UserPermission permission);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permission">Permission record</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(UserPermission permission, UserTable user);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(string actionName, string controllerName, string areaName);

        /// <summary>
        /// Authorize permission
        /// </summary>
        /// <param name="permissionRecordSystemName">Permission record system name</param>
        /// <param name="customer">Customer</param>
        /// <returns>true - authorized; otherwise, false</returns>
        bool Authorize(string actionName, string controllerName, string areaName, UserTable user);
    }
}