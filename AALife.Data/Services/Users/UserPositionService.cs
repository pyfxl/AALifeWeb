using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using AALife.Data.Domain;
using System;
using System.Linq;

namespace AALife.Data.Services
{
    public partial class UserPositionService : BaseService<UserPosition, Guid>, IUserPositionService
    {
        public UserPositionService(IRepository<UserPosition, Guid> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
            
        }

    }
}
