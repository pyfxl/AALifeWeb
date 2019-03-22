using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using AALife.Data.Domain;
using System;
using System.Linq;

namespace AALife.Data.Services
{
    public partial class UserDeptmentService : BaseService<UserDeptment>, IUserDeptmentService
    {
        public UserDeptmentService(IRepository<UserDeptment> repository,
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
        public virtual UserDeptment GetUserDeptmentByName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return null;

            var query = from cr in _repository.Table
                        orderby cr.Id
                        where cr.Name == name
                        select cr;
            var deptment = query.FirstOrDefault();
            return deptment;
        }

    }
}
