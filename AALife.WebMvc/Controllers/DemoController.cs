using AALife.Service.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Linq;
using System.Web.Mvc;

namespace AALife.WebMvc.Controllers
{
    public class DemoController : Controller
    {
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            using (var db = new AALifeDbContext())
            {
                IQueryable<UserTable> users = db.UserTable;
                DataSourceResult result = users.ToDataSourceResult(request);
                return Json(result);
            }
        }
    }
}