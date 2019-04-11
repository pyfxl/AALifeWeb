using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using AALife.Data.Domain;
using System;
using System.Linq;

namespace AALife.Data.Services
{
    public partial class UserPositionService : BaseService<UserPosition>, IUserPositionService
    {
        public UserPositionService(IRepository<UserPosition> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
            
        }

    }
}
