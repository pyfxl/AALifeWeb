using AALife.Core;
using AALife.Core.Authentication;
using AALife.Core.Caching;
using AALife.Core.Services;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;


namespace AALife.WebMvc.DependencyManagement
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
        public virtual void Register(ContainerBuilder builder)
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

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            //controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //webapi
            builder.RegisterApiControllers(typeof(MvcApplication).Assembly);

            //db
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<EfContext>().As<IDbContext>().InstancePerLifetimeScope();

            //cache
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("aalife_cache_static").SingleInstance();

            //authentication
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();

            //service
            builder.RegisterType<ItemService>().As<IItemService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            builder.RegisterType<CategoryTypeService>().As<ICategoryTypeService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<CardService>().As<ICardService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<ZhuanTiService>().As<IZhuanTiService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();
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
