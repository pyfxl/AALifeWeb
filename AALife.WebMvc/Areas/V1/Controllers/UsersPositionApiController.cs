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
    /// 根据用户找岗位接口
    /// </summary>
    public class UsersPositionApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public UsersPositionApiController(IUserService userService,
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
        public IHttpActionResult Get(Guid id)
        {
            var user = _userService.Get(id);

            var grid = new DataSourceResult
            {
                Data = user.UserPositions.Select(x =>
                {
                    var m = x.MapTo<UserPosition, UserPositionModel>();
                    m.Parent = x.Parent;
                    return m;
                }),
                Total = user.UserPositions.Count()
            };

            return Json(grid);
        }

        // POST api/<controller>/5
        public IHttpActionResult Post(Guid id, [FromBody]IEnumerable<UserPosition> models)
        {
            //user
            var user = _userService.Get(id);

            models.ToList().ForEach(x =>
            {
                var position = _userPositionService.Get(x.Id);

                if (!user.UserPositions.Contains(position))
                {
                    user.UserPositions.Add(position);

                    var deptment = _userDeptmentService.Get(position.DeptmentId);

                    user.UserDeptments.Add(deptment);
                }
            });

            //insert
            _userService.Update(user);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入岗位记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(Guid id, [FromBody]IEnumerable<UserPosition> models)
        {
            //user
            var user = _userService.Get(id);

            models.ToList().ForEach(x =>
            {
                var position = _userPositionService.Get(x.Id);

                if (user.UserPositions.Contains(position))
                {
                    user.UserPositions.Remove(position);

                    var deptment = _userDeptmentService.Get(position.DeptmentId);

                    user.UserDeptments.Remove(deptment);
                }
            });

            //insert
            _userService.Update(user);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入岗位记录。{0}", models.ToJson());

            return Json(HttpStatusCode.OK);
        }

    }
}