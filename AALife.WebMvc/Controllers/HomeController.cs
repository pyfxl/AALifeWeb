using AALife.Core.Services.Configuration;
using AALife.Core.Services.Security;
using AALife.Data;
using AALife.Data.Domain;
using AALife.Data.Services;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class HomeController : BaseMvcController
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IEncryptionService _encryptionService;
        private readonly ISettingService _settingService;

        public HomeController(IUserService userService,
            IUserRoleService userRoleService,
            IEncryptionService encryptionService,
            ISettingService settingService)
        {
            this._userService = userService;
            this._userRoleService = userRoleService;
            this._encryptionService = encryptionService;
            this._settingService = settingService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThrowHttp500()
        {
            throw new HttpException(500, "服务器错误");
        }

        [AllowAnonymous]
        public ActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult UpdatePassword(string returnUrl = "")
        {
            var users = _userService.Get();
            var role = _userRoleService.Find(a => a.SystemName == UserRoleNames.Administrators);
            users.ToList().ForEach(a =>
            {
                var saltKey = _encryptionService.CreateSaltKey(Constant.PasswordSaltSize);
                a.PasswordSalt = saltKey;
                a.UserPassword = _encryptionService.CreatePasswordHash(a.UserPassword, saltKey);

                //权限
                if(a.UserName == "admin")
                {
                    a.UserRoles.Add(role);
                }
            });

            _userService.Update(users);

            return View();
        }
    }
}