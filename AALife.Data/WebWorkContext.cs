using AALife.Core.Domain.Configuration;
using AALife.Core.Services.Configuration;
using AALife.Data.Authentication;
using AALife.Data.Domain;
using AALife.Data.Services;
using System;

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
        private readonly ISettingService _settingService;

        private UserTable _cachedUser;
        private UserSettings _cachedUserSettings;
        private DefaultSettings _cachedDefaultSettings;

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
        /// user settings
        /// </summary>
        public virtual UserSettings UserSettings
        {
            get
            {
                if (_cachedUserSettings != null)
                    return _cachedUserSettings;

                if (CurrentUser == null)
                    return null;

                UserSettings settings = _settingService.LoadSetting<UserSettings>(CurrentUser.Id);

                //validation
                if (settings != null)
                {
                    _cachedUserSettings = settings;
                }

                return _cachedUserSettings;
            }
            set
            {
                _cachedUserSettings = value;
            }
        }

        /// <summary>
        /// default settings
        /// </summary>
        public virtual DefaultSettings DefaultSettings
        {
            get
            {
                if (_cachedDefaultSettings != null)
                    return _cachedDefaultSettings;

                DefaultSettings settings = _settingService.LoadSetting<DefaultSettings>(default(Guid));

                //validation
                if (settings != null)
                {
                    _cachedDefaultSettings = settings;
                }

                return _cachedDefaultSettings;
            }
            set
            {
                _cachedDefaultSettings = value;
            }
        }

        #endregion
    }
}
