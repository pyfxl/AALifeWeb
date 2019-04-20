using AALife.Core.Domain.Media;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Media
{
    public partial class DownloadMap : EntityTypeConfiguration<Download>
    {
        public DownloadMap()
        {
            this.ToTable("bse_Download");
            this.HasKey(p => p.Id);
            this.Property(p => p.DownloadBinary).IsMaxLength();
            this.Property(p => p.Filename).HasMaxLength(100);
            this.Property(p => p.Extension).HasMaxLength(10);
            this.Property(p => p.ContentType).HasMaxLength(40);
            this.Property(p => p.DownloadUrl).HasMaxLength(200);
        }
    }
}