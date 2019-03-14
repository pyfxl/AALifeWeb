using AALife.Core;
using AALife.Core.Services;
using AALife.Data.Domain;
using AALife.Core.Infrastructure.Kendoui;
using System;
using System.Collections.Generic;

namespace AALife.Data.Services
{
    public interface IUserService : IBaseService<UserTable>
    {
        IPagedList<UserTable> GetAllUserByPage(int page = 0, int pageSize = int.MaxValue, IEnumerable<Sort> sort = null, Filter filter = null, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null);

        UserTable GetUserByUserName(string userName);

        UserTable Login(string userName, string userPassword);
    }
}
