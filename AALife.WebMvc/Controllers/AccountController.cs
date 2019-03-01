using AALife.Data.Authentication;
using AALife.Data.Services;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Web;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IUserService userService, IAuthenticationService authenticationService)
        {
            this._userService = userService;
            this._authenticationService = authenticationService;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnUrl = "~/")
        {
            ViewBag.ReturnUrl = HttpUtility.UrlEncode(returnUrl);
            return View();
        }

        // GET: Logout
        public ActionResult Logout()
        {
            //standard logout 
            _authenticationService.SignOut();

            return Redirect("~/");
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }

        //public ActionResult ResetPassword(string userName)
        //{
        //    ViewBag.Username = userName;
        //    return View();
        //}

        [HttpPost]
        public ActionResult Login(UserLoginModel userModel, string returnUrl = "~/")
        {
            ViewBag.ReturnUrl = HttpUtility.UrlEncode(returnUrl);

            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userService.Login(userModel.UserName, userModel.UserPassword);

                    //sign in new user
                    _authenticationService.SignIn(user, false);

                    return Redirect(returnUrl);
                }
                catch(Exception ex)
                {
                     ModelState.AddModelError("", ex.Message);
                }                
            }

            return View(userModel);
        }

        //[HttpPost]
        //public ActionResult ResetPassword(string username, string password, string rePassword)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (!password.Equals(rePassword))
        //            {
        //                throw new Exception("重复密码不正确。");
        //            }

        //            _userService.ResetPassword(username, password);

        //            return RedirectToAction("Login");
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("", ex.Message);
        //        }
        //    }

        //    ViewBag.Username = username;
        //    return View();
        //}

    }
}