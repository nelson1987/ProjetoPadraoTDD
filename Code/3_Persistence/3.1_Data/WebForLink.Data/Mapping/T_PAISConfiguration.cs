using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class T_PAISConfiguration : EntityTypeConfiguration<TiposDePais>
    {
        public T_PAISConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.PAIS_SGL)
                .IsRequired()
                .HasMaxLength(3);

            Property(t => t.PAIS_NM)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("UTIL_PAIS");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PAIS_SGL).HasColumnName("PAIS_SGL");
            Property(t => t.PAIS_NM).HasColumnName("PAIS_NM");
        }
    }
}