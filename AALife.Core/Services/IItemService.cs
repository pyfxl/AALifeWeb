using AALife.Core.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Core.Services
{
    public interface IItemService
    {
        IPagedList<ItemTable> GetAllItem(int pageIndex = 0, int pageSize = int.MaxValue, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null, int? regionId = null);

        IQueryable<ItemTable> GetAllItem(int userId);

        ItemTable GetItem(int itemId);

        void AddItem(ItemTable model);

        void AddItem(IEnumerable<ItemTable> models);

        void UpdateItem(ItemTable model);

        void UpdateItem(IEnumerable<ItemTable> models);

        void DeleteItem(int id);
    }
}
