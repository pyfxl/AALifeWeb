using AALife.Core;
using AALife.Core.Services;
using AALife.Data.Domain;
using System;
using System.Linq;

namespace AALife.Data.Services
{
    public interface IItemService : IBaseUserService<ItemTable>, IBaseService<ItemTable>
    {
        IPagedList<ItemTable> GetAllItemByPage(int pageIndex = 0, int pageSize = int.MaxValue, string sortName = null, string sort = null, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null, int? regionId = null);

        IQueryable<ItemTable> GetAllItem(int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null);

        int GetMaxRegionId(int userId);
    }
}
