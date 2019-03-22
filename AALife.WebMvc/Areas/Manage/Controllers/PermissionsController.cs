﻿using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class PermissionsController : BaseAdminController
    {
        private readonly IUserPermissionService _permissionService;
        private readonly IUserRoleService _userRoleService;
        private readonly IUserDeptmentService _userDeptmentService;

        public PermissionsController(IUserRoleService userRoleService,
            IUserDeptmentService userDeptmentService,
            IUserPermissionService permissionService)
        {
            this._permissionService = permissionService;
            this._userRoleService = userRoleService;
            this._userDeptmentService = userDeptmentService;
        }

        // GET: Manage/Permissions
        [AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Employees()
        {
            return View();
        }

        [AdminAuthorize]
        public ActionResult Setting()
        {
            var model = new PermissionMappingModel();

            var permissionRecords = _permissionService.Get();

            permissionRecords = permissionRecords.OrderBy(a => a.OrderNo);

            var userRoles = _userRoleService.Get();

            foreach (var pr in permissionRecords)
            {
                model.AvailablePermissions.Add(new PermissionRecordModel
                {
                    Id = pr.Id,
                    //Name = pr.Name,
                    Name = _permissionService.GetFormattedBreadCrumb(pr)
                });
            }

            foreach (var cr in userRoles)
            {
                model.AvailableUserRoles.Add(new UserRoleModel
                {
                    Id = cr.Id,
                    Name = cr.Name
                });
            }

            foreach (var pr in permissionRecords)
                foreach (var cr in userRoles)
                {
                    bool allowed = pr.UserRoles.Count(x => x.Id == cr.Id) > 0;
                    if (!model.Allowed.ContainsKey(pr.Id))
                        model.Allowed[pr.Id] = new Dictionary<int, bool>();
                    model.Allowed[pr.Id][cr.Id] = allowed;
                }

            return View(model);
        }

        //[AdminAuthorize]
        public ActionResult Setting2()
        {
            var model = new PermissionMappingModel();

            var permissionRecords = _permissionService.Get();

            permissionRecords = permissionRecords.OrderBy(a => a.OrderNo);

            var userDeptments = _userDeptmentService.Get();

            foreach (var pr in permissionRecords)
            {
                model.AvailablePermissions.Add(new PermissionRecordModel
                {
                    Id = pr.Id,
                    //Name = pr.Name,
                    Name = _permissionService.GetFormattedBreadCrumb(pr)
                });
            }

            foreach (var cr in userDeptments)
            {
                model.AvailableUserDeptments.Add(new UserDeptmentModel
                {
                    Id = cr.Id,
                    Name = cr.Name
                });
            }

            foreach (var pr in permissionRecords)
                foreach (var cr in userDeptments)
                {
                    bool allowed = pr.UserDeptments.Count(x => x.Id == cr.Id) > 0;
                    if (!model.Allowed.ContainsKey(pr.Id))
                        model.Allowed[pr.Id] = new Dictionary<int, bool>();
                    model.Allowed[pr.Id][cr.Id] = allowed;
                }

            return View(model);
        }

        [AdminAuthorize]
        public ActionResult SettingTree()
        {            
            return View();
        }

    }
}