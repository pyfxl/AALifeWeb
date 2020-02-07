using AALife.Service.EF;
using DevExtreme.AspNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AALife.WebMvc.Controllers
{
    public class ViewPivotGridController : BaseApiController
    {
        // GET api/<controller>
        public async Task<IHttpActionResult> Get([FromUri]DataSourceLoadOptions loadOptions)
        {
            ViewTableBLL bll = new ViewTableBLL();
            var results = bll.GetAll();

            loadOptions.PrimaryKey = new[] { "ViewID" };
            //loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(results, loadOptions));
        }
    }
}