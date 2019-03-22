using AALife.Core.Domain.Configuration;
using AALife.Core.Domain.Logging;
using AALife.Core.Domain.Media;
using AALife.Core.Domain.Messages;
using AALife.Core.Domain.Tasks;
using AALife.Core.Mapping.Configuration;
using AALife.Core.Mapping.Logging;
using AALife.Core.Mapping.Media;
using AALife.Core.Mapping.Messages;
using AALife.Data.Mapping.Tasks;
using System.Data.Entity;

namespace AALife.Core
{
    /// <summary>
    /// Object context
    /// </summary>
    public class EfContext : DbContext, IDbContext
    {
        #region Ctor

        public EfContext()
            : base("BaseConnString")
        {
        }

        public EfContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = false;
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.ProxyCreationEnabled = false;
        }

        //系统部分
        public virtual IDbSet<Setting> Settings { get; set; }
        public virtual IDbSet<Log> Logs { get; set; }
        public virtual IDbSet<ActivityLog> ActivityLogs { get; set; }
        public virtual IDbSet<Picture> Pictures { get; set; }
        public virtual IDbSet<Parameter> Parameters { get; set; }
        public virtual IDbSet<MessageTemplate> MessageTemplates { get; set; }
        public virtual IDbSet<QueuedEmail> QueuedEmails { get; set; }
        public virtual IDbSet<ScheduleTask> ScheduleTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SettingMap());
            modelBuilder.Configurations.Add(new ActivityLogMap());
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new PictureMap());
            modelBuilder.Configurations.Add(new ParameterMap());
            modelBuilder.Configurations.Add(new MessageTemplateMap());
            modelBuilder.Configurations.Add(new QueuedEmailMap());
            modelBuilder.Configurations.Add(new ScheduleTaskMap());

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