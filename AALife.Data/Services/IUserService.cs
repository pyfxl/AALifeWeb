using AALife.Core;
using AALife.Core.Services;
using AALife.Data.Domain;
using AALife.Data.Infrastructure.Kendoui;
using System;
using System.Collections.Generic;

namespace AALife.Data.Services
{
    public interface IUserService : IBaseService<UserTable>
    {
        IPagedList<UserTable> GetAllUserByPage(int page = 0, int pageSize = int.MaxValue, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null, IEnumerable<Sort> sort = null, Filter filter = null);

        UserTable GetUserByUserName(string userName);

        UserTable Login(string userName, string userPassword);
    }
}
