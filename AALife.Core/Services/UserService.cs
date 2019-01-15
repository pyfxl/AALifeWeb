using AALife.Core.Domain;
using System.Linq;

namespace AALife.Core.Services
{
    public partial class UserService : IUserService
    {
        private readonly IRepository<UserTable> _userRepository;
        private readonly IDbContext _dbContext;

        public UserService(IRepository<UserTable> userRepository, IDbContext dbContext)
        {
            this._userRepository = userRepository;
            this._dbContext = dbContext;
        }

        public virtual IPagedList<UserTable> GetAllUser(string userName = "", int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _userRepository.Table;
            query = query.Where(c => c.UserName.Contains(userName));
            query = query.OrderBy(c => c.UserID);

            var users = new PagedList<UserTable>(query, pageIndex, pageSize);
            return users;
        }

        public UserTable GetUser(int userId)
        {
            return _userRepository.GetById(userId);
        }

        public virtual UserTable GetUserByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            var query = from c in _userRepository.Table
                        orderby c.UserID
                        where c.UserName == userName
                        select c;
            var user = query.FirstOrDefault();
            return user;
        }
    }
}
