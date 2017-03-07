using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_TIPO_DOCUMENTOSConfiguration : EntityTypeConfiguration<TipoDeDocumento>
    {
        public WFD_TIPO_DOCUMENTOSConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.DESCRICAO)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_DOCUMENTOS");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.TIPO_DOCUMENTOS_CH_ID).HasColumnName("TIPO_DOCUMENTOS_CH_ID");
            Property(t => t.ATIVO).HasColumnName("ATIVO");

            // Relationships
            HasOptional(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFD_TIPO_DOCUMENTOS)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasOptional(t => t.WFD_TIPO_DOCUMENTOS_CH)
                .WithMany(t => t.WFD_TIPO_DOCUMENTOS)
                .HasForeignKey(d => d.TIPO_DOCUMENTOS_CH_ID);
        }
    }
}