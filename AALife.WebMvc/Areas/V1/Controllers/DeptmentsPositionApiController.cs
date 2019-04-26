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
    /// 操作指定部门下的岗位
    /// </summary>
    public class DeptmentsPositionApiController : BaseApiController
    {
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public DeptmentsPositionApiController(IUserDeptmentService userDeptmentService,
            IUserPositionService userPositionService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userDeptmentService = userDeptmentService;
            this._userPositionService = userPositionService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            return Json(HttpStatusCode.OK);
        }
        
        // GET api/<controller>/5
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

        // POST api/<controller>/5
        public void Post(Guid id, [FromBody]IEnumerable<UserPosition> models)
        {
            var deptment = _userDeptmentService.Get(id);
            models.ToList().ForEach(a =>
            {
                a.Id = Guid.NewGuid();
                if (a.Parent != null)
                {
                    if (_userPositionService.IsExists(b => b.Id == a.Parent.Id))
                    {
                        a.ParentId = a.Parent.Id;
                    }
                    a.Parent = null;
                }
                a.Deptment = deptment;
                _userPositionService.Add(a);
            });

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Insert, "插入部门岗位记录。{0}", models.ToJson());
        }

        // PUT api/<controller>/5
        public void Put(Guid id, [FromBody]IEnumerable<UserPosition> models)
        {
            var deptment = _userDeptmentService.Get(id);
            models.ToList().ForEach(a =>
            {
                var position = _userPositionService.Get(a.Id);
                position.Name = a.Name;
                position.Code = a.Code;
                position.Notes = a.Notes;
                if (a.Parent != null)
                {
                    if (_userPositionService.IsExists(b => b.Id == a.Parent.Id))
                    {
                        position.DeptmentId = a.Parent.DeptmentId;
                        position.ParentId = a.Parent.Id;
                    }
                    position.Parent = null;
                }
                _userPositionService.Update(position);
            });

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Update, "更新部门岗位记录。{0}", models.ToJson());
        }

        // DELETE api/<controller>/5
        public void Delete(Guid id, [FromBody]IEnumerable<UserPosition> models)
        {
            var deptment = _userDeptmentService.Get(id);
            models.ToList().ForEach(a =>
            {
                var position = _userPositionService.Get(a.Id);
                if (deptment.Positions.Contains(position))
                {
                    deptment.Positions.Remove(position);
                    _userPositionService.Delete(position);
                }
            });

            _userDeptmentService.Update(deptment);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Delete, "删除部门岗位记录。{0}", models.ToJson());
        }

        #region 其它方法

        // 取组织下岗位的用户
        [Route("api/v1/deptmentspositionuserapi")]
        public IHttpActionResult GetDeptmentsPositionUser(Guid id, [FromUri]DataSourceRequest request)
        {
            var position = _userPositionService.Get(id);
            if (position != null)
            {
                var users = position.UsersPositions.Select(a => a.User);
                var grid = new DataSourceResult
                {
                    Data = users.Skip(request.Skip).Take(request.Take).Select(x =>
                    {
                        var m = x.MapTo<UserTable, UserRoleViewModel>();
                        m.Position = x.UsersPositions.First(a => a.IsMainPosition.GetValueOrDefault()).Position;
                        return m;
                    }),
                    Total = users.Count()
                };

                return Json(grid);
            }

            return Json(HttpStatusCode.OK);
        }

        #endregion
    }
}