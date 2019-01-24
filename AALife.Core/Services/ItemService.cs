using AALife.Core.Caching;
using AALife.Core.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace AALife.Core.Services
{
    public partial class ItemService : BaseUserService<ItemTable>, IItemService
    {
        public ItemService(IRepository<ItemTable> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
        }

        public virtual IPagedList<ItemTable> GetAllItemByPage(int pageIndex = 0, int pageSize = int.MaxValue, string sortName = null, string sort = null, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null, int? regionId = null)
        {
            var query = _repository.Table;

            if (userId != null && userId > 0)
            {
                query = query.Where(c => c.UserId == userId && c.Live == 1);
            }

            if (startDate != null)
            {
                query = query.Where(c => DateTime.Compare(c.ItemBuyDate, startDate.Value) >= 0);
            }

            if (endDate != null)
            {
                query = query.Where(c => DateTime.Compare(c.ItemBuyDate, endDate.Value) < 0);
            }

            if (keyWords != null && keyWords != "")
            {
                query = query.Where(c => c.ItemName.Contains(keyWords));
            }

            if (regionId != null && regionId > 0)
            {
                query = query.Where(c => c.RegionId == regionId);
            }

            if (sortName != null && sortName != "")
            {
                query = query.OrderBy(string.Format("{0} {1}", sortName, sort));
            }
            
            var items = new PagedList<ItemTable>(query, pageIndex, pageSize);
            return items;
        }

        public virtual IQueryable<ItemTable> GetAllItem(int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null)
        {
            var query = _repository.Table;

            if (userId != null && userId > 0)
            {
                query = query.Where(c => c.UserId == userId && c.Live == 1);
            }

            if (startDate != null)
            {
                query = query.Where(c => DateTime.Compare(c.ItemBuyDate, startDate.Value) >= 0);
            }

            if (endDate != null)
            {
                query = query.Where(c => DateTime.Compare(c.ItemBuyDate, endDate.Value) < 0);
            }

            if (keyWords != null && keyWords != "")
            {
                query = query.Where(c => c.ItemName.Contains(keyWords));
            }

            return query;
        }

        //取最大id
        public int GetMaxId(int userId)
        {
            var query = _repository.Table;
            query = query.Where(c => c.UserId == userId && c.Live == 1);

            var maxId = query.Max(a => a.RegionId).GetValueOrDefault();
            maxId = maxId + 1;

            return maxId % 2 == 0 ? maxId + 1 : maxId;
        }
    }
}
