using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AALife.Service.Models;
using AALife.WebMvc.Areas.Manage.Models;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class SiteConfigController : Controller
    {
        // GET: Manage/SiteConfigs/Create
        public ActionResult Create()
        {
            var config = new SiteConfigModel();
            config.SiteName = WebConfiguration.SiteName;
            config.SiteAuthor = WebConfiguration.SiteAuthor;
            config.SiteKeywords = WebConfiguration.SiteKeywords;
            config.SiteDescription = WebConfiguration.SiteDescription;
            config.PagePerNumber = WebConfiguration.PagePerNumber;
            config.UserWorkDay = WebConfiguration.UserWorkDay;
            config.CategoryRate = WebConfiguration.CategoryRate;
            config.SiteTips = Utility.UnReplaceString(WebConfiguration.SiteTips);
            config.MessageCode = WebConfiguration.MessageCode;
            config.SiteMessage = Utility.UnReplaceString(WebConfiguration.SiteMessage);
            config.PhoneMessage = Utility.UnReplaceString(WebConfiguration.PhoneMessage);
            config.PageNumber = WebConfiguration.PageNumber;

            return View(config);
        }

        // POST: Manage/SiteConfigs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SiteConfigModel siteConfigModel)
        {
            if (ModelState.IsValid)
            {
                WebConfiguration.SiteName = siteConfigModel.SiteName;
                WebConfiguration.SiteAuthor = siteConfigModel.SiteAuthor;
                WebConfiguration.SiteKeywords = siteConfigModel.SiteKeywords;
                WebConfiguration.SiteDescription = siteConfigModel.SiteDescription;
                WebConfiguration.PagePerNumber = siteConfigModel.PagePerNumber;
                WebConfiguration.UserWorkDay = siteConfigModel.UserWorkDay;
                WebConfiguration.CategoryRate = siteConfigModel.CategoryRate;
                WebConfiguration.SiteTips = Utility.ReplaceString(siteConfigModel.SiteTips);
                WebConfiguration.MessageCode = siteConfigModel.MessageCode;
                WebConfiguration.SiteMessage = Utility.ReplaceString(siteConfigModel.SiteMessage);
                WebConfiguration.PhoneMessage = Utility.ReplaceString(siteConfigModel.PhoneMessage);
                WebConfiguration.PageNumber = siteConfigModel.PageNumber;
                WebConfiguration.SetConfig();
                return RedirectToAction("Create");
            }

            return View();
        }

    }
}
