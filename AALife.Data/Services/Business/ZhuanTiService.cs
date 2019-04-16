using AALife.Core;
using AALife.Core.Caching;
using AALife.Data.Domain;

namespace AALife.Data.Services
{
    public partial class ZhuanTiService : BaseUserService<ZhuanTiTable>, IZhuanTiService
    {
        public ZhuanTiService(IRepository<ZhuanTiTable, int> repository,
               ICacheManager cacheManager,
               IDbContext dbContext)
               : base(repository, cacheManager, dbContext)
        {
        }

    }
}
