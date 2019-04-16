using AALife.Core.Domain.Logging;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class PositionsApiController : BaseApiController
    {
        private readonly IUserPositionService _userPositionService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly ICustomerActivityService _customerActivityService;

        public PositionsApiController(IUserPositionService userPositionService,
            IUserDeptmentService userDeptmentService,
            ICustomerActivityService customerActivityService)
        {
            this._userPositionService = userPositionService;
            this._userDeptmentService = userDeptmentService;
            this._customerActivityService = customerActivityService;
        }

        // GET api/<controller>
        public IHttpActionResult Get(Guid id)
        {
            var positions = _userPositionService.FindAll(x => x.DeptmentId == id);

            var grid = new DataSourceResult
            {
                Data = positions,
                Total = positions.Count()
            };

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Query, "浏览部门岗位记录。{0}", id);

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
            var positions = new List<UserPosition>();
            models.ToList().ForEach(a =>
            {
                var position = _userPositionService.Get(a.Id);
                positions.Add(position);
            });

            _userPositionService.Delete(positions);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Delete, "删除岗位记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }
    }
}