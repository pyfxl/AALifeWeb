using Yanzi.Core.Kendoui;
using AALife.Service.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace AALife.Service.Kendoui
{
    /// <summary>
    /// 服务基类实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BaseService<T> where T : class, new()
    {

        protected readonly DbContext _context;
        protected IDbSet<T> _entities;

        /// <summary>
        /// 构造
        /// </summary>
        public BaseService(DbContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T Get(Guid id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> Get()
        {
            return this.Entities.AsNoTracking();
        }

        /// <summary>
        /// 获取集合--kendo
        /// </summary>
        /// <returns></returns>
        public virtual DataSourceResult GetKendoDataSource(DataSourceRequest request, Expression<Func<T, object>> order = null)
        {
            var list = Get();
            return list.ToDataSourceResult(request, order);
        }

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<T> TableNoTracking
        {
            get
            {
                return this.Entities.AsNoTracking();
            }
        }

        /// <summary>
        /// Entities
        /// </summary>
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }

        #endregion
    }
}
