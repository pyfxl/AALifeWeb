using AALife.Core.Caching;
using AALife.Core.Domain.Customers;
using AALife.Core.Repositorys.Customers;
using AALife.Core.Services.Security;
using System;
using System.Linq;

namespace AALife.Core.Services
{
    public partial class CustomerService : BaseService<Customer>, ICustomerService
    {
        private readonly IEncryptionService _encryptionService;

        public CustomerService(ICustomerRepository repository,
            ICacheManager cacheManager,
            IDbContext dbContext,
            IEncryptionService encryptionService)
            : base(repository, cacheManager, dbContext)
        {
            this._encryptionService = encryptionService;
        }

        public Customer GetCustomerByUsername(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return null;

            var query = from c in _repository.Table
                        orderby c.Id
                        where c.Username == userName
                        select c;
            var customer = query.FirstOrDefault();
            return customer;
        }

        public Customer Login(string userName, string userPassword)
        {
            var user = _repository.Table.FirstOrDefault(a => a.Username == userName);
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }

            if (user.ResetPassword)
            {
                return user;
            }

            if (!PasswordsMatch(user, userPassword))
            {
                throw new Exception("密码错误！");
            }

            return user;
        }

        public void ResetPassword(string userName, string userPassword)
        {
            var user = GetCustomerByUsername(userName);
            if (user == null)
            {
                throw new Exception("用户不存在！");
            }
            var passwordSalt = _encryptionService.CreateSaltKey(Constant.PasswordSaltSize);
            user.Password = _encryptionService.CreatePasswordHash(userPassword, passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.ResetPassword = false;
            _repository.Update(user);
        }

        protected bool PasswordsMatch(Customer customerPassword, string enteredPassword)
        {
            if (customerPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = _encryptionService.CreatePasswordHash(enteredPassword, customerPassword.PasswordSalt);

            return customerPassword.Password.Equals(savedPassword);
        }
    }
}
