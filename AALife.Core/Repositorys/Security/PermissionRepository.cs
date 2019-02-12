using AALife.Core.Domain.Security;

namespace AALife.Core.Repositorys.Security
{
    public partial class PermissionRepository : EfRepository<PermissionRecord>, IPermissionRepository
    {
        public PermissionRepository(IDbContext context) : base(context)
        {
        }
    }
}
