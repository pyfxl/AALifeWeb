using AALife.Core.Caching;
using AALife.Core.Configuration;
using AALife.Core.Repositorys.Configuration;
using AALife.Core.Services.Configuration;
using AALife.Core.Services.Logging;
using AALife.Core.Services.Media;
using AALife.Core.Services.Security;
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

            //db
            builder.RegisterType<EfContext>().As<IDbContext>().Named<IDbContext>("ef_context").InstancePerLifetimeScope();

            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            //cache
            builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("aalife_cache_static").SingleInstance();

            //logger
            builder.RegisterType<DefaultLogger>().As<ILogger>().InstancePerLifetimeScope();

            #region services

            //encryption
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();

            //settings
            builder.RegisterType<SettingService>().As<ISettingService>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ef_context"))
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();
            builder.RegisterSource(new SettingsSource());

            //standard file system
            builder.RegisterType<PictureService>().As<IPictureService>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ef_context"))
                .InstancePerLifetimeScope();

            //activity log
            builder.RegisterType<CustomerActivityService>().As<ICustomerActivityService>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ef_context"))
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();

            //paremeter
            builder.RegisterType<ParameterService>().As<IParameterService>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ef_context"))
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();

            #endregion

            #region repositorys

            //picture
            builder.RegisterType<PictureRepository>().As<IPictureRepository>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ef_context"))
                .InstancePerLifetimeScope();

            //setting
            builder.RegisterType<SettingRepository>().As<ISettingRepository>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ef_context"))
                .InstancePerLifetimeScope();

            //log
            builder.RegisterType<LogRepository>().As<ILogRepository>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ef_context"))
                .InstancePerLifetimeScope();

            //activity log
            builder.RegisterType<ActivityLogRepository>().As<IActivityLogRepository>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ef_context"))
                .InstancePerLifetimeScope();

            //paremeter
            builder.RegisterType<ParameterRepository>().As<IParameterRepository>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("ef_context"))
                .InstancePerLifetimeScope();

            #endregion

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

    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
            "BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
                Service service,
                Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
        {
            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    //var currentStoreId = c.Resolve<IStoreContext>().CurrentStore.Id;
                    //uncomment the code below if you want load settings per store only when you have two stores installed.
                    //var currentStoreId = c.Resolve<IStoreService>().GetAllStores().Count > 1
                    //    c.Resolve<IStoreContext>().CurrentStore.Id : 0;

                    //although it's better to connect to your database and execute the following SQL:
                    //DELETE FROM [Setting] WHERE [StoreId] > 0
                    return c.Resolve<ISettingService>().LoadSetting<TSettings>(0);
                })
                .InstancePerLifetimeScope()
                .CreateRegistration();
        }

        public bool IsAdapterForIndividualComponents { get { return false; } }
    }

}
