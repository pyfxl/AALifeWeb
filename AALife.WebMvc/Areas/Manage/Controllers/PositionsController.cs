using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class PositionsController : BaseAdminController
    {
        private readonly IUserService _userService;

        public PositionsController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET: Manage/Positions
        [AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Manage/Positions
        [AdminAuthorize]
        public ActionResult Index2()
        {
            return View();
        }

    }
}