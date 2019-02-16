using AALife.Core.Services.Configuration;
using AALife.Data;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class UserController : BaseMvcController
    {
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;
        private readonly ISettingService _settingService;

        public UserController(IUserService userService,
            IWorkContext workContext,
            ISettingService settingService)
        {
            this._userService = userService;
            this._workContext = workContext;
            this._settingService = settingService;
        }

        public ActionResult Index()
        {
            var user = _userService.Get(_workContext.CurrentUser.Id);
            var model = user.ToModel();
            model.UserSettings = _settingService.LoadSetting<UserSettings>(user.Id);

            return View(model);
        }

        /// <summary>
        /// mvc form sample
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        [HttpPost]
        public virtual ActionResult SaveSettings(UserSettings settings)
        {
            int userId = _workContext.CurrentUser.Id;

            if (ModelState.IsValid)
            {
                _settingService.SaveSetting(settings, userId);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public virtual ActionResult UserSettings(UserSettings settings)
        {
            int userId = _workContext.CurrentUser.Id;

            if (ModelState.IsValid)
            {
                _settingService.SaveSetting(settings, userId);
            }

            return Content("");
        }

        public ActionResult CategoryPage()
        {
            return PartialView("_CategoryPage");
        }

        public ActionResult CardPage()
        {
            return PartialView("_CardPage");
        }

        public ActionResult ZhuanTiPage()
        {
            return PartialView("_ZhuanTiPage");
        }

        public ActionResult ZhuanZhangPage()
        {
            return PartialView("_ZhuanZhangPage");
        }

    }
}