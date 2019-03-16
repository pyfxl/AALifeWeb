﻿using AALife.Data;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Areas.Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class HomeController : BaseAdminController
    {
        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContextService;

        public HomeController(IUserService userService, 
            IPermissionService permissionService,
            IWorkContext workContextService)
        {
            this._userService = userService;
            this._permissionService = permissionService;
            this._workContextService = workContextService;
        }

        // GET: Manage/Common
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manage/Common
        public ActionResult UserSelect()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult SidebarMenu()
        {
            var permissions = new List<PermissionRecord> { };

            var rolePermissions = _workContextService.CurrentUser.UserRoles.Select(t => t.PermissionRecords.OrderBy(a => a.OrderNo)).ToList();

            foreach (var rp in rolePermissions)
            {
                permissions = permissions.Union(rp).ToList();
            }

            var model = SortMenuForTree(null, permissions);
            return PartialView(model);
        }

        /// <summary>
        /// 菜单节点
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <returns></returns>
        public List<MenuViewModel> SortMenuForTree(int? parentId, IEnumerable<PermissionRecord> rolePermissions)
        {
            var model = new List<MenuViewModel>();
            foreach (var p in rolePermissions.Where(t => t.ParentId == parentId))
            {
                var menu = new MenuViewModel
                {
                    Name = p.Name,
                    ControllerName = p.ControllerName,
                    ActionName = p.ActionName
                };
                menu.ChildMenus.AddRange(SortMenuForTree(p.Id, rolePermissions));
                model.Add(menu);
            }
            return model;
        }
        
    }
}