using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Domain;
using AALife.Core.Services;
using AALife.WebMvc.jqGrid;
using AALife.WebMvc.Models.Query;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        private const string ITEM_ALL_USER = "aalife.item.user.{0}";

        public ItemApiController(ICacheManager cacheManager, 
            IItemService itemService,
            ICategoryTypeService categoryTypeService,
            ICardService cardService,
            IZhuanTiService zhuanTiService)
        {
            this._cacheManager = cacheManager;
            this._itemService = itemService;
            this._categoryTypeService = categoryTypeService;
            this._cardService = cardService;
            this._zhuanTiService = zhuanTiService;
        }

        // GET api/<controller>
        public IHttpActionResult Get([FromUri]DataSourceRequest common, [FromUri]ItemsQuery query)
        {
            string key = string.Format(ITEM_ALL_USER, query.userId.Value);
            var all = _cacheManager.Get(key, () =>
            {
                return _itemService.GetAllItem(userId: query.userId).ToList();
            });
            var totals = _itemService.GetAllItem(query.userId, query.startDate, query.endDate, query.keyWords);
            var result = _itemService.GetAllItemByPage(common.page - 1, common.rows, common.sidx, common.sord, query.userId, query.startDate, query.endDate, query.keyWords);
            
            var grid = new DataSourceResult
            {
                rows = result.Select(x => 
                {
                    var region = !x.RegionId.IsNullOrDefault() ? all.Where(a => a.RegionId == x.RegionId.Value).GroupBy(a => a.RegionId).Select(a => new { MinDate = a.Min(b => b.ItemBuyDate), MaxDate = a.Max(b => b.ItemBuyDate) }).FirstOrDefault() : null;
                    var m = x.ToModel();
                    m.ItemBuyDateStart = region != null ? region.MinDate : x.ItemBuyDate;
                    m.ItemBuyDateEnd = region != null ? region.MaxDate : x.ItemBuyDate;
                    m.ItemTypeName = Constant.ItemTypeDic[x.ItemType];
                    m.CategoryTypeName = !x.CategoryTypeId.IsNullOrDefault() ? _categoryTypeService.GetCategoryType(x.UserId, x.CategoryTypeId.Value).CategoryTypeName : "";
                    m.CardName = _cardService.GetCard(x.UserId, x.CardId).CardName;
                    m.ZhuanTiName = !x.ZhuanTiId.IsNullOrDefault() ? _zhuanTiService.GetZhuanTi(x.UserId, x.ZhuanTiId.Value).ZhuanTiName : "";
                    m.RegionName = string.IsNullOrWhiteSpace(x.RegionType) ? "" : Constant.RegionTypeDic[x.RegionType];
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
                    m.ItemTypeName = Constant.ItemTypeDic[x.ItemType];
                    m.CategoryTypeName = !x.CategoryTypeId.IsNullOrDefault() ? _categoryTypeService.GetCategoryType(x.UserId, x.CategoryTypeId.Value).CategoryTypeName : "";
                    m.CardName = _cardService.GetCard(x.UserId, x.CardId).CardName;
                    m.ZhuanTiName = !x.ZhuanTiId.IsNullOrDefault() ? _zhuanTiService.GetZhuanTi(x.UserId, x.ZhuanTiId.Value).ZhuanTiName : "";
                    m.RegionName = string.IsNullOrWhiteSpace(x.RegionType) ? "" : Constant.RegionTypeDic[x.RegionType];
                    return m;
                }),
                Total = result.TotalCount
            };

            return Json(grid);
        }

        // GET api/<controller>
        [Route("api/v1/itemnamesapi")]
        public IHttpActionResult GetItemNames(int id, string term)
        {
            var all = _itemService.GetAllItem(id);

            all = all.Where(a => a.Live == 1);

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
                model.RegionId = _itemService.GetMaxId(model.UserId);

                var regions = GetRegionTables(model);

                _itemService.Add(regions);
            }
            else
            {
                var table = model.ToEntity();
                table.UpdateField();

                _itemService.Add(table);
            }

            string key = string.Format(ITEM_ALL_USER, model.UserId);
            _cacheManager.Remove(key);

            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put([FromBody]ItemViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.RegionType))
            {
                var all = _itemService.GetAllItem(userId: model.UserId);

                var tables = all.Where(a => a.RegionId == model.RegionId.Value).ToList();
                foreach (var table in tables)
                {
                    table.UpdateField(0);
                };

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
                table.UpdateField();

                _itemService.Update(table);
            }

            string key = string.Format(ITEM_ALL_USER, model.UserId);
            _cacheManager.Remove(key);

            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var item = _itemService.Get(id);
            if (!string.IsNullOrWhiteSpace(item.RegionType))
            {
                var all = _itemService.GetAllItem(userId: item.UserId);

                var tables = all.Where(a => a.RegionId == item.RegionId.Value).ToList();
                foreach (var table in tables)
                {
                    table.UpdateField(0);
                };

                _itemService.Update(tables);
            }
            else
            {
                item.UpdateField(0);

                _itemService.Update(item);
            }

            return Ok();
        }

        #region 私有方法

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
                table.Id = 0;
                table.ItemBuyDate = date;
                table.ModifyDate = DateTime.Now;
                table.Synchronize = 1;
                table.Live = 1;
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