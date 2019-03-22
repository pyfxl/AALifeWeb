using AALife.Core;
using AALife.Core.Caching;
using AALife.Core.Infrastructure;
using AALife.Core.Infrastructure.DependencyManagement;
using AALife.Data;
using AALife.Data.Authentication;
using AALife.Data.Services;
using Autofac;
using Autofac.Core;

namespace AALife.WebMvc.Infrastructure.DependencyManagement
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
            //db
            builder.RegisterType<AALifeContext>().As<IDbContext>().Named<IDbContext>("aalife_context").InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            //authentication
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();

            //user
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<UserRoleService>().As<IUserRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<UserDeptmentService>().As<IUserDeptmentService>().InstancePerLifetimeScope();
            builder.RegisterType<UserPermissionService>().As<IUserPermissionService>().InstancePerLifetimeScope();

            //service
            builder.RegisterType<ItemService>().As<IItemService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<CategoryTypeService>().As<ICategoryTypeService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<CardService>().As<ICardService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<ZhuanTiService>().As<IZhuanTiService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();

            builder.RegisterType<ZhuanZhangService>().As<IZhuanZhangService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("aalife_cache_static"))
                .InstancePerLifetimeScope();

        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order
        {
            get { return 2; }
        }
    }

}
