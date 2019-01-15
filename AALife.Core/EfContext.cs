using AALife.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;

namespace AALife.Core
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

        public virtual IDbSet<ItemTable> ItemTables { get; set; }
        public virtual IDbSet<UserTable> UserTables { get; set; }
        public virtual IDbSet<CategoryTypeTable> CategoryTypeTables { get; set; }
        public virtual IDbSet<CardTable> CardTables { get; set; }
        public virtual IDbSet<OAuthTable> OAuthTables { get; set; }
        public virtual IDbSet<ZhuanTiTable> ZhuanTiTables { get; set; }
        public virtual IDbSet<ZhuanZhangTable> ZhuanZhangTables { get; set; }
        public virtual IDbSet<UserFromTable> UserFromTables { get; set; }
        public virtual IDbSet<UserLevelTable> UserLevelTables { get; set; }

        public EfContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.LazyLoadingEnabled = false;
            //((IObjectContextAdapter)this).ObjectContext.ContextOptions.ProxyCreationEnabled = false;
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