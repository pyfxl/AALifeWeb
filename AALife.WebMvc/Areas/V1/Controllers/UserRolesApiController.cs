using AALife.Core.Domain.Logging;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
using AutoMapper.QueryableExtensions;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    /// <summary>
    /// 操作指定角色下的用户
    /// </summary>
    public class UserRolesApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly ICustomerActivityService _customerActivityService;

        public UserRolesApiController(IUserService userService,
            IUserRoleService userRoleService,
            ICustomerActivityService customerActivityService)
        {
            this._userService = userService;
            this._userRoleService = userRoleService;
            this._customerActivityService = customerActivityService;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id, [FromUri]DataSourceRequest request)
        {
            var result = _userRoleService.Get(id).UserTables
                .OrderByDescending(a => a.Id)
                .Select(x => 
                {
                    var m = x.MapTo<UserTable, UserRoleViewModel>();
                    return m;
                })
                .AsQueryable();

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Query, "浏览用户角色记录。{0}", request.ToJson());

            return Json(result.ToDataSourceResult(request));
        }

        // POST api/<controller>/5
        public void Post(int id, [FromBody]IEnumerable<UserTable> models)
        {
            var role = _userRoleService.Get(id);
            models.ToList().ForEach(a =>
            {
                var user = _userService.Get(a.Id);
                if(user != null)
                    role.UserTables.Add(user);
            });

            _userRoleService.Update(role);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Insert, "插入用户角色。{0}", models.ToJson());
        }

        // PUT api/<controller>/5
        public void Put(int id, UserTable model)
        {
            var role = _userRoleService.Get(id);
            var user = _userService.Get(model.Id);

            if (role.UserTables.Contains(user))
            {
                role.UserTables.Remove(user);
            }
            else
            {
                role.UserTables.Add(user);
            }

            _userRoleService.Update(role);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Insert, "插入更新用户角色。{0}", user.ToJson());
        }

        // DELETE api/<controller>/5
        public void Delete(int id, [FromBody]IEnumerable<UserTable> models)
        {
            var role = _userRoleService.Get(id);
            models.ToList().ForEach(a =>
            {
                var user = _userService.Get(a.Id);
                if(role.UserTables.Contains(user))
                    role.UserTables.Remove(user);
            });

            _userRoleService.Update(role);

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Delete, "删除用户角色。{0}", models.ToJson());
        }

        #region 其它方法

        // 获取用户角色列表，默认选中当前已有的角色
        [Route("api/v1/userroleselectsapi/{id}")]
        public IHttpActionResult GetUserRoles(int id, [FromUri]Data.Infrastructure.Kendoui.DataSourceRequest common)
        {
            var result = _userService.GetAllUserByPage(common.Page - 1, common.PageSize, common.Sort, common.Filter);

            var grid = new Data.Infrastructure.Kendoui.DataSourceResult
            {
                Data = result.Select(x =>
                {
                    var m = x.MapTo<UserTable, UserRoleViewModel>();
                    m.IsCurrentRole = x.UserRoles.Any(w => w.Id == id);
                    return m;
                }),
                Total = result.TotalCount
            };

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Query, "根据角色主键浏览用户角色记录。{0}", common.ToJson());

            return Json(grid);
        }

        #endregion

    }
}