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
using AALife.Core.Services.Configuration;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class UsersApiController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IEncryptionService _encryptionService;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IParameterService _parameterService;

        public UsersApiController(IUserService userService,
            IUserRoleService userRoleService,
            IEncryptionService encryptionService,
            ICustomerActivityService customerActivityService,
            IParameterService parameterService)
        {
            this._userService = userService;
            this._userRoleService = userRoleService;
            this._encryptionService = encryptionService;
            this._customerActivityService = customerActivityService;
            this._parameterService = parameterService;
        }

        // GET api/<controller>
        public IHttpActionResult Get([FromUri]DataSourceRequest common, [FromUri]UsersQuery query)
        {
            //不使用Linq插件，因为速度慢
            var result = _userService.GetAllUserByPage(common.Page - 1, common.PageSize, common.Sort, common.Filter, query.userId, query.startDate, query.endDate, query.keyWords);

            var viewModel = result.Select(x =>
            {
                var m = x.MapTo<UserTable, UserManageViewModel>();
                m.Position = x.UserPositions.FirstOrDefault();
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

            //add role
            model.UserRoles.Add(registerRole);

            //insert
            _userService.Add(model);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Insert, "插入用户记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(UserTable model)
        {
            var user = _userService.Get(model.Id);

            user.UserName = model.UserName;
            user.UserCode = model.UserCode;
            user.FirstName = model.FirstName;
            user.UserImage = model.UserImage;
            user.UserPassword = model.UserPassword;
            user.PasswordSalt = model.PasswordSalt;
            user.UserTheme = model.UserTheme;
            user.UserLevel = model.UserLevel;
            user.UserFrom = model.UserFrom;
            user.CreateDate = model.CreateDate;
            user.Synchronize = model.Synchronize;
            user.Remark = model.Remark;
            user.ModifyDate = DateTime.Now;
            user.IsAdmin = model.IsAdmin;

            //update
            _userService.Update(user);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Update, "更新用户记录。{0}", user.ToJson());

            return Json(HttpStatusCode.OK);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(UserTable model)
        {
            var user = _userService.Get(model.Id);

            //干掉用户角色
            user.UserRoles.Clear();

            //delete
            _userService.Delete(user);

            //activity log
            _customerActivityService.InsertActivity(null, ActivityLogType.Delete, "删除用户记录。{0}", model.ToJson());

            return Json(HttpStatusCode.OK);
        }

        #region 其它方法

        // 用与用户名框下拉
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