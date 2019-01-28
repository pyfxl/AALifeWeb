using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using AALife.Data.Domain;
using System;
using System.Linq;

namespace AALife.Data.Services
{
    public partial class UserService : BaseService<UserTable>, IUserService
    {
        public UserService(IRepository<UserTable> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
        }

        public virtual IPagedList<UserTable> GetAllUserByPage(int pageIndex = 0, int pageSize = int.MaxValue, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null)
        {
            var query = _repository.Table;

            if (userId != null && userId > 0)
            {
                query = query.Where(c => c.Id == userId);
            }

            if (startDate != null)
            {
                query = query.Where(c => DateTime.Compare(c.CreateDate, startDate.Value) >= 0);
            }

            if (endDate != null)
            {
                query = query.Where(c => DateTime.Compare(c.CreateDate, endDate.Value) < 0);
            }

            if (keyWords != null && keyWords != "")
            {
                query = query.Where(c => c.UserName.Contains(keyWords) || c.UserNickName.Contains(keyWords) || c.UserEmail.Contains(keyWords));
            }

            query = query.OrderByDescending(c => c.Id);

            var users = new PagedList<UserTable>(query, pageIndex, pageSize);
            return users;
        }

        public virtual UserTable GetUserByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            var query = from c in _repository.Table
                        where c.UserName == userName
                        select c;
            var user = query.FirstOrDefault();
            return user;
        }

        public UserTable Login(string userName, string userPassword)
        {
            var user = _repository.Table.FirstOrDefault(a => a.UserName == userName);
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }

            if (user.UserPassword != userPassword)
            {
                throw new Exception("密码错误！");
            }

            return user;
        }

    }
}
