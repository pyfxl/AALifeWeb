using AALife.Core.Domain.Logging;
using AALife.Core.Infrastructure.Mapper;
using AALife.Core.Services.Logging;
using AALife.Core.Services.Security;
using AALife.Data;
using AALife.Data.Domain;
using AALife.Core.Infrastructure.Kendoui;
using AALife.Data.Services;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.Query;
using AALife.WebMvc.Models.ViewModel;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class UsersApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IEncryptionService _encryptionService;
        private readonly ICustomerActivityService _customerActivityService;

        public UsersApiController(IUserService userService,
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
        public IHttpActionResult Get([FromUri]DataSourceRequest common, [FromUri]UsersQuery query)
        {
            //不使用Linq插件，因为速度慢
            var result = _userService.GetAllUserByPage(common.Page - 1, common.PageSize, common.Sort, common.Filter, query.userId, query.startDate, query.endDate, query.keyWords);

            var viewModel = result.Select(x =>
            {
                var m = x.MapTo<UserTable, UserManageViewModel>();
                return m;
            });

            var grid = new DataSourceResult
            {
                Data = viewModel,
                Total = result.TotalCount
            };

            return Json(grid);
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
            var registerRole = _userRoleService.GetUserRoleBySystemName(UserRoleNames.Registered.ToString());
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

            AutoMapperConfiguration.Mapper.Map(model, user);

            user.ModifyDate = DateTime.Now;

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

        #region 其它方法

        // 获取用户列表，用于弹出窗口选择
        [Route("api/v1/userselectsapi")]
        public IHttpActionResult GetUserRoles([FromUri]DataSourceRequest common, [FromUri]UsersQuery query)
        {
            var result = _userService.GetAllUserByPage(common.Page - 1, common.PageSize, common.Sort, common.Filter, query.userId, query.startDate, query.endDate, query.keyWords);

            var grid = new DataSourceResult
            {
                Data = result.Select(x =>
                {
                    var m = x.MapTo<UserTable, UserRoleViewModel>();
                    return m;
                }),
                Total = result.TotalCount
            };

            return Json(grid);
        }

        // GET api/<controller>
        [Route("api/v1/usernamesapi")]
        public IHttpActionResult GetUserNames(string term)
        {
            if (string.IsNullOrWhiteSpace(term)) return Json("");

            var result = _userService.FindAll(a => a.UserName.Contains(term))
                .Select(a => new { value = a.Id, text = a.UserName, Index = a.UserName.IndexOf(term) })
                .OrderBy(a => a.Index)
                .Skip(0).Take(10)
                .ToList();

            return Json(result);
        }

        #endregion
    }
}