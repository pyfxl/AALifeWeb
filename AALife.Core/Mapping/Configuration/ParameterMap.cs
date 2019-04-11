using AALife.Core.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AALife.Core.Mapping.Configuration
{
    public partial class ParameterMap : EntityTypeConfiguration<Parameter>
    {
        public ParameterMap()
        {
            this.ToTable("bse_Parameter");
            this.HasKey(p => p.Id);
            this.Property(p => p.Name).IsRequired().HasMaxLength(20);
            this.Property(p => p.Value).IsRequired().HasMaxLength(20);
            this.Property(p => p.Notes).HasMaxLength(200);
        }
    }
}
