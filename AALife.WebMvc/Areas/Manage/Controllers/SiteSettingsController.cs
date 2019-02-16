using AALife.Core.Domain.Configuration;
using AALife.Core.Domain.Logging;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using System.Web.Mvc;

namespace AALife.WebMvc.Areas.Manage.Controllers
{
    public class SiteSettingsController : BaseAdminController
    {
        private readonly ISettingService _settingService;
        private readonly ICustomerActivityService _customerActivityService;

        public SiteSettingsController(ISettingService settingService,
            ICustomerActivityService customerActivityService)
        {
            this._settingService = settingService;
            this._customerActivityService = customerActivityService;
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

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入网站配置记录。{0}", settings);

            SuccessNotification("添加成功。");

            return View(settings);
        }

    }
}