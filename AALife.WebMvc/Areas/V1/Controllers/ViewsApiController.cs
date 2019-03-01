using AALife.Service.EF;
using Kendo.DynamicLinq;
using System.Web.Http;

namespace AALife.WebMvc.Areas.V1.Controllers
{
    public class ViewsApiController : BaseApiController
    {
        // GET api/<controller>
        public DataSourceResult Get(string query)
        {
            ViewTableBLL bll = new ViewTableBLL();
            DataSourceRequest model = Newtonsoft.Json.JsonConvert.DeserializeObject<DataSourceRequest>(query);

            return bll.GetViewTable(model.Take, model.Skip, model.Sort, model.Filter, null);
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