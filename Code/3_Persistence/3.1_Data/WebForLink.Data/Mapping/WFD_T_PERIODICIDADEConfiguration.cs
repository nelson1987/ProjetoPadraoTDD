using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_T_PERIODICIDADEConfiguration : EntityTypeConfiguration<TIPO_PERIODICIDADE>
    {
        public WFD_T_PERIODICIDADEConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.PERIODICIDADE_NM)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_PERIODICIDADE");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PERIODICIDADE_NM).HasColumnName("PERIODICIDADE_NM");
            Property(t => t.DIAS).HasColumnName("DIAS");
        }
    }
}