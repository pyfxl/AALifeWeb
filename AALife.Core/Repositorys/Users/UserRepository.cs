using AALife.Core.Domain.Users;

namespace AALife.Core.Repositorys.Users
{
    public partial class UserRepository : EfRepository<UserTable>, IUserRepository
    {
        public UserRepository(IDbContext context) : base(context)
        {
        }
    }
}
