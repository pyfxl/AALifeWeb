using AALife.Core.Domain.Messages;

namespace AALife.Core.Repositorys.Messages
{
    public partial interface IQueuedEmailRepository : IRepository<QueuedEmail, int>
    {
    }
}
