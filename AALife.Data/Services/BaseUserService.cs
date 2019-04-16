using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Data.Services
{
    public partial class BaseUserService<T> : BaseService<T, int>, IBaseUserService<T> where T : UserEntity
    {
        private const string CACHE_KEY_USER_ID = "aalife.{0}.user.{1}";

        public BaseUserService(IRepository<T, int> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public new IQueryable<T> Get()
        {
            return _repository.Table.Where(a => a.Live == 1);
        }

        /// <summary>
        /// 取用户所有记录，有缓存
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<T> GetAll(Guid userId)
        {
            string key = string.Format(CACHE_KEY_USER_ID, typeof(T).Name.ToLower(), userId);
            return _cacheManager.Get(key, () =>
            {
                var query = _repository.Table;
                query = query.Where(c => c.UserId == userId && c.Live == 1);

                return query.ToList();
            });
        }

        /// <summary>
        /// 清除用户所有记录缓存
        /// </summary>
        /// <param name="userId"></param>
        public void ClearCache(Guid userId)
        {
            string key = string.Format(CACHE_KEY_USER_ID, typeof(T).Name.ToLower(), userId);
            _cacheManager.Remove(key);
        }
    }
}
