using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFL_T_TP_PAPELConfiguration : EntityTypeConfiguration<TipoDePapel>
    {
        public WFL_T_TP_PAPELConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.TP_PAPEL_NM)
                .IsRequired()
                .HasMaxLength(100);

            Ignore(t => t.EhValido);
            Ignore(t => t.ValidationResult);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_PAPEL");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.TP_PAPEL_NM).HasColumnName("TP_PAPEL_NM");
        }
    }
}