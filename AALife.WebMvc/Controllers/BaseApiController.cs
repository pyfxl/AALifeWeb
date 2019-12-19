using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AALife.WebMvc.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected Logger log
        {
            get
            {
                return LogManager.GetLogger(GetType().Name);
            }
        }
    }
}