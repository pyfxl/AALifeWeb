using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace SexSpider.Core.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("name=DefaultConnString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SexSpiders>().ToTable("SexSpiders");
        }

        public virtual DbSet<SexSpiders> SexSpiders { get; set; }
    }
}