﻿using AALife.Core.Domain.Logging;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.ViewModel;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    /// <summary>
    /// 根据用户获取角色接口
    /// </summary>
    public class UsersRoleApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public UsersRoleApiController(IUserService userService,
            IUserRoleService userRoleService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userRoleService = userRoleService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(Guid id, [FromUri]DataSourceRequest request)
        {
            var users = _userService.GetByPage(request, x => x.UserRoles.Any(a => a.Id == id));

            var grid = new DataSourceResult
            {
                Data = users.Select(x =>
                {
                    var m = x.MapTo<UserTable, UserRoleViewModel>();
                    return m;
                }),
                Total = users.TotalCount
            };

            //activity log
            _customerActivityService.InsertActivity(id, ActivityLogType.Query, "浏览用户角色记录。{0}", request.ToJson());

            return Json(grid);
        }

        // POST api/<controller>/5
        public void Post(Guid id, [FromBody]IEnumerable<UserTable> models)
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
        public void Put(Guid id, UserTable model)
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
        public void Delete(Guid id, [FromBody]IEnumerable<UserTable> models)
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
        
    }
}