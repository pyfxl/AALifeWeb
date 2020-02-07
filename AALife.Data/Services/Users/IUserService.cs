using AALife.Core;
using AALife.Core.Services;
using AALife.Data.Domain;
using Yanzi.Core.Kendoui;
using System;
using System.Collections.Generic;

namespace AALife.Data.Services
{
    public interface IUserService : IBaseService<UserTable, Guid>
    {
        IPagedList<UserTable> GetAllUserByPage(int page = 0, int pageSize = int.MaxValue, IEnumerable<Sort> sort = null, Filter filter = null, Guid? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null);

        UserTable GetUserByUserName(string userName);

        UserTable Login(string userName, string userPassword);

    }
}
