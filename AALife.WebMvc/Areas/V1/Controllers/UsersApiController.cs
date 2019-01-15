using AALife.Service.EF;
using AALife.Service.Domain.Common;
using AALife.Service.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class UsersApiController : ApiController
    {
        private readonly UserTableBLL bll = new UserTableBLL();

        // GET api/<controller>
        public IHttpActionResult Get([FromUri]QueryPageModel query)
        {
            var result = new ListModel<UserTableViewModel>();

            try
            {
                int count = 0;
                var lists = bll.GetUserTable(query, out count);

                result.rows = lists.ToList();
                result.total = count;
            }
            catch (Exception ex)
            {
                result.error = "加载出错！";
            }

            return Json(result);
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