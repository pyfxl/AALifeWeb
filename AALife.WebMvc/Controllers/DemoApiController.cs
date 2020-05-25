using AALife.Service.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AALife.WebMvc.Controllers
{
    public class DemoApiController : ApiController
    {
        // POST api/<controller>
        public DataSourceResult Post([DataSourceRequest]DataSourceRequest request)
        {
            using (var db = new AALifeDbContext())
            {
                IQueryable<UserTable> users = db.UserTable;
                DataSourceResult result = users.ToDataSourceResult(request);
                return result;
            }
        }
    }
}