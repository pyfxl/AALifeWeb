﻿using AALife.Core;
using AALife.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;

        public UserController(IUserService userService,
            IWorkContext workContext)
        {
            this._userService = userService;
            this._workContext = workContext;
        }

        [Authorize]
        public ActionResult Index()
        {
            var user = _userService.Get(_workContext.CurrentUser.Id);
            return View(user.ToModel());
        }

        public ActionResult CategoryPage()
        {
            return PartialView("_CategoryPage");
        }

        public ActionResult CardPage()
        {
            return PartialView("_CardPage");
        }

        public ActionResult ZhuanTiPage()
        {
            return PartialView("_ZhuanTiPage");
        }
    }
}