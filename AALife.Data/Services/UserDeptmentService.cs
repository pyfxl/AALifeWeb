using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using AALife.Data.Domain;
using System;
using System.Collections.Generic;
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
        
        /// <summary>
        /// 获取多级目录
        /// </summary>
        /// <param name="deptment"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public virtual string GetFormattedBreadCrumb(UserDeptment deptment, string separator = "--")
        {
            if (deptment == null)
                throw new ArgumentNullException("deptment");

            string result = string.Empty;

            var alreadyProcessedPermissionIds = new List<int>() { };

            while (deptment != null && !alreadyProcessedPermissionIds.Contains(deptment.Id)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = deptment.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", deptment.Name, separator, result);
                }

                alreadyProcessedPermissionIds.Add(deptment.Id);

                deptment = _repository.GetById(deptment.ParentId);

            }

            return result;
        }

    }
}
