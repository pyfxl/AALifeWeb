using AALife.Service.EF;
using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AALife.WebMvc.Models.Query;
using AALife.Core.Services;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class UserApiController : ApiController
    {
        private readonly IUserService _userService;

        public UserApiController(IUserService userService)
        {
            this._userService = userService;
        }

        // GET api/<controller>
        [Route("api/v1/usersapi")]
        public IHttpActionResult GetUsers([FromUri]Kendoui.DataSourceRequest common, [FromUri]UsersQuery query)
        {
            var result = _userService.GetAllUserByPage(common.Page - 1, common.PageSize, query.userId, query.startDate, query.endDate, query.keyWords);

            var grid = new Kendoui.DataSourceResult
            {
                Data = result.Select(x =>
                {
                    var m = x.ToModel();
                    m.UserFromName = Constant.UserFromDic[x.UserFrom];
                    m.UserLevelName = Constant.UserLevelDic[x.UserLevel];
                    return m;
                }),
                Total = result.TotalCount
            };

            return Json(grid);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post(UserTableViewModel models)
        {
            string error = "";
            try
            {
                UserTableBLL bll = new UserTableBLL();
                bool exists = bll.CheckUserExists(models, "UserName");
                if (exists)
                {
                    error = "用户名重复！";
                }
                else
                {
                    bll.AddUserTable(models);
                }
            }
            catch
            {
                error = "添加错误！";
            }

            return Json(new ResultModel { error = error });
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(UserTableViewModel models)
        {
            string error = "";
            try
            {
                models.ModifyDate = DateTime.Now;

                UserTableBLL bll = new UserTableBLL();
                bll.UpdateUserTable(models);
            }
            catch
            {
                error = "更新出错！";
            }

            return Json(new ResultModel { error = error });
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(UserTableViewModel models)
        {
            string error = "";
            try
            {
                UserTableBLL bll = new UserTableBLL();
                bll.RemoveUserTable(models);
            }
            catch
            {
                error = "删除错误！";
            }

            return Json(new ResultModel { error = error });
        }
    }
}