using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFL_T_TP_FLUXOConfiguration : EntityTypeConfiguration<TipoDeFluxo>
    {
        public WFL_T_TP_FLUXOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_FLUXO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.Nome).HasColumnName("Nome");
        }
    }
}