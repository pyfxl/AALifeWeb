using AALife.Core.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Services
{
    public partial class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly ICacheManager _cacheManager;
        protected readonly IDbContext _dbContext;
        protected readonly IRepository<T> _repository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="dbContext"></param>
        public BaseService(IRepository<T> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
        {
            this._repository = repository;
            this._cacheManager = cacheManager;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _repository.Insert(entity);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities"></param>
        public void Add(IEnumerable<T> entities)
        {
            _repository.Insert(entities);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entities"></param>
        public void Delete(IEnumerable<T> entities)
        {
            _repository.Delete(entities);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(int id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        public void Update(IEnumerable<T> entities)
        {
            _repository.Update(entities);
        }
    }
}
