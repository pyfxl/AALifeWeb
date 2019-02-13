using AALife.Core.Caching;
using AALife.Core.Domain.Users;
using AALife.Core.Repositorys.Users;
using AALife.Core.Services.Security;
using System;
using System.Linq;

namespace AALife.Core.Services.Users
{
    public partial class UserService : BaseService<UserTable>, IUserService
    {
        private readonly IEncryptionService _encryptionService;

        public UserService(IUserRepository repository,
            ICacheManager cacheManager,
            IDbContext dbContext,
            IEncryptionService encryptionService)
            : base(repository, cacheManager, dbContext)
        {
            this._encryptionService = encryptionService;
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

        public UserTable GetUserByUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            var query = from c in _repository.Table
                        orderby c.Id
                        where c.UserName == userName
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        public UserTable Login(string userName, string userPassword)
        {
            var user = _repository.Table.FirstOrDefault(a => a.UserName == userName);
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }

            if (!PasswordsMatch(user, userPassword))
            {
                throw new Exception("密码错误！");
            }

            return user;
        }

        public void ResetPassword(string userName, string userPassword)
        {
            var user = GetUserByUserName(userName);
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }
            var passwordSalt = _encryptionService.CreateSaltKey(Constant.PasswordSaltSize);
            user.UserPassword = _encryptionService.CreatePasswordHash(userPassword, passwordSalt);
            user.PasswordSalt = passwordSalt;
            _repository.Update(user);
        }

        protected bool PasswordsMatch(UserTable customerPassword, string enteredPassword)
        {
            if (customerPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, customerPassword.PasswordSalt);

            return customerPassword.UserPassword.Equals(savedPassword);
        }
    }
}
