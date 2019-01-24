using AALife.Core.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Services
{
    public partial class BaseUserService<T> : BaseService<T>, IBaseUserService<T> where T : UserEntity
    {
        private const string CACHE_KEY_USER_ID = "aalife.{0}.user.{1}";

        public BaseUserService(IRepository<T> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            :base(repository, cacheManager, dbContext)
        {
        }

        public IList<T> GetAll(int userId)
        {
            string key = string.Format(CACHE_KEY_USER_ID, typeof(T).Name, userId);
            return _cacheManager.Get(key, () =>
            {
                var query = _repository.Table;
                query = query.Where(c => c.UserId == userId && c.Live == 1);

                return query.ToList();
            });
        }

        public void ClearCache(int userId)
        {
            string key = string.Format(CACHE_KEY_USER_ID, typeof(T).Name, userId);
            _cacheManager.Remove(key);
        }
    }
}
