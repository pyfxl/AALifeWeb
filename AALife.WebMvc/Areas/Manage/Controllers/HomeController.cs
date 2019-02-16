using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class HomeController : BaseAdminController
    {
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
    }
}