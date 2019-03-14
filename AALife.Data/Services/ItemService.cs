using AALife.Core;
using AALife.Core.Caching;
using AALife.Data.Domain;
using AALife.Core.Infrastructure.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace AALife.Data.Services
{
    public partial class ItemService : BaseUserService<ItemTable>, IItemService
    {
        public ItemService(IRepository<ItemTable> repository,
            ICacheManager cacheManager,
            IDbContext dbContext)
            : base(repository, cacheManager, dbContext)
        {
        }

        public virtual IPagedList<ItemTable> GetAllItemByPage(int page = 0, int pageSize = int.MaxValue, IEnumerable<Sort> sort = null, Filter filter = null, string sidx = null, string sord = null, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null, int? regionId = null)
        {
            var query = _repository.Table;

            if (filter != null)
            {
                query = query.Filter(filter);
            }

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

            if (sidx != null && sidx != "")
            {
                query = query.OrderBy(string.Format("{0} {1}", sidx, sord));
            }

            if (sort != null)
            {
                query = query.Sort(sort);
            }

            var items = new PagedList<ItemTable>(query, page, pageSize);
            return items;
        }

        public virtual IPagedList<ItemTable> GetAllItemByPage(int page = 0, int pageSize = int.MaxValue, int? userId = null, DateTime? startDate = null, DateTime? endDate = null, string keyWords = null, int? regionId = null, IEnumerable<Sort> sort = null, Filter filter = null)
        {
            var query = _repository.Table;

            if (filter != null)
            {
                query = query.Filter(filter);
            }

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

            if (sort != null)
            {
                query = query.Sort(sort);
            }
            else
            {
                query = query.OrderByDescending(c => c.Id);
            }

            var items = new PagedList<ItemTable>(query, page, pageSize);
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
        public int GetMaxRegionId(int userId)
        {
            var query = _repository.Table;
            query = query.Where(c => c.UserId == userId && c.Live == 1);

            var maxId = query.Max(a => a.RegionId).GetValueOrDefault();
            maxId = maxId + 1;

            return maxId % 2 == 0 ? maxId + 1 : maxId;
        }
    }
}
