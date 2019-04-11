using AALife.Core.Domain.Logging;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class PositionsApiController : BaseApiController
    {
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;

        public PositionsApiController(IUserPositionService userPositionService,
            ICustomerActivityService customerActivityService)
        {
            this._userPositionService = userPositionService;
            this._customerActivityService = customerActivityService;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var result = _userPositionService.Get();

            var grid = new DataSourceResult
            {
                Data = result,
                Total = result.Count()
            };

            return Json(grid);
        }

        // POST api/<controller>
        public IHttpActionResult Post(IEnumerable<UserPosition> models)
        {
            if (ModelState.IsValid)
            {
                _userPositionService.Add(models);
            }

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入岗位记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(IEnumerable<UserPosition> models)
        {
            if (ModelState.IsValid)
            {
                var items = new List<UserPosition>();
                models.ToList().ForEach(a =>
                {
                    var item = _userPositionService.Get(a.Id);
                    item.Name = a.Name;
                    item.Notes = a.Notes;
                    items.Add(item);
                });

                _userPositionService.Update(items);
            }

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Update, "更新岗位记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(IEnumerable<UserPosition> models)
        {
            var items = new List<UserPosition>();
            models.ToList().ForEach(a =>
            {
                var item = _userPositionService.Get(a.Id);
                items.Add(item);
            });

            _userPositionService.Delete(items);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Delete, "删除岗位记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }
    }
}