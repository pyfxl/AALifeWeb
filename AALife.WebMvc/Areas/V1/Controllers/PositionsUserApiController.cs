using AALife.Core.Domain.Logging;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
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
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public PositionsUserApiController(IUserService userService,
            IUserPositionService userPositionService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userPositionService = userPositionService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id, [FromUri]DataSourceRequest request)
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
        public void Post(int id, [FromBody]IEnumerable<UserTable> models)
        {
            var deptment = _userPositionService.Get(id);
            models.ToList().ForEach(a =>
            {
                var user = _userService.Get(a.Id);
                if(user != null)
                    deptment.Users.Add(user);
            });

            _userPositionService.Update(deptment);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Insert, "插入用户岗位。{0}", models.ToJson());
        }

        // PUT api/<controller>/5
        public void Put(int id, UserTable model)
        {
            var role = _userPositionService.Get(id);
            var user = _userService.Get(model.Id);

            if (role.Users.Contains(user))
            {
                role.Users.Remove(user);
            }
            else
            {
                role.Users.Add(user);
            }

            _userPositionService.Update(role);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Insert, "插入更新用户岗位。{0}", user.ToJson());
        }

        // DELETE api/<controller>/5
        public void Delete(int id, [FromBody]IEnumerable<UserTable> models)
        {
            var role = _userPositionService.Get(id);
            models.ToList().ForEach(a =>
            {
                var user = _userService.Get(a.Id);
                if(role.Users.Contains(user))
                    role.Users.Remove(user);
            });

            _userPositionService.Update(role);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Delete, "删除用户岗位。{0}", models.ToJson());
        }

    }
}