using AALife.Core.Caching;
using AALife.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Core.Services
{
    public partial class CardService : BaseUserService<CardTable>,  ICardService
    {
        public CardService(IRepository<CardTable> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
        }

        public CardTable GetCard(int userId, int cardId)
        {
            var query = _repository.Table;
            query = query.Where(c => c.UserId == userId && c.Live == 1 && c.CardId == cardId);

            return query.FirstOrDefault();
        }

        //取最大id
        public int GetMaxId(int userId)
        {
            var query = _repository.Table;
            query = query.Where(c => c.UserId == userId && c.Live == 1);

            var maxId = query.Max(a => a.CardId);
            maxId = maxId + 1;

            return maxId % 2 == 0 ? maxId + 1 : maxId;
        }
    }
}
