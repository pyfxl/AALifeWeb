using AALife.Service.EF;
using AALife.Service.Domain.Common;
using AALife.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class UserWorkDayApiController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            WorkDayBLL bll = new WorkDayBLL();

            var lists = bll.GetWorkDay();

            var result = new ListModel<WorkDayTable>
            {
                rows = lists.ToList(),
                total = lists.Count()
            };

            return Json(result);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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
}