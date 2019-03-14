using AALife.Core.Services.Configuration;
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
        private readonly IParameterService _parameterService;

        public UsersController(IUserService userService, IParameterService parameterService)
        {
            this._userService = userService;
            this._parameterService = parameterService;
        }

        // GET: Manage/Users
        [AdminAuthorize]
        public ActionResult Index()
        {
            var dataTheme = _parameterService.GetParamsByName("theme");
            var dataUserFrom = _parameterService.GetParamsByName("userfrom");
            var dataUserLevel = _parameterService.GetParamsByName("userlevel");
            ViewBag.DataTheme = dataTheme.ToValue();
            ViewBag.DataUserFrom = dataUserFrom.ToValue();
            ViewBag.DataUserLevel = dataUserLevel.ToValue();

            return View();
        }

        // GET: Manage/Users
        [AdminAuthorize]
        public ActionResult Index2()
        {
            var dataTheme = _parameterService.GetParamsByName("theme");
            var dataUserFrom = _parameterService.GetParamsByName("userfrom");
            var dataUserLevel = _parameterService.GetParamsByName("userlevel");
            ViewBag.DataTheme = dataTheme.ToValue();
            ViewBag.DataUserFrom = dataUserFrom.ToValue();
            ViewBag.DataUserLevel = dataUserLevel.ToValue();

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