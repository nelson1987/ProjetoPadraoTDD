using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_GRUPOConfiguration : EntityTypeConfiguration<GRUPO>
    {
        public WFD_GRUPOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.GRUPO_NM)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_GRUPO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.GRUPO_NM).HasColumnName("GRUPO_NM");
        }
    }
}