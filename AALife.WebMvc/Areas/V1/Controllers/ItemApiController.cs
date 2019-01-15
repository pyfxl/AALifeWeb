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
    public class ItemApiController : ApiController
    {
        private readonly ICacheManager _cacheManager;
        private readonly IItemService _itemService;
        private readonly ICategoryTypeService _categoryTypeService;
        private readonly ICardService _cardService;
        private readonly IZhuanTiService _zhuanTiService;

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
            var all = _itemService.GetAllItem(query.userId.Value);

            var result = _itemService.GetAllItem(common.page - 1, common.rows, query.userId, query.startDate, query.endDate, query.keyWords);
            
            var grid = new DataSourceResult
            {
                rows = result.Select(x => 
                {
                    var regions = all.Where(a => a.RegionID == x.RegionID.Value).ToList();
                    var m = x.ToModel();
                    m.ItemBuyDateStart = regions.Any() ? regions.LastOrDefault().ItemBuyDate : x.ItemBuyDate;
                    m.ItemBuyDateEnd = regions.Any() ? regions.FirstOrDefault().ItemBuyDate : x.ItemBuyDate;
                    m.ItemTypeName = Constant.ItemTypeDic[x.ItemType];
                    m.CategoryTypeName = _categoryTypeService.GetCategoryType(x.UserID, x.CategoryTypeID).CategoryTypeName;
                    m.CardName = _cardService.GetCard(x.UserID, x.CardID).CardName;
                    m.ZhuanTiName = x.ZhuanTiID != null && x.ZhuanTiID.Value > 0 ? _zhuanTiService.GetZhuanTi(x.UserID, x.ZhuanTiID.Value).ZhuanTiName : "";
                    m.RegionName = string.IsNullOrWhiteSpace(x.RegionType) ? "" : Constant.RegionTypeDic[x.RegionType];
                    return m;
                }),
                records = result.TotalCount,
                total = result.TotalPages,
                page = common.page,
                userdata = new ItemTotalModel()
                {
                    ShouRuCount = result.Count(a => a.ItemType == "sr"),
                    ShouRuAmount = result.Where(a => a.ItemType == "sr").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum(),
                    ZhiChuCount = result.Count(a => a.ItemType == "zc"),
                    ZhiChuAmount = result.Where(a => a.ItemType == "zc").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum(),
                    JieRuAmount = result.Where(a => a.ItemType == "jr").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum(),
                    JieChuAmount = result.Where(a => a.ItemType == "jc").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum(),
                    HuanRuAmount = result.Where(a => a.ItemType == "hr").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum(),
                    HuanChuAmount = result.Where(a => a.ItemType == "hc").Select(a => a.ItemPrice).DefaultIfEmpty(0).Sum()
                }
            };

            return Json(grid);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post([FromBody]ItemViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.RegionType))
            {
                var table = model.ToEntity();
                table.ModifyDate = DateTime.Now;
                table.Synchronize = 1;
                table.ItemLive = 1;

                _itemService.AddItem(table);
            }
            else
            {
                var regionId = _itemService.GetAllItem(userId: model.UserID).Max(a => a.RegionID.Value);
                regionId = regionId + 1;
                model.RegionID = regionId % 2 == 0 ? regionId + 1 : regionId;

                var regions = GetRegionTables(model);

                _itemService.AddItem(regions);
            }

            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(ItemViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.RegionType))
            {
                var item = _itemService.GetItem(model.ItemID);

                var table = model.MapTo(item);
                table.ModifyDate = DateTime.Now;
                table.Synchronize = 1;

                _itemService.UpdateItem(table);
            }
            else
            {
                var all = _itemService.GetAllItem(userId: model.UserID);

                var tables = all.Where(a => a.RegionID == model.RegionID.Value).ToList();
                foreach(var table in tables)
                {
                    table.ModifyDate = DateTime.Now;
                    table.Synchronize = 1;
                    table.ItemLive = 0;
                };

                var regions = GetRegionTables(model);

                using (var ts = new TransactionScope())
                {
                    _itemService.UpdateItem(tables);
                    _itemService.AddItem(regions);
                    ts.Complete();
                }
            }

            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            //_itemService.DeleteItem(id);

            var item = _itemService.GetItem(id);
            if (string.IsNullOrWhiteSpace(item.RegionType))
            {
                item.ModifyDate = DateTime.Now;
                item.Synchronize = 1;
                item.ItemLive = 0;

                _itemService.UpdateItem(item);
            }
            else
            {
                var all = _itemService.GetAllItem(userId: item.UserID);

                var tables = all.Where(a => a.RegionID == item.RegionID.Value).ToList();
                foreach (var table in tables)
                {
                    table.ModifyDate = DateTime.Now;
                    table.Synchronize = 1;
                    table.ItemLive = 0;
                };

                _itemService.UpdateItem(tables);
            }

            return Ok();
        }

        private IEnumerable<ItemTable> GetRegionTables(ItemViewModel model)
        {
            var dates = new List<DateTime>();
            var tables = new List<ItemTable>();
            int days = GetRegionDays(model.RegionType, model.ItemBuyDateStart.Value, model.ItemBuyDateEnd.Value);
            for (int i = 0; i <= days; i++)
            {
                DateTime date = GetItemBuyDate(i, model.RegionType, model.ItemBuyDateStart.Value);
                var table = model.ToEntity();
                table.ItemID = 0;
                table.ItemBuyDate = date;
                table.ModifyDate = DateTime.Now;
                table.Synchronize = 1;
                table.ItemLive = 1;
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

    }
}