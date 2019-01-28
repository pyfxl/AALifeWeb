using AALife.Core.Caching;
using AALife.Core.Domain.Logging;
using AALife.Core.Services.Logging;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System.Linq;
using System.Web;

namespace AALife.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //HTTP context and other related stuff
            builder.Register(c =>
                //register FakeHttpContext when HttpContext is not available
                new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            //db
            //builder.RegisterType<BsContext>().As<IDbContext>().Named<IDbContext>("bs_context").InstancePerLifetimeScope();
            //builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>))
            //    .WithParameter(ResolvedParameter.ForNamed<IDbContext>("bs_context")).InstancePerLifetimeScope();

            //cache
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("aalife_cache_static").SingleInstance();

            //log
            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();

            //use static cache (between HTTP requests)
            builder.RegisterType<CustomerActivityService>().As<ICustomerActivityService>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("bs_context"))
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //webapi
            builder.RegisterApiControllers(typeFinder.GetAssemblies().ToArray());

        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 0; }
        }
    }

}
