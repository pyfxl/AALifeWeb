﻿using AALife.Data.Services;
using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using AALife.Service.EF;
using AALife.WebMvc.Infrastructure.Mapper;
using AALife.WebMvc.Models.Query;
using System;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class UserApiController : BaseApiController
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
                    return m;
                }),
                Total = result.TotalCount
            };

            return Json(grid);
        }

        // GET api/<controller>
        [Route("api/v1/userfromapi")]
        public IHttpActionResult GetUserFrom()
        {
            var result = Constant.UserFromDic.ToList();
            var grid = new Kendoui.DataSourceResult
            {
                Data = result,
                Total = result.Count()
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