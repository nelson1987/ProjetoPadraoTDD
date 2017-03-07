using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class T_UFConfiguration : EntityTypeConfiguration<TiposDeEstado>
    {
        public T_UFConfiguration()
        {
            // Primary Key
            HasKey(t => t.UF_SGL);

            // Properties
            Property(t => t.UF_SGL)
                .IsRequired()
                .HasMaxLength(2);

            Property(t => t.UF_NM)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            ToTable("UTIL_ESTADO");
            Property(t => t.UF_SGL).HasColumnName("UF_SGL");
            Property(t => t.UF_NM).HasColumnName("UF_NM");
        }
    }
}