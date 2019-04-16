using AALife.Code.Services;
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
    /// 操作指定部门下的用户
    /// </summary>
    public class DeptmentsUserApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserDeptmentService _userDeptmentService;
        private readonly IUserPositionService _userPositionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public DeptmentsUserApiController(IUserService userService,
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
        public IHttpActionResult Get(Guid? id, [FromUri]DataSourceRequest request)
        {
            var users = _userService.GetByPage(request, a => a.UserDeptments.Any(b => b.Id == id));

            var grid = new DataSourceResult
            {
                Data = users.Select(x =>
                {
                    var m = x.MapTo<UserTable, UserRoleViewModel>();
                    m.Position = x.UserPositions.FirstOrDefault(a => a.DeptmentId == id);
                    return m;
                }),
                Total = users.TotalCount
            };

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Query, "浏览用户部门记录。{0}", request.ToJson());

            return Json(grid);
        }

        // POST api/<controller>/5
        public void Post(Guid id, [FromBody]IEnumerable<UserTable> models)
        {
            var deptment = _userDeptmentService.Get(id);
            models.ToList().ForEach(a =>
            {
                var user = _userService.Get(a.Id);
                if (user != null)
                    deptment.Users.Add(user);
            });

            _userDeptmentService.Update(deptment);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Insert, "插入用户部门。{0}", models.ToJson());
        }

        // PUT api/<controller>/5
        public void Put(Guid id, UserTable model)
        {
            var deptment = _userDeptmentService.Get(id);
            var user = _userService.Get(model.Id);

            if (deptment.Users.Contains(user))
            {
                deptment.Users.Remove(user);
            }
            else
            {
                deptment.Users.Add(user);
            }

            _userDeptmentService.Update(deptment);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Insert, "插入更新用户部门。{0}", user.ToJson());
        }

        // DELETE api/<controller>/5
        public void Delete(Guid id, [FromBody]IEnumerable<UserTable> models)
        {
            var deptment = _userDeptmentService.Get(id);
            models.ToList().ForEach(a =>
            {
                var user = _userService.Get(a.Id);
                if (deptment.Users.Contains(user))
                    deptment.Users.Remove(user);
            });

            _userDeptmentService.Update(deptment);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Delete, "删除用户部门。{0}", models.ToJson());
        }

        #region 其它方法
        
        // 根据部门获取用户列表，支持获取子级部门，用于弹出窗口选择
        [Route("api/v1/deptmentsuserselectapi")]
        public IHttpActionResult GetDeptmentsUserSelect(Guid id, [FromUri]DataSourceRequest request)
        {
            var users = new List<UserTable>();
            var deptment = _userDeptmentService.Get(id);

            //查找部门下岗位的用户
            Action<UserDeptment> action = null;
            action = (item) =>
            {
                //TODO: 性能慢，待优化
                users.AddRange(item.Users);
                foreach (var it in item.Children)
                {
                    action(it);
                }
            };

            //调用
            action(deptment);

            //去重复
            users = users.Where((x, i) => users.FindIndex(z => z.Id == x.Id) == i).ToList();

            var grid = new DataSourceResult
            {
                Data = users.ToDataSourceResult(request).Select(x =>
                {
                    var m = x.MapTo<UserTable, UserRoleViewModel>();
                    m.UserFromName = _parameterService.GetParamsByName("userfrom").First(a => a.Value == m.UserFrom).Name;
                    return m;
                }),
                Total = users.Count()
            };

            return Json(grid);
        }

        #endregion
    }
}