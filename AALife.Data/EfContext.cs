using AALife.Core;
using AALife.Core.Domain.Configuration;
using AALife.Core.Domain.Logging;
using AALife.Core.Mapping.Configuration;
using AALife.Core.Mapping.Logging;
using AALife.Data.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AALife.Data
{
    /// <summary>
    /// Object context
    /// </summary>
    public class EfContext : DbContext, IDbContext
    {
        #region Ctor

        public EfContext()
            : base("DefaultConnString")
        {
        }

        //系统部分
        public virtual IDbSet<Setting> Settings { get; set; }
        public virtual IDbSet<Log> Logs { get; set; }
        public virtual IDbSet<ActivityLog> ActivityLogs { get; set; }

        //业务部分
        public virtual IDbSet<ItemTable> ItemTables { get; set; }
        public virtual IDbSet<UserTable> UserTables { get; set; }
        public virtual IDbSet<CategoryTypeTable> CategoryTypeTables { get; set; }
        public virtual IDbSet<CardTable> CardTables { get; set; }
        public virtual IDbSet<OAuthTable> OAuthTables { get; set; }
        public virtual IDbSet<ZhuanTiTable> ZhuanTiTables { get; set; }
        public virtual IDbSet<ZhuanZhangTable> ZhuanZhangTables { get; set; }

        public EfContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = false;
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SettingMap());
            modelBuilder.Configurations.Add(new ActivityLogMap());
            modelBuilder.Configurations.Add(new LogMap());

            //移除级联删除
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get DbSet
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>DbSet</returns>
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        #endregion
    }
}