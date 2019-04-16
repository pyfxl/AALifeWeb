using AALife.Core.Domain.Messages;

namespace AALife.Core.Repositorys.Messages
{
    public partial class QueuedEmailRepository : EfRepository<QueuedEmail, int>, IQueuedEmailRepository
    {
        public QueuedEmailRepository(IDbContext context) : base(context)
        {
        }
    }
}
