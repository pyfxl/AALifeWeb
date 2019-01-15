using AALife.Core.Caching;
using AALife.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Core.Services
{
    public partial class CardService : ICardService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<CardTable> _cardRepository;
        private readonly IDbContext _dbContext;

        private const string CARD_BY_UID_KEY = "aalife.card.user.{0}";

        public CardService(ICacheManager cacheManager, 
            IRepository<CardTable> cardRepository, 
            IDbContext dbContext)
        {
            this._cacheManager = cacheManager;
            this._cardRepository = cardRepository;
            this._dbContext = dbContext;
        }

        public virtual IList<CardTable> GetAllCard(int userId)
        {
            string key = string.Format(CARD_BY_UID_KEY, userId);
            return _cacheManager.Get(key, () =>
            {
                var query = _cardRepository.Table;
                query = query.Where(c => c.UserID == userId && c.CardLive == 1);
                query = query.OrderBy(c => c.CDID);

                return query.ToList();
            });
        }

        public CardTable GetCard(int userId, int cardId)
        {
            var query = _cardRepository.Table;
            query = query.Where(c => c.UserID == userId && c.CDID == cardId && c.CardLive == 1);
            
            return query.FirstOrDefault();
        }
    }
}
