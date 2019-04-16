using AALife.Core.Caching;
using AALife.Core.Domain.Logging;
using AALife.Core.Repositorys.Configuration;
using System;
using System.Linq;

namespace AALife.Core.Services.Logging
{
    /// <summary>
    /// Customer activity service
    /// </summary>
    public class CustomerActivityService : ICustomerActivityService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        private const string ACTIVITYTYPE_ALL_KEY = "aalife.activitytype.all";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string ACTIVITYTYPE_PATTERN_KEY = "aalife.activitytype.";

        #endregion

        #region Fields

        /// <summary>
        /// Cache manager
        /// </summary>
        private readonly ICacheManager _cacheManager;
        private readonly IActivityLogRepository _activityLogRepository;
        private readonly IDbContext _dbContext;
        private readonly IWebHelper _webHelper;
        #endregion
        
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="activityLogRepository">Activity log repository</param>
        /// <param name="activityLogTypeRepository">Activity log type repository</param>
        /// <param name="workContext">Work context</param>
        /// <param name="dbContext">DB context</param>>
        /// <param name="dataProvider">WeData provider</param>
        /// <param name="commonSettings">Common settings</param>
        /// <param name="webHelper">Web helper</param>
        public CustomerActivityService(ICacheManager cacheManager,
            IActivityLogRepository activityLogRepository,
            IDbContext dbContext,
            IWebHelper webHelper)
        {
            this._cacheManager = cacheManager;
            this._activityLogRepository = activityLogRepository;
            this._dbContext = dbContext;
            this._webHelper = webHelper;
        }

        #endregion

        #region Nested classes

        [Serializable]
        public class ActivityLogTypeForCaching
        {
            public int Id { get; set; }
            public string SystemKeyword { get; set; }
            public string Name { get; set; }
            public bool Enabled { get; set; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts an activity log item
        /// </summary>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="comment">The activity comment</param>
        /// <param name="commentParams">The activity comment parameters for string.Format() function.</param>
        /// <returns>Activity log item</returns>
        public virtual ActivityLog InsertActivity(ActivityLogType systemKeyword, string comment, params object[] commentParams)
        {
            return InsertActivity(null, systemKeyword, comment, commentParams);
        }

        /// <summary>
        /// Inserts an activity log item
        /// </summary>
        /// <param name="user">The user</param>
        /// <param name="systemKeyword">The system keyword</param>
        /// <param name="comment">The activity comment</param>
        /// <param name="commentParams">The activity comment parameters for string.Format() function.</param>
        /// <returns>Activity log item</returns>
        public virtual ActivityLog InsertActivity(Guid? userId, ActivityLogType systemKeyword, string comment, params object[] commentParams)
        {
            comment = CommonHelper.EnsureNotNull(comment);
            comment = string.Format(comment, commentParams);
            comment = CommonHelper.EnsureMaximumLength(comment, 4000);
            
            var activity = new ActivityLog();
            activity.ActivityLogType = systemKeyword;
            activity.Comment = comment;
            activity.UserId = userId;
            activity.CreatedDate = DateTime.Now;
            activity.IpAddress = _webHelper.GetCurrentIpAddress();

            _activityLogRepository.Insert(activity);

            return activity;
        }
        
        /// <summary>
        /// Deletes an activity log item
        /// </summary>
        /// <param name="activityLog">Activity log type</param>
        public virtual void DeleteActivity(ActivityLog activityLog)
        {
            if (activityLog == null)
                throw new ArgumentNullException("activityLog");

            _activityLogRepository.Delete(activityLog);
        }

        /// <summary>
        /// Gets all activity log items
        /// </summary>
        /// <param name="createdOnFrom">Log item creation from; null to load all activities</param>
        /// <param name="createdOnTo">Log item creation to; null to load all activities</param>
        /// <param name="customerId">Customer identifier; null to load all activities</param>
        /// <param name="activityLogTypeId">Activity log type identifier</param>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="ipAddress">IP address; null or empty to load all activities</param>
        /// <returns>Activity log items</returns>
        public virtual IPagedList<ActivityLog> GetAllActivities(DateTime? createdOnFrom = null,
            DateTime? createdOnTo = null, Guid? userId = null, int activityLogTypeId = 0,
            int pageIndex = 0, int pageSize = int.MaxValue, string ipAddress = null)
        {
            var query = _activityLogRepository.Table;
            if(!String.IsNullOrEmpty(ipAddress))
                query = query.Where(al => al.IpAddress.Contains(ipAddress));
            if (createdOnFrom.HasValue)
                query = query.Where(al => createdOnFrom.Value <= al.CreatedDate);
            if (createdOnTo.HasValue)
                query = query.Where(al => createdOnTo.Value >= al.CreatedDate);
            if (activityLogTypeId > 0)
                query = query.Where(al => activityLogTypeId == al.ActivityLogTypeId);
            if (userId.HasValue)
                query = query.Where(al => userId.Value == al.UserId);

            query = query.OrderByDescending(al => al.CreatedDate);

            var activityLog = new PagedList<ActivityLog>(query, pageIndex, pageSize);
            return activityLog;
        }
        
        /// <summary>
        /// Gets an activity log item
        /// </summary>
        /// <param name="activityLogId">Activity log identifier</param>
        /// <returns>Activity log item</returns>
        public virtual ActivityLog GetActivityById(int activityLogId)
        {
            if (activityLogId == 0)
                return null;

            return _activityLogRepository.GetById(activityLogId);
        }

        #endregion

    }
}
