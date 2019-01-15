using AALife.Core.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AALife.Core.Services
{
    public partial class ItemService : IItemService
    {
        private readonly IRepository<ItemTable> _itemRepository;
        private readonly IDbContext _dbContext;

        public ItemService(IRepository<ItemTable> itemRepository, IDbContext dbContext)
        {
            this._itemRepository = itemRepository;
            this._dbContext = dbContext;
        }

        public virtual IPagedList<ItemTable> GetAllItem(int pageIndex = 0, int pageSize = int.MaxValue, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null, int? regionId = null)
        {
            var query = _itemRepository.Table;

            if (userId != null && userId > 0)
            {
                query = query.Where(c => c.UserID == userId && c.ItemLive == 1);
            }

            if (startDate != null)
            {
                query = query.Where(c => DateTime.Compare(c.ItemBuyDate, startDate.Value) >= 0);
            }

            if (endDate != null)
            {
                query = query.Where(c => DateTime.Compare(c.ItemBuyDate, endDate.Value) < 0);
            }

            if (regionId != null && regionId > 0)
            {
                query = query.Where(c => c.RegionID == regionId);
            }

            if (keyWords != null && keyWords != "")
            {
                query = query.Where(c => c.ItemName.Contains(keyWords));
            }

            query = query.OrderByDescending(c => c.ItemBuyDate);

            var items = new PagedList<ItemTable>(query, pageIndex, pageSize);
            return items;
        }

        public virtual IQueryable<ItemTable> GetAllItem(int userId)
        {
            return _itemRepository.Table.Where(c => c.UserID == userId && c.ItemLive == 1).OrderByDescending(c => c.ItemBuyDate);
        }

        public ItemTable GetItem(int itemId)
        {
            return _itemRepository.GetById(itemId);
        }

        public void AddItem(ItemTable model)
        {
            _itemRepository.Insert(model);
        }

        public void AddItem(IEnumerable<ItemTable> models)
        {
            _itemRepository.Insert(models);
        }

        public void UpdateItem(ItemTable model)
        {
            _itemRepository.Update(model);
        }

        public void UpdateItem(IEnumerable<ItemTable> models)
        {
            _itemRepository.Update(models);
        }

        public void DeleteItem (int id)
        {
            _itemRepository.Delete(_itemRepository.GetById(id));
        }
    }
}
