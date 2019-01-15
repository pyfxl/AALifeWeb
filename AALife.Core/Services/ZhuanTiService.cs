using AALife.Core.Caching;
using AALife.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Core.Services
{
    public partial class ZhuanTiService : IZhuanTiService
    {
        private readonly ICacheManager _cacheManager;
        private readonly IRepository<ZhuanTiTable> _zhuanTiRepository;
        private readonly IDbContext _dbContext;

        private const string ZHUANTI_BY_UID_KEY = "aalife.zhuanti.user.{0}";

        public ZhuanTiService(ICacheManager cacheManager, 
            IRepository<ZhuanTiTable> zhuanTiRepository, 
            IDbContext dbContext)
        {
            this._cacheManager = cacheManager;
            this._zhuanTiRepository = zhuanTiRepository;
            this._dbContext = dbContext;
        }

        public virtual IList<ZhuanTiTable> GetAllZhuanTi(int userId)
        {
            string key = string.Format(ZHUANTI_BY_UID_KEY, userId);
            return _cacheManager.Get(key, () =>
            {
                var query = _zhuanTiRepository.Table;
                query = query.Where(c => c.UserID == userId && c.ZhuanTiLive == 1);
                query = query.OrderBy(c => c.ZTID);

                return query.ToList();
            });
        }

        public ZhuanTiTable GetZhuanTi(int userId, int zhuanTiId)
        {
            var query = _zhuanTiRepository.Table;
            query = query.Where(c => c.UserID == userId && c.ZTID == zhuanTiId && c.ZhuanTiLive == 1);

            return query.FirstOrDefault();
        }
    }
}
