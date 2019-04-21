using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using AALife.Data.Domain;
using System;
using System.Linq;

namespace AALife.Data.Services
{
    public partial class UserTitleService : BaseService<UserTitle, Guid>, IUserTitleService
    {
        public UserTitleService(IRepository<UserTitle, Guid> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
            
        }

    }
}
