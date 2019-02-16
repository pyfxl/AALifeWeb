using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using AALife.Data.Domain;

namespace AALife.Data.Services
{
    public partial class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        public UserRoleService(IRepository<UserRole> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
            
        }

    }
}
