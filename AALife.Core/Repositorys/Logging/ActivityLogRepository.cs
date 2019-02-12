using AALife.Core.Domain.Logging;

namespace AALife.Core.Repositorys.Configuration
{
    public partial class ActivityLogRepository : EfRepository<ActivityLog>, IActivityLogRepository
    {
        public ActivityLogRepository(IDbContext context) : base(context)
        {
        }
    }
}
