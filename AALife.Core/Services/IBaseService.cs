using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Services
{
    public partial interface IBaseService<T> where T : BaseEntity
    {
        T Get(int id);

        void Add(T entity);

        void Add(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        void Delete(int id);

        void Delete(IEnumerable<T> entities);
    }
}
