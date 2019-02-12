using AALife.Core.Authentication;
using AALife.Core.Domain.Customers;
using AALife.Core.Services;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IAuthenticationService _authenticationService;

        public AccountController(ICustomerService customerService, IAuthenticationService authenticationService)
        {
            this._customerService = customerService;
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

        public ActionResult ResetPassword(string userName)
        {
            ViewBag.Username = userName;
            return View();
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName,UserPassword")] UserLoginModel userModel, string returnUrl = "~/")
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _customerService.Login(userModel.UserName, userModel.UserPassword);

                    if (user.ResetPassword)
                    {
                        return RedirectToAction("ResetPassword", new { userName = user.Username });
                    }

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

        [HttpPost]
        public ActionResult ResetPassword(string username, string password, string rePassword)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!password.Equals(rePassword))
                    {
                        throw new Exception("重复密码不正确。");
                    }

                    _customerService.ResetPassword(username, password);

                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.Username = username;
            return View();
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