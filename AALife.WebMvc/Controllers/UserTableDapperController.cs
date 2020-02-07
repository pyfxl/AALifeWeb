﻿using AALife.Service.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Yanzi.Core.KendoDapper;

namespace AALife.WebMvc.Controllers
{
    public class UserTableDapperController : BaseApiController
    {
        // GET api/<controller>
        public DataSourceResult Get([FromUri]DataSourceRequest request)
        {
            var grid = new DataSourceResult();

            try
            {
                UserTableBLL bll = new UserTableBLL();
                grid = bll.GetDapperDataSource(request);
            }
            catch (Exception ex)
            {
                grid.Errors = "加载出错！";
                log.Info(ex);
            }

            return grid;
        }

    }
}