using AALife.Core.Domain.Tasks;

namespace AALife.Core.Repositorys.Messages
{
    public partial class ScheduleTaskRepository : EfRepository<ScheduleTask>, IScheduleTaskRepository
    {
        public ScheduleTaskRepository(IDbContext context) : base(context)
        {
        }
    }
}
