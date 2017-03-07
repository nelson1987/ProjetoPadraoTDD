using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class T_BANCOConfiguration : EntityTypeConfiguration<TiposDeBanco>
    {
        public T_BANCOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.BANCO_COD)
                .IsRequired()
                .HasMaxLength(3);

            Property(t => t.BANCO_NM)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("UTIL_BANCO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.BANCO_COD).HasColumnName("BANCO_COD");
            Property(t => t.BANCO_NM).HasColumnName("BANCO_NM");
        }
    }
}