using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Services
{
    public partial interface IBaseUserService<T> where T : UserEntity
    {
        IList<T> GetAll(int userId);

        void ClearCache(int userId);
    }
}
