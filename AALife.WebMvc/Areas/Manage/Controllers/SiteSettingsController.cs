using AALife.Core.Domain.Common;
using AALife.Core.Services.Configuration;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class SiteSettingsController : Controller
    {
        private readonly ISettingService _settingService;

        public SiteSettingsController(ISettingService settingService)
        {
            this._settingService = settingService;
        }

        // GET: Manage/SiteSettings
        public ActionResult Index()
        {
            var model = _settingService.LoadSetting<SiteSettings>(0);
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Index(SiteSettings settings)
        {
            if (ModelState.IsValid)
            {
                _settingService.SaveSetting(settings, 0);
            }

            return View(settings);
        }

    }
}