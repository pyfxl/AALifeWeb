using AALife.Data.Domain;
using AALife.Data.Services;
using System;
using System.Web;
using System.Web.Security;

namespace AALife.Data.Authentication
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public partial class FormsAuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly IUserService _userService;
        private readonly TimeSpan _expirationTimeSpan;

        private UserTable _cachedUser;
        private string _ticket { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FormsAuthenticationService(IUserService userService)
        {
            this._userService = userService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="userService">User service</param>
        /// <param name="userSettings">User settings</param>
        public FormsAuthenticationService(HttpContextBase httpContext,
            IUserService userService)
        {
            this._httpContext = httpContext;
            this._userService = userService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get authenticated user
        /// </summary>
        /// <param name="ticket">Ticket</param>
        /// <returns>User</returns>
        protected virtual UserTable GetAuthenticatedUserFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var userName = ticket.UserData;

            if (String.IsNullOrWhiteSpace(userName))
                return null;
            var user = _userService.GetUserByUserName(userName);
            return user;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="createPersistentCookie">A value indicating whether to create a persistent cookie</param>
        public virtual void SignIn(UserTable user, bool createPersistentCookie, bool isApi = false)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                user.UserName,
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                user.UserName,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            _ticket = encryptedTicket;

            if (isApi)
                return;

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);
            _cachedUser = user;
        }

        /// <summary>
        /// Sign out
        /// </summary>
        public virtual void SignOut()
        {
            _cachedUser = null;
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// Get authenticated user
        /// </summary>
        /// <returns>User</returns>
        public virtual UserTable GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var user = GetAuthenticatedUserFromTicket(formsIdentity.Ticket);
            if (user != null)
                _cachedUser = user;
            return _cachedUser;
        }

        /// <summary>
        /// Get authenticated user by ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public virtual UserTable GetAuthenticatedUserByTicket(string ticket)
        {
            if (string.IsNullOrWhiteSpace(ticket))
                throw new ArgumentNullException("ticket");

            var userName = "";
            try
            {
                userName = FormsAuthentication.Decrypt(ticket).UserData;
            }
            catch
            {
            }

            if (String.IsNullOrWhiteSpace(userName))
                return null;
            var user = _userService.GetUserByUserName(userName);
            return user;
        }

        /// <summary>
        /// Get ticket
        /// </summary>
        /// <returns></returns>
        public virtual string GetTicket()
        {
            return _ticket;
        }

        #endregion

    }
}