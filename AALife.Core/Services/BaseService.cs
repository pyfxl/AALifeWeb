﻿using AALife.Core.Caching;
using Yanzi.Core.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;

namespace AALife.Core.Services
{
    public partial class BaseService<T, TPrimaryKey> : IBaseService<T, TPrimaryKey> where T : BaseEntity<TPrimaryKey>
    {
        protected readonly ICacheManager _cacheManager;
        protected readonly IDbContext _dbContext;
        protected readonly IRepository<T, TPrimaryKey> _repository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="cacheManager"></param>
        /// <param name="dbContext"></param>
        public BaseService(IRepository<T, TPrimaryKey> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
        {
            this._repository = repository;
            this._cacheManager = cacheManager;
            this._dbContext = dbContext;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(TPrimaryKey id)
        {
            return _repository.GetById(id);
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<T> Get()
        {
            return _repository.Table;
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        public void Delete(TPrimaryKey id)
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

        #region 其它方法

        /// <summary>
        /// 获取分页集合
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public IQueryable<T> GetByRequest(DataSourceRequest request)
        {
            var query = _repository.Table;

            if (request.Filter != null)
            {
                query = query.Filter(request.Filter);
            }

            if (request.Sort != null)
            {
                query = query.Sort(request.Sort);
            }
            else
            {
                query = query.OrderBy("Id desc");
            }

            return query;
        }

        /// <summary>
        /// 获取分页集合
        /// </summary>
        /// <param name="request"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IPagedList<T> GetByPage(DataSourceRequest request, Expression<Func<T, bool>> where = null)
        {
            var query = _repository.Table;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (request.Filter != null)
            {
                query = query.Filter(request.Filter);
            }

            if (request.Sort != null)
            {
                query = query.Sort(request.Sort);
            }
            else
            {
                query = query.OrderBy("Id desc");
            }

            var results = new PagedList<T>(query, request.Page - 1, request.PageSize);
            return results;
        }

        /// <summary>
        /// 条件获取实体
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Find(Expression<Func<T, bool>> where)
        {
            return _repository.Table.Where(where).SingleOrDefault();
        }

        /// <summary>
        /// 条件获取集合
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IQueryable<T> FindAll(Expression<Func<T, bool>> where)
        {
            return _repository.Table.Where(where);
        }

        /// <summary>
        /// 根据条件判断是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public bool IsExists(Expression<Func<T, bool>> where)
        {
            return _repository.Table.Any(where);
        }

        #endregion
    }
}
