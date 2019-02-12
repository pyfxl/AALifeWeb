using AALife.Core.Caching;
using AALife.Core.Domain.Security;
using AALife.Core.Repositorys.Security;

namespace AALife.Core.Services.Security
{
    /// <summary>
    /// Permission service
    /// </summary>
    public partial class PermissionService : BaseService<PermissionRecord>, IPermissionService
    {
        public PermissionService(IPermissionRepository repository,
               ICacheManager cacheManager,
               IDbContext dbContext)
               : base(repository, cacheManager, dbContext)
        {
        }
    }
}