using AALife.Core.Caching;
using AALife.Core.Domain;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Core.Services
{
    public partial class ZhuanTiService : BaseUserService<ZhuanTiTable>, IZhuanTiService
    {
        public ZhuanTiService(IRepository<ZhuanTiTable> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
        }

        public ZhuanTiTable GetZhuanTi(int userId, int zhuanTiId)
        {
            var query = _repository.Table;
            query = query.Where(c => c.UserId == userId && c.Live == 1 && c.ZhuanTiId == zhuanTiId);

            return query.FirstOrDefault();
        }

        //取最大id
        public int GetMaxId(int userId)
        {
            var query = _repository.Table;
            query = query.Where(c => c.UserId == userId && c.Live == 1);

            var maxId = query.Max(a => a.ZhuanTiId).GetValueOrDefault();
            maxId = maxId + 1;

            return maxId % 2 == 0 ? maxId + 1 : maxId;
        }
    }
}
