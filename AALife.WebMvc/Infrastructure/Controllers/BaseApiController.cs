using AALife.Core.Infrastructure;
using AALife.Core.Services.Logging;
using AALife.Data;
using Kendo.DynamicLinq;
using System;
using System.Net;
using System.Web.Http;

namespace AALife.WebMvc
{
    [Authorize]
    public abstract partial class BaseApiController : ApiController
    {
        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="exc">Exception</param>
        protected void LogException(Exception exc)
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var logger = EngineContext.Current.Resolve<ILogger>();

            var user = workContext.CurrentUser;
            logger.Error(exc.Message, exc, user.Id);
        }

        /// <summary>
        /// Log exception
        /// </summary>
        /// <param name="exc">Exception</param>
        protected void LogException(string exc)
        {
            LogException(new Exception(exc));
        }

        /// <summary>
        /// Error's json data for kendo grid
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <returns>Error's json data</returns>
        protected IHttpActionResult ErrorForKendoGridJson(string errorMessage)
        {
            LogException(errorMessage);

            var gridModel = new DataSourceResult
            {
                //Errors = errorMessage
            };

            return Content(HttpStatusCode.BadRequest, gridModel);
        }

    }
}