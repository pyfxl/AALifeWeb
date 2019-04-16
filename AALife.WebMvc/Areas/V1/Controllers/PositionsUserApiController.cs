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
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    /// <summary>
    /// 操作指定岗位下的用户
    /// </summary>
    public class PositionsUserApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public PositionsUserApiController(IUserService userService,
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

        // GET api/<controller>/5
        public IHttpActionResult Get(Guid id, [FromUri]DataSourceRequest request)
        {
            var users = _userService.GetByPage(request, x => x.UserPositions.Any(a => a.Id == id));

            var grid = new DataSourceResult
            {
                Data = users.Select(x =>
                {
                    var m = x.MapTo<UserTable, UserRoleViewModel>();
                    m.UserFromName = _parameterService.GetParamsByName("userfrom").First(a => a.Value == m.UserFrom).Name;
                    return m;
                }),
                Total = users.TotalCount
            };

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Query, "浏览用户岗位记录。{0}", request.ToJson());

            return Json(grid);
        }

        // POST api/<controller>/5
        public void Post(Guid id, [FromBody]IEnumerable<UserTable> models)
        {
            var position = _userPositionService.Get(id);
            var deptment = _userDeptmentService.Get(position.DeptmentId.Value);
            models.ToList().ForEach(a =>
            {
                var user = _userService.Get(a.Id);
                if (user != null)
                {
                    position.Users.Add(user);
                    deptment.Users.Add(user);
                }
            });

            _userPositionService.Update(position);

            _userDeptmentService.Update(deptment);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Insert, "插入用户岗位。{0}", models.ToJson());
        }

        // PUT api/<controller>/5
        public void Put(Guid id, UserTable model)
        {
            var position = _userPositionService.Get(id);
            var user = _userService.Get(model.Id);

            if (position.Users.Contains(user))
            {
                position.Users.Remove(user);
            }
            else
            {
                position.Users.Add(user);
            }

            _userPositionService.Update(position);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Insert, "插入更新用户岗位。{0}", user.ToJson());
        }

        // DELETE api/<controller>/5
        public void Delete(Guid id, [FromBody]IEnumerable<UserTable> models)
        {
            var position = _userPositionService.Get(id);
            var deptment = _userDeptmentService.Get(position.DeptmentId.Value);
            models.ToList().ForEach(a =>
            {
                var user = _userService.Get(a.Id);
                if (position.Users.Contains(user))
                    position.Users.Remove(user);
                if (deptment.Users.Contains(user))
                    deptment.Users.Remove(user);
            });

            _userPositionService.Update(position);

            _userDeptmentService.Update(deptment);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Delete, "删除用户岗位。{0}", models.ToJson());
        }

    }
}