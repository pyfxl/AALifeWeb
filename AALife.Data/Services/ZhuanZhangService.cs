using AALife.Core;
using AALife.Core.Caching;
using AALife.Data.Domain;

namespace AALife.Data.Services
{
    public partial class ZhuanZhangService : BaseUserService<ZhuanZhangTable>, IZhuanZhangService
    {
        public ZhuanZhangService(IRepository<ZhuanZhangTable> repository,
               ICacheManager cacheManager,
               IDbContext dbContext)
               : base(repository, cacheManager, dbContext)
        {
        }

    }
}
