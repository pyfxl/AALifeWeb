using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using AALife.Data.Domain;
using System;
using System.Linq;

namespace AALife.Data.Services
{
    public partial class UsersPositionsService : BaseService<UsersPositions, Guid>, IUsersPositionsService
    {
        public UsersPositionsService(IRepository<UsersPositions, Guid> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
            
        }

    }
}
