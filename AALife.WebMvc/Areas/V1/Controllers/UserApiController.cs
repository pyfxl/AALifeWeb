using AALife.Core.Domain.Logging;
using AALife.Core.Services.Logging;
using AALife.Core.Services.Security;
using AALife.Data;
using AALife.Data.Domain;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.Query;
using AALife.WebMvc.Models.ViewModel;
using AutoMapper.QueryableExtensions;
using Kendo.DynamicLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class UserApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IEncryptionService _encryptionService;
        private readonly ICustomerActivityService _customerActivityService;

        public UserApiController(IUserService userService,
            IUserRoleService userRoleService,
            IEncryptionService encryptionService,
            ICustomerActivityService customerActivityService)
        {
            this._userService = userService;
            this._userRoleService = userRoleService;
            this._encryptionService = encryptionService;
            this._customerActivityService = customerActivityService;
        }

        // GET api/<controller>
        [Route("api/kendo/usersapi")]
        public IHttpActionResult GetUsers([FromUri]Data.Infrastructure.Kendoui.DataSourceRequest common, [FromUri]UsersQuery query)
        {
            var result = _userService.GetAllUserByPage(common.Page - 1, common.PageSize, query.userId, query.startDate, query.endDate, query.keyWords, common.Sort, common.Filter);

            var grid = new Data.Infrastructure.Kendoui.DataSourceResult
            {
                Data = result.Select(x =>
                {
                    var m = x.ToModel();
                    return m;
                }),
                Total = result.TotalCount
            };

            return Json(grid);
        }

        // GET api/<controller>
        [Route("api/v1/usernamesapi")]
        public IHttpActionResult GetUserNames()
        {
            var result = _userService.FindAll(a => a.Live == 1).Select(a => a.UserName).ToArray();
            
            return Json(result);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post(UserTable model)
        {
            var user = _userService.GetUserByUserName(model.UserName);
            if (user != null)
                return ErrorForKendoGridJson("用户名重复！");

            var passwordSalt = _encryptionService.CreateSaltKey(Constant.PasswordSaltSize);
            model.PasswordSalt = passwordSalt;
            model.UserPassword = _encryptionService.CreatePasswordHash(model.UserPassword, passwordSalt);

            //user role
            var registerRole = _userRoleService.Find(a => a.SystemName == UserRoleNames.Registered.ToString());
            var userRoles = new List<UserRole>();
            userRoles.Add(registerRole);

            //add role
            model.UserRoles = userRoles;

            //insert
            _userService.Add(model);

            //activity log
            _customerActivityService.InsertActivity(1, ActivityLogType.Insert, "插入用户记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(UserTable model)
        {
            var user = _userService.Get(model.Id);
            user.UserEmail = model.UserEmail;
            user.UserTheme = model.UserTheme;
            user.UserLevel = model.UserLevel;
            user.UserFrom = model.UserFrom;
            user.ModifyDate = DateTime.Now;
            user.Synchronize = 1;
            user.Live = 1;
            user.Remark = model.Remark;

            //update
            _userService.Update(user);

            //activity log
            _customerActivityService.InsertActivity(1, ActivityLogType.Update, "更新用户记录。{0}", user.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(UserTable model)
        {
            //delete
            _userService.Delete(model.Id);

            //activity log
            _customerActivityService.InsertActivity(1, ActivityLogType.Delete, "删除用户记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }
    }
}