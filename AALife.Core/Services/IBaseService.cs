using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AALife.Core.Services
{
    public partial interface IBaseService<T> where T : BaseEntity
    {
        T Get(int id);

        T Find(Expression<Func<T, bool>> where);

        IQueryable<T> FindAll(Expression<Func<T, bool>> where);

        void Add(T entity);

        void Add(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        void Delete(int id);

        void Delete(IEnumerable<T> entities);
    }
}
