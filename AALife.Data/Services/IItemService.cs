using AALife.Core;
using AALife.Core.Services;
using AALife.Data.Domain;
using AALife.Data.Infrastructure.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Data.Services
{
    public interface IItemService : IBaseUserService<ItemTable>, IBaseService<ItemTable>
    {
        IPagedList<ItemTable> GetAllItemByPage(int page = 0, int pageSize = int.MaxValue, IEnumerable<Sort> sort = null, Filter filter = null, string sidx = null, string sord = null, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null, int? regionId = null);

        IQueryable<ItemTable> GetAllItem(int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null);

        int GetMaxRegionId(int userId);
    }
}
