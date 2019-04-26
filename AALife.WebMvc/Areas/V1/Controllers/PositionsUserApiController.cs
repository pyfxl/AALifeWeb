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
    /// 岗位用户接口
    /// </summary>
    public class PositionsUserApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly IUserPositionService _userPositionService;
        private readonly IUsersPositionsService _usersPositionsService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public PositionsUserApiController(IUserService userService,
            IUserDeptmentService userDeptmentService,
            IUserPositionService userPositionService,
            IUsersPositionsService usersPositionsService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userDeptmentService = userDeptmentService;
            this._userPositionService = userPositionService;
            this._usersPositionsService = usersPositionsService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(Guid id, [FromUri]DataSourceRequest request)
        {
            var users = _userService.GetByPage(request, x => x.UsersPositions.Select(a => a.Position).Any(a => a.Id == id));

            var grid = new DataSourceResult
            {
                Data = users.Select(x =>
                {
                    var m = x.MapTo<UserTable, UserRoleViewModel>();
                    m.Position = x.UsersPositions.First(a => a.IsMainPosition.GetValueOrDefault()).Position;
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
            var deptment = _userDeptmentService.Get(position.DeptmentId);

            models.ToList().ForEach(x =>
            {                
                var user = _userService.Get(x.Id);
                if (user != null)
                {
                    var userPositions = new UsersPositions()
                    {
                        Id = Guid.NewGuid(),
                        Position = position,
                        User = user
                    };
                    _usersPositionsService.Add(userPositions);

                    deptment.Users.Add(user);
                }
            });

            //insert
            _userDeptmentService.Update(deptment);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Insert, "插入用户岗位。{0}", models.ToJson());
        }

        // DELETE api/<controller>/5
        public void Delete(Guid id, [FromBody]IEnumerable<UserTable> models)
        {
            var position = _userPositionService.Get(id);
            var deptment = _userDeptmentService.Get(position.DeptmentId);

            models.ToList().ForEach(x =>
            {
                var userPositions = _usersPositionsService.Find(a => a.Position.Id == id && a.User.Id == x.Id);
                if (userPositions != null)
                    _usersPositionsService.Delete(userPositions);

                var user = _userService.Get(x.Id);
                if (deptment.Users.Contains(user))
                    deptment.Users.Remove(user);
            });

            //delete
            _userDeptmentService.Update(deptment);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Delete, "删除用户岗位。{0}", models.ToJson());
        }

    }
}