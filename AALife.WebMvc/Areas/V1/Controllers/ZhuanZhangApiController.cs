using AALife.Data;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.jqGrid;
using AALife.WebMvc.Models.ViewModel;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class ZhuanZhangApiController : BaseApiController
    {
        private readonly IZhuanZhangService _zhuanZhangService;
        private readonly ICardService _cardService;

        public ZhuanZhangApiController(IZhuanZhangService zhuanZhangService,
            ICardService cardService)
        {
            this._zhuanZhangService = zhuanZhangService;
            this._cardService = cardService;
        }

        // GET api/<controller>
        public IHttpActionResult Get(int id)
        {
            var result = _zhuanZhangService.GetAll(id);
            var grid = new DataSourceResult
            {
                rows = result.Select(x =>
                {
                    var m = x.MapTo<ZhuanZhangTable, ZhuanZhangViewModel>();
                    m.ZhuanZhangFromName = _cardService.Find(a => a.UserId == m.UserId && a.Live == 1 && a.CardId == m.ZhuanZhangFrom)?.CardName;
                    m.ZhuanZhangToName = _cardService.Find(a => a.UserId == m.UserId && a.Live == 1 && a.CardId == m.ZhuanZhangTo)?.CardName;
                    return m;
                }),
                records = result.Count()
            };
            return Json(grid);
        }

        // POST api/<controller>
        public IHttpActionResult Post(int id, ZhuanZhangTable model)
        {
            model.LiveOn();
            model.UserId = id;

            _zhuanZhangService.Add(model);
            _zhuanZhangService.ClearCache(id);

            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, ZhuanZhangTable model)
        {
            var item = _zhuanZhangService.Get(model.Id);
            item.LiveOn();
            //item.ZhuanZhangName = model.ZhuanZhangName;
            item.Image = model.Image;

            _zhuanZhangService.Update(item);
            _zhuanZhangService.ClearCache(id);

            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var item = _zhuanZhangService.Get(id);
            item.LiveOff();

            _zhuanZhangService.Update(item);
            _zhuanZhangService.ClearCache(item.UserId);

            return Ok();
        }

    }
}