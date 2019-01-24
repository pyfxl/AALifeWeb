using AALife.Core.Authentication;
using AALife.Core.Services;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }
        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UserName,UserPassword")] UserLoginModel userModel, string returnUrl = "~/")
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _userService.Login(userModel.UserName, userModel.UserPassword);

                    //sign in new customer
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

        // GET: Logout
        public ActionResult Logout()
        {
            //standard logout 
            _authenticationService.SignOut();

            return Redirect("~/");
        }

    }
}