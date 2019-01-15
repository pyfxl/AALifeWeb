using AALife.Service.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class UserController : Controller
    {
        private readonly UserTableBLL bll = new UserTableBLL();

        // GET: Manage/User
        public ActionResult Index(int id)
        {
            var user = bll.GetUser(id);
            return View(user);
        }
    }
}