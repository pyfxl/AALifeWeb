using AALife.Core.Infrastructure.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AALife.Core.Services
{
    public partial interface IBaseService<T, TPrimaryKey> where T : BaseEntity<TPrimaryKey>
    {
        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(TPrimaryKey id);

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Get();

        /// <summary>
        /// 添加单个
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// 添加多个
        /// </summary>
        /// <param name="entities"></param>
        void Add(IEnumerable<T> entities);

        /// <summary>
        /// 更新单个
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// 更新多个
        /// </summary>
        /// <param name="entities"></param>
        void Update(IEnumerable<T> entities);

        /// <summary>
        /// 删除单个
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 根据Id删除单个
        /// </summary>
        /// <param name="id"></param>
        void Delete(TPrimaryKey id);

        /// <summary>
        /// 删除多个
        /// </summary>
        /// <param name="entities"></param>
        void Delete(IEnumerable<T> entities);

        #region 其它方法

        /// <summary>
        /// 获取全部
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetByRequest(DataSourceRequest request);

        /// <summary>
        /// 获取全部，返回分页
        /// </summary>
        /// <returns></returns>
        IPagedList<T> GetByPage(DataSourceRequest request, Expression<Func<T, bool>> where = null);

        /// <summary>
        /// 根据条件获取单个
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T Find(Expression<Func<T, bool>> where);

        /// <summary>
        /// 根据条件获取全部
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IQueryable<T> FindAll(Expression<Func<T, bool>> where);

        /// <summary>
        /// 根据条件判断是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool IsExists(Expression<Func<T, bool>> where);

        #endregion
    }

}
