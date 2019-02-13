using AALife.Core.Domain.Users;
using System;

namespace AALife.Core.Services.Users
{
    public partial interface IUserService : IBaseService<UserTable>
    {
        IPagedList<UserTable> GetAllUserByPage(int pageIndex = 0, int pageSize = int.MaxValue, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null);

        UserTable GetUserByUserName(string userName);

        UserTable Login(string userName, string userPassword);

        void ResetPassword(string userName, string userPassword);
    }
}
