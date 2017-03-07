using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFL_FLUXOConfiguration : EntityTypeConfiguration<Fluxo>
    {
        public WFL_FLUXOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.FLUXO_NM)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_FLUXO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.FLUXO_NM).HasColumnName("FLUXO_NM");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.APLICACAO_ID).HasColumnName("APLICACAO_ID");
            Property(t => t.PAPEL_INI_FLUXO).HasColumnName("PAPEL_INI_FLUXO");
            Property(t => t.FLUXO_TP_ID).HasColumnName("FLUXO_TP_ID");

            // Relationships
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFL_FLUXO)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasRequired(t => t.WflTTpDeFluxo)
                .WithMany(t => t.Fluxo)
                .HasForeignKey(d => d.FLUXO_TP_ID);
        }
    }
}