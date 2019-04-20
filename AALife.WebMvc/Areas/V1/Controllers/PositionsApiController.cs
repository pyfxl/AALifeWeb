using AALife.Core.Domain.Logging;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Configuration;
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
    /// <summary>
    /// 岗位管理接口
    /// </summary>
    public class PositionsApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public PositionsApiController(IUserService userService,
            IUserDeptmentService userDeptmentService, 
            IUserPositionService userPositionService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userDeptmentService = userDeptmentService;
            this._userPositionService = userPositionService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET: api/<Controller>/5
        public IHttpActionResult Get(Guid? id, [FromUri]DataSourceRequest request)
        {
            var positions = _userPositionService.Get();

            //deptment
            if (id != null)
            {
                var deptment = _userDeptmentService.Get(id.Value);

                if (deptment != null)
                {
                    positions = deptment.Positions.AsQueryable();
                }
                else
                {
                    positions = _userPositionService.FindAll(a => a.Id == id.Value);
                }
            }

            var grid = new DataSourceResult
            {
                Data = positions.ToKendoDataSource(request).AsEnumerable().Select(x =>
                {
                    var m = x.MapTo<UserPosition, UserPositionModel>();
                    m.Parent = x.Parent;
                    m.ParentId = x.ParentId;
                    return m;
                }),
                Total = positions.Count()
            };

            return Json(grid);
        }

        // POST api/<controller>
        public IHttpActionResult Post(Guid id, IEnumerable<UserPosition> models)
        {
            if (!ModelState.IsValid)
                return ErrorForKendoGridJson(ModelState);

            //deptment
            var deptment = _userDeptmentService.Get(id);

            //position
            var position = _userPositionService.Get(id);

            models.ToList().ForEach(x =>
            {
                x.Id = Guid.NewGuid();
                if (deptment != null)
                {
                    x.DeptmentId = deptment.Id;
                }
                else
                {
                    x.DeptmentId = position.DeptmentId;
                }
                if (x.Parent != null)
                {
                    x.ParentId = x.Parent.Id;
                    x.Parent = null;
                }
            });

            //insert
            _userPositionService.Add(models);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入岗位记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(Guid id, IEnumerable<UserPosition> models)
        {
            if (!ModelState.IsValid)
                return ErrorForKendoGridJson(ModelState);

            var positions = new List<UserPosition>();
            models.ToList().ForEach(x =>
            {
                var position = _userPositionService.Get(x.Id);
                position.Name = x.Name;
                position.Code = x.Code;
                position.Notes = x.Notes;
                if (x.Parent != null)
                {
                    position.ParentId = x.Parent.Id;
                    position.DeptmentId = x.Parent.DeptmentId;
                }
                positions.Add(position);
            });

            //update
            _userPositionService.Update(positions);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Update, "更新岗位记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(Guid id, IEnumerable<UserPosition> models)
        {
            var positions = new List<UserPosition>();
            models.ToList().ForEach(x =>
            {
                var position = _userPositionService.Get(x.Id);
                positions.Add(position);
            });

            //delete
            _userPositionService.Delete(positions);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Delete, "删除岗位记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }
    }
}
