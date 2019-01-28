using AALife.BLL;
using AALife.Data;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.jqGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class ZhuanTiApiController : BaseApiController
    {
        private readonly IZhuanTiService _zhuanTiService;

        public ZhuanTiApiController(IZhuanTiService zhuanTiService)
        {
            this._zhuanTiService = zhuanTiService;
        }

        // GET api/<controller>
        public IHttpActionResult Get(int id)
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
        public IHttpActionResult Post(int id, ZhuanTiTable model)
        {
            model.LiveOn();
            model.UserId = id;

            _zhuanTiService.Add(model);
            _zhuanTiService.ClearCache(id);

            return Ok();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, ZhuanTiTable model)
        {
            var item = _zhuanTiService.Get(model.Id);
            item.LiveOn();
            item.ZhuanTiName = model.ZhuanTiName;
            item.Image = model.Image;

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

    }
}