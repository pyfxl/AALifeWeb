using AALife.Core.Caching;
using AALife.Core.Domain.Users;
using AALife.Core.Repositorys.Users;

namespace AALife.Core.Services.Users
{
    public partial class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        public UserRoleService(IUserRoleRepository repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
        }
    }
}
