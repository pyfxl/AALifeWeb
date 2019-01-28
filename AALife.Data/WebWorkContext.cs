using AALife.Data.Authentication;
using AALife.Data.Domain;
using AALife.Data.Services;

namespace AALife.Data
{
    /// <summary>
    /// Work context for web application
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;

        private UserTable _cachedUser;

        #endregion

        #region Ctor

        public WebWorkContext(IUserService userService,
            IAuthenticationService authenticationService)
        {
            this._userService = userService;
            this._authenticationService = authenticationService;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current user
        /// </summary>
        public virtual UserTable CurrentUser
        {
            get
            {
                if (_cachedUser != null)
                    return _cachedUser;

                UserTable user = null;

                //registered user
                if (user == null)
                {
                    user = _authenticationService.GetAuthenticatedUser();
                }

                //validation
                if (user != null)
                {
                    _cachedUser = user;
                }

                return _cachedUser;
            }
            set
            {
                _cachedUser = value;
            }
        }

        /// <summary>
        /// Get or set value indicating whether we're in admin area
        /// </summary>
        public virtual bool IsAdmin { get; set; }

        #endregion
    }
}
