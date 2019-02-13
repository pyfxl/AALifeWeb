using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Domain.Logging;
using AALife.Core.Services.Logging;
using AALife.Data;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.jqGrid;
using AALife.WebMvc.Models.Query;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class ItemApiController : BaseApiController
    {
        private readonly ICacheManager _cacheManager;
        private readonly IItemService _itemService;
        private readonly ICategoryTypeService _categoryTypeService;
        private readonly ICardService _cardService;
        private readonly IZhuanTiService _zhuanTiService;
        private readonly ICustomerActivityService _customerActivityService;

        public ItemApiController(ICacheManager cacheManager, 
            IItemService itemService,
            ICategoryTypeService categoryTypeService,
            ICardService cardService,
            IZhuanTiService zhuanTiService,
            ICustomerActivityService customerActivityService)
        {
            this._cacheManager = cacheManager;
            this._itemService = itemService;
            this._categoryTypeService = categoryTypeService;
            this._cardService = cardService;
            this._zhuanTiService = zhuanTiService;
            this._customerActivityService = customerActivityService;
        }

        // GET api/<controller>
        public IHttpActionResult Get([FromUri]DataSourceRequest common, [FromUri]ItemsQuery query)
        {
            var totals = _itemService.GetAllItem(query.userId, query.startDate, query.endDate, query.keyWords);
            var result = _itemService.GetAllItemByPage(common.page - 1, common.rows, common.sidx, common.sord, query.userId, query.startDate, query.endDate, query.keyWords);
            
            var grid = new DataSourceResult
            {
                rows = result.Select(x => 
                {
                    var m = x.ToModel();
                    UpdateNameValue(m);
                    var region = GetRegion(x);
                    if (region != null)
                    {
                        m.RegionName = region.Item1;
                        m.ItemBuyDateStart = region.Item2;
                        m.ItemBuyDateEnd = region.Item3;
                    }
                    return m;
                }),
                records = result.TotalCount,
                total = result.TotalPages,
                page = common.page,
                userdata = new ItemTotalModel()
                {
                    ShouRuCount = totals.Count(a => a.ItemType == "sr"),
                    ShouRuAmount = totals.Where(a => a.ItemType == "sr").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum(),
                    ZhiChuCount = totals.Count(a => a.ItemType == "zc"),
                    ZhiChuAmount = totals.Where(a => a.ItemType == "zc").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum(),
                    JieRuAmount = totals.Where(a => a.ItemType == "jr").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum(),
                    JieChuAmount = totals.Where(a => a.ItemType == "jc").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum(),
                    HuanRuAmount = totals.Where(a => a.ItemType == "hr").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum(),
                    HuanChuAmount = totals.Where(a => a.ItemType == "hc").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum()
                }
            };

            //activity log
            _customerActivityService.InsertActivity(query.userId, ActivityLogType.Query, "浏览消费列表记录。{0} {1}", common.ToJson(), query.ToJson());

            return Json(grid);
        }

        // GET api/<controller>
        [Route("api/v1/itemsapi")]
        public IHttpActionResult GetItems([FromUri]Kendoui.DataSourceRequest common, [FromUri]ItemsQuery query)
        {
            var result = _itemService.GetAllItemByPage(common.Page - 1, common.PageSize, "Id", "desc", query.userId, query.startDate, query.endDate, query.keyWords);

            var grid = new Kendoui.DataSourceResult
            {
                Data = result.Select(x =>
                {
                    var m = x.ToModel();
                    UpdateNameValue(m);
                    var region = GetRegion(x);
                    if (region != null)
                    {
                        m.RegionName = region.Item1;
                    }
                    return m;
                }),
                Total = result.TotalCount
            };

            return Json(grid);
        }

        // GET api/<controller>
        [Route("api/v1/itemnamesapi/{id}")]
        public IHttpActionResult GetItemNames(int id, string term)
        {
            var all = _itemService.GetAllItem(id);

            if (term != null && term != "")
            {
                all = all.Where(a => a.ItemName.Contains(term));
            }

            var item = all.GroupBy(a => a.ItemName)
                .Select(a => new { Count = a.Count(), ItemName = a.Key, Index = a.Key.IndexOf(term) })
                .OrderBy(a => a.Index).ThenByDescending(a => a.Count)
                .Select(a => a.ItemName)
                .Skip(0).Take(10)
                .ToArray();

            return Json(item);
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]ItemViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.RegionType))
            {
                model.RegionId = _itemService.GetMaxRegionId(model.UserId);

                var regions = GetRegionTables(model);

                _itemService.Add(regions);
            }
            else
            {
                var table = model.ToEntity();
                table.LiveOn();

                _itemService.Add(table);
            }

            _itemService.ClearCache(model.UserId);

            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]ItemViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.RegionType))
            {
                var tables = new List<ItemTable>();
                //修改普通消费到区间消费的判断
                if (!model.RegionId.IsNullOrDefault())
                {
                    tables = _itemService.FindAll(a => a.UserId == model.UserId && a.Live == 1 && a.RegionId == model.RegionId).ToList();
                }
                else
                {
                    var m = _itemService.Get(model.Id);
                    tables.Add(m);

                    model.RegionId = _itemService.GetMaxRegionId(model.UserId);
                }

                //删除旧记录
                foreach (var table in tables)
                {
                    table.LiveOff();
                }

                //获取新的区间记录
                var regions = GetRegionTables(model);

                using (var ts = new TransactionScope())
                {
                    _itemService.Update(tables);
                    _itemService.Add(regions);
                    ts.Complete();
                }
            }
            else
            {
                var item = _itemService.Get(model.Id);

                var table = model.MapTo(item);
                table.LiveOn();

                _itemService.Update(table);
            }

            _itemService.ClearCache(model.UserId);

            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var model = _itemService.Get(id);
            if (!string.IsNullOrWhiteSpace(model.RegionType))
            {
                //TODO: 有缓存会导致更新失败
                var tables = _itemService.FindAll(a => a.UserId == model.UserId && a.Live == 1 && a.RegionId == model.RegionId).ToList();
                foreach (var table in tables)
                {
                    table.LiveOff();
                }

                _itemService.Update(tables);
            }
            else
            {
                model.LiveOff();

                _itemService.Update(model);
            }

            _itemService.ClearCache(model.UserId);

            return Ok();
        }

        #region 私有方法

        //更新字段值
        private void UpdateNameValue(ItemViewModel m)
        {
            m.CategoryTypeName = _categoryTypeService.Find(a => a.UserId == m.UserId && a.Live == 1 && a.CategoryTypeId == m.CategoryTypeId)?.CategoryTypeName;
            m.CardName = _cardService.Find(a => a.UserId == m.UserId && a.Live == 1 && a.CardId == m.CardId)?.CardName;
            m.ZhuanTiName = _zhuanTiService.Find(a => a.UserId == m.UserId && a.Live == 1 && a.ZhuanTiId == m.ZhuanTiId)?.ZhuanTiName;
        }

        //取区间的最大最小日期
        private Tuple<string, DateTime, DateTime> GetRegion(ItemTable model)
        {
            if (model.RegionId.IsNullOrDefault()) return null;

            //已经缓存了
            var all = _itemService.GetAll(model.UserId);

            var region = all.Where(a => a.RegionId == model.RegionId.Value).GroupBy(a => a.RegionId).Select(a => new { MinDate = a.Min(b => b.ItemBuyDate), MaxDate = a.Max(b => b.ItemBuyDate) }).FirstOrDefault();
            if (region == null) return null;

            var regionName = AALife.Data.Constant.RegionTypeDic[model.RegionType];

            return new Tuple<string, DateTime, DateTime>(regionName, region.MinDate, region.MaxDate);
        }

        //取固定消费日期区间
        private IEnumerable<ItemTable> GetRegionTables(ItemViewModel model)
        {
            var dates = new List<DateTime>();
            var tables = new List<ItemTable>();
            int days = GetRegionDays(model.RegionType, model.ItemBuyDateStart.Value, model.ItemBuyDateEnd.Value);
            for (int i = 0; i <= days; i++)
            {
                DateTime date = GetItemBuyDate(i, model.RegionType, model.ItemBuyDateStart.Value);
                if (!IsWorkDay(date, 5)) continue;
                var table = model.ToEntity();
                table.LiveOn();
                table.Id = 0;
                table.ItemBuyDate = date;
                tables.Add(table);
            }
            return tables;
        }

        //取两日日期的区间最大数
        private int GetRegionDays(string retionType, DateTime date1, DateTime date2)
        {
            int result = 0;

            switch (retionType)
            {
                case "d":
                case "b":
                    result = ((TimeSpan)(date2 - date1)).Days;
                    break;
                case "w":
                    result = (int)Math.Floor(((TimeSpan)(date2 - date1)).Days / 7.0);
                    break;
                case "m":
                    result = ((date2.Year - date1.Year) * 12) + (date2.Month - date1.Month);
                    break;
                case "j":
                    result = (4 * (date2.Year - date1.Year)) + ((int)Math.Ceiling(date2.Month / 3.0)) - ((int)Math.Ceiling(date1.Month / 3.0));
                    break;
                case "y":
                    result = (date2.Year - date1.Year);
                    break;
            }

            return result;
        }

        //取购买日期
        private DateTime GetItemBuyDate(int i, string regionType, DateTime itemBuyDate1)
        {
            DateTime itemBuyDate = itemBuyDate1;

            switch (regionType)
            {
                case "d":
                case "b":
                    itemBuyDate = itemBuyDate1.AddDays(i);
                    break;
                case "w":
                    itemBuyDate = itemBuyDate1.AddDays(i * 7);
                    break;
                case "m":
                    itemBuyDate = itemBuyDate1.AddMonths(i);
                    break;
                case "j":
                    itemBuyDate = itemBuyDate1.AddMonths(i * 3);
                    break;
                case "y":
                    itemBuyDate = itemBuyDate1.AddYears(i);
                    break;
            }

            return itemBuyDate;
        }

        //判断是否工作日
        private bool IsWorkDay(DateTime date, int day)
        {
            int week = Convert.ToInt32(date.DayOfWeek);
            switch (day)
            {
                case 1:
                    if (week != 1) return false;
                    break;
                case 2:
                    if (week > 2 || week == 0) return false;
                    break;
                case 3:
                    if (week > 3 || week == 0) return false;
                    break;
                case 4:
                    if (week > 4 || week == 0) return false;
                    break;
                case 5:
                    if (week > 5 || week == 0) return false;
                    break;
                case 6:
                    if (week == 0) return false;
                    break;
            }

            return true;
        }

        #endregion
    }
}