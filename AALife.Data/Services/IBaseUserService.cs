using System;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Data.Services
{
    public partial interface IBaseUserService<T> where T : UserEntity
    {
        IList<T> GetAll(Guid userId);

        void ClearCache(Guid userId);
    }
}
