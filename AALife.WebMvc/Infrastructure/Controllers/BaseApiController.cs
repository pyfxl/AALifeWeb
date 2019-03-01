using AALife.Core.Infrastructure;
using AALife.Core.Services.Logging;
using AALife.Data;
using AALife.Data.Infrastructure.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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
                Errors = errorMessage
            };

            return Content(HttpStatusCode.BadRequest, gridModel);
        }

        /// <summary>
        /// Error's json data for kendo grid
        /// </summary>
        /// <param name="errorMessage">Error message</param>
        /// <returns>Error's json data</returns>
        protected IHttpActionResult ErrorForKendoGridJson(ModelStateDictionary ModelState)
        {
            var errorMessage = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage).FirstOrDefault();

            return ErrorForKendoGridJson(errorMessage);
        }

    }
}