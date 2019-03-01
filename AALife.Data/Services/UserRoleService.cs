using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using AALife.Data.Domain;
using System;
using System.Linq;

namespace AALife.Data.Services
{
    public partial class UserRoleService : BaseService<UserRole>, IUserRoleService
    {
        public UserRoleService(IRepository<UserRole> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
            
        }

        /// <summary>
        /// Gets a customer role
        /// </summary>
        /// <param name="systemName">Customer role system name</param>
        /// <returns>Customer role</returns>
        public virtual UserRole GetUserRoleBySystemName(string systemName)
        {
            if (String.IsNullOrWhiteSpace(systemName))
                return null;

            var query = from cr in _repository.Table
                        orderby cr.Id
                        where cr.SystemName == systemName
                        select cr;
            var customerRole = query.FirstOrDefault();
            return customerRole;
        }

    }
}
