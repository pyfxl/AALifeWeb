using AALife.Core.Services.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class ItemsController : BaseAdminController
    {
        private readonly IParameterService _parameterService;

        public ItemsController(IParameterService parameterService)
        {
            this._parameterService = parameterService;
        }

        // GET: Manage/Lists
        [AdminAuthorize]
        public ActionResult Index(int userId = 0, int dateType = 0)
        {
            ViewBag.UserId = userId;
            ViewBag.DateType = dateType;
            return View();
        }

        // GET: Manage/Lists
        [AdminAuthorize]
        public ActionResult Index2(int userId = 0, int dateType = 0)
        {
            ViewBag.UserId = userId;
            ViewBag.DateType = dateType;

            var dataItemType = _parameterService.GetParamsByName("ItemType");
            var dataRegionType = _parameterService.GetParamsByName("RegionType");

            ViewBag.DataItemType = dataItemType.ToValue();
            ViewBag.DataRegionType = dataRegionType.ToValue();

            return View();
        }

        // GET: Manage/Lists
        public ActionResult Index3()
        {
            return View();
        }
    }
}