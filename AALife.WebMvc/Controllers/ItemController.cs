using AALife.Core;
using AALife.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class ItemController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;

        public ItemController(IUserService userService,
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
    }
}