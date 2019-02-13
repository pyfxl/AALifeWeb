using AALife.Core.Domain.Users;

namespace AALife.Core.Repositorys.Users
{
    public partial class UserRoleRepository : EfRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IDbContext context) : base(context)
        {
        }
    }
}
