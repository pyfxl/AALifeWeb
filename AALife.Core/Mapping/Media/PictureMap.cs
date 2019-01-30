using AALife.Core.Domain.Media;
using System.Data.Entity.ModelConfiguration;

namespace AALife.Core.Mapping.Media
{
    public partial class PictureMap : EntityTypeConfiguration<Picture>
    {
        public PictureMap()
        {
            this.ToTable("bse_Picture");
            this.HasKey(p => p.Id);
            this.Property(p => p.FileName).IsRequired().HasMaxLength(100);
            this.Property(p => p.FileExtName).IsRequired().HasMaxLength(10);
            this.Property(p => p.MimeType).IsRequired().HasMaxLength(40);
        }
    }
}