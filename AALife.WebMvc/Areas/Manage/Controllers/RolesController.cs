using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class RolesController : BaseAdminController
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public RolesController(IUserRoleService userRoleService,
            IUserService userService)
        {
            this._userService = userService;
            this._userRoleService = userRoleService;
        }

        // GET: Manage/Roles
        [AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manage/Roles
        [AdminAuthorize]
        public ActionResult Index2()
        {
            return View();
        }

        // GET: Manage/Roles
        [AdminAuthorize]
        public ActionResult Index3()
        {
            return View();
        }

        // GET: Manage/Roles
        [AdminAuthorize]
        public ActionResult Index4()
        {
            return View();
        }

        // GET: Manage/Roles
        public ActionResult Detail(int id)
        {
            ViewBag.Id = id;
            var role = _userRoleService.Get(id);
            return View(role);
        }

        public ActionResult RoleEdit()
        {
            return PartialView("_RoleEdit");
        }
    }
}