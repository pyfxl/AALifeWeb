using AALife.Core;
using AALife.Data.Domain;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AALife.Data
{
    /// <summary>
    /// Object context
    /// </summary>
    public class AALifeContext : DbContext, IDbContext
    {
        #region Ctor

        public AALifeContext()
            : base("DefaultConnString")
        {
        }

        //ҵ�񲿷�
        public virtual IDbSet<UserTable> UserTables { get; set; }
        public virtual IDbSet<ItemTable> ItemTables { get; set; }
        public virtual IDbSet<CategoryTypeTable> CategoryTypeTables { get; set; }
        public virtual IDbSet<CardTable> CardTables { get; set; }
        public virtual IDbSet<OAuthTable> OAuthTables { get; set; }
        public virtual IDbSet<ZhuanTiTable> ZhuanTiTables { get; set; }
        public virtual IDbSet<ZhuanZhangTable> ZhuanZhangTables { get; set; }

        public AALifeContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = false;
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //�Ƴ�����ɾ��
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