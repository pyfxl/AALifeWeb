using System.Collections.Generic;

namespace AALife.Data.Services
{
    public partial interface IBaseUserService<T> where T : UserEntity
    {
        IList<T> GetAll(int userId);

        void ClearCache(int userId);
    }
}
