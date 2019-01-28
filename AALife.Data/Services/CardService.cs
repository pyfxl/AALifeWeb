using AALife.Core;
using AALife.Core.Caching;
using AALife.Data.Domain;

namespace AALife.Data.Services
{
    public partial class CardService : BaseUserService<CardTable>,  ICardService
    {
        public CardService(IRepository<CardTable> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
        }

    }
}
