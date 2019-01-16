using AALife.Core.Services;
using AALife.WebMvc.Kendoui;
using AALife.WebMvc.Models.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class ItemsApiController : ApiController
    {
        private readonly IItemService _itemService;
        private readonly ICategoryTypeService _categoryTypeService;
        private readonly ICardService _cardService;
        private readonly IZhuanTiService _zhuanTiService;

        public ItemsApiController(IItemService itemService,
            ICategoryTypeService categoryTypeService,
            ICardService cardService,
            IZhuanTiService zhuanTiService)
        {
            this._itemService = itemService;
            this._categoryTypeService = categoryTypeService;
            this._cardService = cardService;
            this._zhuanTiService = zhuanTiService;
        }

        // GET api/<controller>
        public IHttpActionResult Get([FromUri]DataSourceRequest common, [FromUri]ItemsQuery query)
        {
            var result = _itemService.GetAllItem(common.Page - 1, common.PageSize, "", "", query.userId, query.startDate, query.endDate, query.keyWords);

            var grid = new DataSourceResult
            {
                Data = result.Select(x => 
                {
                    var m = x.ToModel();
                    m.ItemTypeName = Constant.ItemTypeDic[x.ItemType];
                    m.CategoryTypeName = _categoryTypeService.GetCategoryType(x.UserID, x.CategoryTypeID).CategoryTypeName;
                    m.CardName = _cardService.GetCard(x.UserID, x.CardID).CardName;
                    m.ZhuanTiName = x.ZhuanTiID.Value > 0 ? _zhuanTiService.GetZhuanTi(x.UserID, x.ZhuanTiID.Value).ZhuanTiName : "";
                    m.RegionName = string.IsNullOrWhiteSpace(x.RegionType) ? "" : Constant.RegionTypeDic[x.RegionType];
                    return m;
                }),
                Total = result.TotalCount
            };

            return Json(grid);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}