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

        //业务部分
        public virtual IDbSet<UserTable> UserTables { get; set; }
        public virtual IDbSet<UserRole> UserRoles { get; set; }
        public virtual IDbSet<UserPermission> UserPermissions { get; set; }
        public virtual IDbSet<UserDeptment> UserDeptments { get; set; }
        public virtual IDbSet<UserPosition> UserPositions { get; set; }

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
        public IDbSet<TEntity> Set<TEntity, TPrimaryKey>() where TEntity : BaseEntity<TPrimaryKey>
        {
            return base.Set<TEntity>();
        }

        #endregion
    }
}