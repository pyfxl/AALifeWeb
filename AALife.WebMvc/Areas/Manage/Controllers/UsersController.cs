using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET: Manage/Users
        [AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Roles()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var user = _userService.Get(id);
            var model = user.ToModel();
            return View(model);
        }
    }
}