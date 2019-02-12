using AALife.Core.Domain;
using AALife.Core.Domain.Customers;
using AALife.Core.Services;
using System;
using System.Web;
using System.Web.Security;

namespace AALife.Core.Authentication
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public partial class FormsAuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;
        private readonly TimeSpan _expirationTimeSpan;

        private Customer _cachedCustomer;
        private string _ticket { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public FormsAuthenticationService(ICustomerService customerService)
        {
            this._customerService = customerService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="customerService">Customer service</param>
        /// <param name="customerSettings">Customer settings</param>
        public FormsAuthenticationService(HttpContextBase httpContext,
            ICustomerService customerService)
        {
            this._httpContext = httpContext;
            this._customerService = customerService;
            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <param name="ticket">Ticket</param>
        /// <returns>Customer</returns>
        protected virtual Customer GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var userName = ticket.UserData;

            if (String.IsNullOrWhiteSpace(userName))
                return null;
            var customer = _customerService.GetCustomerByUsername(userName);
            return customer;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="createPersistentCookie">A value indicating whether to create a persistent cookie</param>
        public virtual void SignIn(Customer customer, bool createPersistentCookie, bool isApi = false)
        {
            var now = DateTime.Now;

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                customer.Username,
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                customer.Username,
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
            _cachedCustomer = customer;
        }

        /// <summary>
        /// Sign out
        /// </summary>
        public virtual void SignOut()
        {
            _cachedCustomer = null;
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Customer</returns>
        public virtual Customer GetAuthenticatedCustomer()
        {
            if (_cachedCustomer != null)
                return _cachedCustomer;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var customer = GetAuthenticatedCustomerFromTicket(formsIdentity.Ticket);
            if (customer != null)
                _cachedCustomer = customer;
            return _cachedCustomer;
        }

        /// <summary>
        /// Get authenticated customer by ticket
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public virtual Customer GetAuthenticatedCustomerByTicket(string ticket)
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
            var customer = _customerService.GetCustomerByUsername(userName);
            return customer;
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