using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class T_UNSPSCConfiguration : EntityTypeConfiguration<TIPO_UNSPSC>
    {
        public T_UNSPSCConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.UNSPSC_DSC)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_UNSPSC");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.UNSPSC_COD).HasColumnName("UNSPSC_COD");
            Property(t => t.UNSPSC_DSC).HasColumnName("UNSPSC_DSC");
            Property(t => t.NIV).HasColumnName("NIV");
        }
    }
}