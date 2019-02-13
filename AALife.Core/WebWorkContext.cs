using AALife.Core.Authentication;
using AALife.Core.Domain.Configuration;
using AALife.Core.Domain.Users;
using AALife.Core.Services;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Users;

namespace AALife.Core
{
    /// <summary>
    /// Work context for web application
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingService _settingService;

        private UserTable _cachedUser;
        private CommonSettings _cachedSettings;

        #endregion

        #region Ctor

        public WebWorkContext(IUserService userService,
            IAuthenticationService authenticationService,
            ISettingService settingService)
        {
            this._userService = userService;
            this._authenticationService = authenticationService;
            this._settingService = settingService;
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

                UserTable user = _authenticationService.GetAuthenticatedUser();

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

        /// <summary>
        /// site settings
        /// </summary>
        public virtual CommonSettings CommonSettings
        {
            get
            {
                if (_cachedSettings != null)
                    return _cachedSettings;

                CommonSettings settings = _settingService.LoadSetting<CommonSettings>(0);

                //validation
                if (settings != null)
                {
                    _cachedSettings = settings;
                }

                return _cachedSettings;
            }
            set
            {
                _cachedSettings = value;
            }
        }

        #endregion
    }
}
