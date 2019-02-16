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

        public RolesController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET: Manage/Roles
        public ActionResult Index()
        {
            return View();
        }
    }
}