using AALife.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AALife.WebMvc.Controllers
{
    public class BookApiController : BaseApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var result = new BookResult()
            {
                Code = "1",
                Message = "作者：张三<br>出版社：中国出版",
                MsgType = "info"
            };
            return Json(result);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string id)
        {
            using(var db = new AALifeDbContext())
            {
                var exists = db.UserTable.Any(a => a.DtUser == id && !(id == null || id.Trim() == string.Empty));
                var result = new BookResult()
                {
                    Code = exists ? "1" : "0",
                    Message = exists ? "有效账号，请继续操作。" : "无效账号，请联系管理员！",
                    MsgType = exists ? "success" : "danger"
                };
                return Json(result);
            }
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }

    public class BookResult
    {
        public string Message { get; set; }
        public string Code { get; set; }
        public string MsgType { get; set; }
    }
}