using AALife.Core.Authentication;
using AALife.Core.Domain.Configuration;
using AALife.Core.Domain.Customers;
using AALife.Core.Services;
using AALife.Core.Services.Configuration;

namespace AALife.Core
{
    /// <summary>
    /// Work context for web application
    /// </summary>
    public partial class WebWorkContext : IWorkContext
    {
        #region Fields

        private readonly ICustomerService _customerService;
        private readonly IAuthenticationService _authenticationService;
        private readonly ISettingService _settingService;

        private Customer _cachedCustomer;
        private CommonSettings _cachedSettings;

        #endregion

        #region Ctor

        public WebWorkContext(ICustomerService customerService,
            IAuthenticationService authenticationService,
            ISettingService settingService)
        {
            this._customerService = customerService;
            this._authenticationService = authenticationService;
            this._settingService = settingService;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current user
        /// </summary>
        public virtual Customer CurrentCustomer
        {
            get
            {
                if (_cachedCustomer != null)
                    return _cachedCustomer;

                Customer customer = _authenticationService.GetAuthenticatedCustomer();

                //validation
                if (customer != null)
                {
                    _cachedCustomer = customer;
                }

                return _cachedCustomer;
            }
            set
            {
                _cachedCustomer = value;
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
