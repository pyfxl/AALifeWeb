using AALife.Core;
using AALife.Core.Services;
using AALife.WebMvc.DependencyManagement;
using AALife.WebMvc.Mapper;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
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
            RegisterMapperConfiguration();
        }

        /// <summary>
        /// Register dependencies
        /// </summary>
        /// <param name="config">Config</param>
        protected virtual void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            var dependencyRegistrar = new DependencyRegistrar();
            dependencyRegistrar.Register(builder);

            var container = builder.Build();

            //set dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //注入webapi
            GlobalConfiguration.Configuration.DependencyResolver = (new AutofacWebApiDependencyResolver(container));
        }

        /// <summary>
        /// Register mapping
        /// </summary>
        /// <param name="config">Config</param>
        protected virtual void RegisterMapperConfiguration()
        {
            //mapper配置
            var mapper = new Mapper.MapperConfiguration();

            //get configurations
            var configurationActions = new List<Action<IMapperConfigurationExpression>>();
            configurationActions.Add(mapper.GetConfiguration());

            //register
            AutoMapperConfiguration.Init(configurationActions);
        }

    }

}
