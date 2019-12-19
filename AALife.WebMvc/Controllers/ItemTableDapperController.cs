using AALife.Service.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yanzi.Core.KendoDapper;

namespace AALife.WebMvc.Controllers
{
    public class ItemTableDapperController : BaseApiController
    {
        // GET api/<controller>
        public DataSourceResult Get([FromUri]DataSourceRequest request)
        {
            var grid = new DataSourceResult();

            try
            {
                ItemTableBLL bll = new ItemTableBLL();
                grid = bll.GetDapperDataSource(request);
            }
            catch (Exception ex)
            {
                grid.Errors = "加载出错！";
                log.Error(ex);
            }

            return grid;
        }

    }
}