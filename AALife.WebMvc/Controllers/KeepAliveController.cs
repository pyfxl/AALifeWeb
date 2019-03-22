using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class KeepAliveController : Controller
    {
        // GET: KeepAlive
        public ActionResult Index()
        {
            return Content("I am alive!");
        }
    }
}