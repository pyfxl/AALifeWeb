﻿using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using AALife.Core.Services.Security;
using AALife.Data.Domain;
using System;
using System.Linq;

namespace AALife.Data.Services
{
    public partial class UserService : BaseService<UserTable>, IUserService
    {
        private readonly IEncryptionService _encryptionService;

        public UserService(IRepository<UserTable> repository,
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

            if (!PasswordsMatch(user, userPassword))
            {
                throw new Exception("密码错误！");
            }

            return user;
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
