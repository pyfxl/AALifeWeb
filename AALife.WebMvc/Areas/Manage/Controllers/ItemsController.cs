using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class ItemsController : BaseAdminController
    {
        // GET: Manage/Lists
        public ActionResult Index(int userId = 0, int dateType = 0)
        {
            ViewBag.UserId = userId;
            ViewBag.DateType = dateType;
            return View();
        }
    }
}