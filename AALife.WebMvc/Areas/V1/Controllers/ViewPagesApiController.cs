using AALife.Service.Domain.Common;
using AALife.Service.EF;
using AALife.Service.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class ViewPagesApiController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var result = new ListModel<ViewPageTable>();

            try
            {
                ViewTableBLL bll = new ViewTableBLL();

                int count = 0;
                var lists = bll.GetViewPageTable(out count);

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