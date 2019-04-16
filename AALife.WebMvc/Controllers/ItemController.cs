using AALife.Data;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class ItemController : BaseMvcController
    {
        private readonly IUserService _userService;
        private readonly IWorkContext _workContext;

        public ItemController(IUserService userService,
            IWorkContext workContext)
        {
            this._userService = userService;
            this._workContext = workContext;
        }

        public ActionResult Index()
        {
            var user = _userService.Find(a => a.UserName == "admin");
            return View(user.ToModel());
        }
    }
}