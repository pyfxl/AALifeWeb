using AALife.Core;
using AALife.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThrowHttp500()
        {
            throw new HttpException(500, "服务器错误");
        }
    }
}