using AALife.Core;
using AALife.Core.Infrastructure;
using AALife.Core.Infrastructure.Mapper;
using AALife.Core.Services;
using AALife.Core.Services.Logging;
using AALife.WebMvc.Infrastructure.DependencyManagement;
using AALife.WebMvc.Infrastructure.Mapper;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace AALife.WebMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //initialize engine context
            EngineContext.Initialize(false);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //webapi返回json
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
            
            //register dependencies
            RegisterDependencies();
            
            //register mapper configurations
            //RegisterMapperConfiguration();

            //log application start
            try
            {
                //log
                var logger = EngineContext.Current.Resolve<ILogger>();
                logger.Information("Application started", null);
            }
            catch (Exception)
            {
                //don't throw new exception if occurs
            }
        }

        /// <summary>
        /// Register dependencies
        /// </summary>
        /// <param name="config">Config</param>
        protected virtual void RegisterDependencies()
        {
            //var builder = new ContainerBuilder();

            //var dependencyRegistrar = new DependencyRegistrar();
            //dependencyRegistrar.Register(builder, null);

            var container = EngineContext.Current.ContainerManager.Container;

            //set dependency resolver
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //注入webapi
            GlobalConfiguration.Configuration.DependencyResolver = (new AutofacWebApiDependencyResolver(container));
        }

        /// <summary>
        /// Register mapping
        /// </summary>
        protected virtual void RegisterMapperConfiguration()
        {
            //mapper配置
            var mapper = new AALife.WebMvc.Infrastructure.Mapper.MapperConfiguration();

            //get configurations
            var configurationActions = new List<Action<IMapperConfigurationExpression>>();
            configurationActions.Add(mapper.GetConfiguration());

            //register
            AutoMapperConfiguration.Init(configurationActions);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();

            //log error
            LogException(exception);

            //process 404 HTTP errors
            var httpException = exception as HttpException;
            if (httpException != null && (httpException.GetHttpCode() == 400 || httpException.GetHttpCode() == 404))
            {
                //Response.WriteFile("~/FileNotFound.htm");
                Response.Write(httpException.Message);
                Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                Response.End();
                Server.ClearError();
                return;
            }
        }

        protected void LogException(Exception exc)
        {
            if (exc == null)
                return;

            //ignore 404 HTTP errors
            //var httpException = exc as HttpException;
            //if (httpException != null && (httpException.GetHttpCode() == 400 || httpException.GetHttpCode() == 404))
            //    return;

            try
            {
                //log
                var logger = EngineContext.Current.Resolve<ILogger>();
                logger.Error(exc.Message, exc);
            }
            catch (Exception)
            {
                //don't throw new exception if occurs
            }
        }
    }

}
