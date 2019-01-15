using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class ViewsController : Controller
    {
        // GET: Manage/Views
        public ActionResult Index()
        {
            return View();
        }
    }
}