using AALife.Core;
using AALife.Core.Services;
using AALife.Data.Domain;
using System;

namespace AALife.Data.Services
{
    public interface IUserService : IBaseService<UserTable>
    {
        IPagedList<UserTable> GetAllUserByPage(int pageIndex = 0, int pageSize = int.MaxValue, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null);

        UserTable GetUserByUserName(string userName);

        UserTable Login(string userName, string userPassword);
    }
}
