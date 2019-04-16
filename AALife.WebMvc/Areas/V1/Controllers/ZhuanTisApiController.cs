using AALife.Data;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.jqGrid;
using System;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class ZhuanTisApiController : BaseApiController
    {
        private readonly IZhuanTiService _zhuanTiService;

        public ZhuanTisApiController(IZhuanTiService zhuanTiService)
        {
            this._zhuanTiService = zhuanTiService;
        }

        // GET api/<controller>
        public IHttpActionResult Get(Guid id)
        {
            var result = _zhuanTiService.GetAll(id);
            var grid = new DataSourceResult
            {
                rows = result,
                records = result.Count()
            };
            return Json(grid);
        }

        // POST api/<controller>
        public IHttpActionResult Post(Guid id, ZhuanTiTable model)
        {
            model.LiveOn();
            model.UserId = id;

            _zhuanTiService.Add(model);
            _zhuanTiService.ClearCache(id);

            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(Guid id, ZhuanTiTable model)
        {
            var item = _zhuanTiService.Get(model.Id);
            item.LiveOn();
            item.ZhuanTiName = model.ZhuanTiName;

            _zhuanTiService.Update(item);
            _zhuanTiService.ClearCache(id);

            return Ok();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var item = _zhuanTiService.Get(id);
            item.LiveOff();

            _zhuanTiService.Update(item);
            _zhuanTiService.ClearCache(item.UserId);

            return Ok();
        }

        #region 其他方法 

        // GET api/<controller>
        [Route("api/v1/zhuantinamesapi")]
        public IHttpActionResult GetZhuanTiNames(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return Json("");

            var all = _zhuanTiService.FindAll(a => a.ZhuanTiName.Contains(term))
                .GroupBy(a => new { a.Id, a.ZhuanTiName })
                .Select(a => new { a.Key.Id, a.Key.ZhuanTiName, Index = a.Key.ZhuanTiName.IndexOf(term) })
                .OrderBy(a => a.Index)
                //.Skip(0).Take(50)
                .ToList();

            var result = all.GroupBy(a => a.ZhuanTiName)
                .Select(a => new { value = string.Join(", ", all.Where(b => b.ZhuanTiName == a.Key).Select(b => b.Id).ToArray()), text = a.Key })
                .ToList();

            return Json(result);
        }

        #endregion

    }
}