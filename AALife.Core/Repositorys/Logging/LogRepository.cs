using AALife.Core.Domain.Logging;

namespace AALife.Core.Repositorys.Configuration
{
    public partial class LogRepository : EfRepository<Log>, ILogRepository
    {
        public LogRepository(IDbContext context) : base(context)
        {
        }
    }
}
