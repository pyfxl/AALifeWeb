using AALife.Core.Domain.Configuration;
using AALife.Core.Services.Configuration;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class CommonSettingsController : Controller
    {
        private readonly ISettingService _settingService;

        public CommonSettingsController(ISettingService settingService)
        {
            this._settingService = settingService;
        }

        // GET: Manage/SiteSettings
        public ActionResult Index()
        {
            var model = _settingService.LoadSetting<CommonSettings>(0);
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Index(CommonSettings settings)
        {
            if (ModelState.IsValid)
            {
                _settingService.SaveSetting(settings, 0);
            }

            return View(settings);
        }

    }
}