using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_DESCRICAO_DOCUMENTOSConfiguration : EntityTypeConfiguration<DescricaoDeDocumentos>
    {
        public WFD_DESCRICAO_DOCUMENTOSConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.DESCRICAO)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_DESCRICAO_DOCUMENTO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.TIPO_DOCUMENTOS_ID).HasColumnName("TIPO_DOCUMENTOS_ID");
            Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.DESCRICAO_DOCUMENTOS_CH_ID).HasColumnName("DESCRICAO_DOCUMENTOS_CH_ID");
            Property(t => t.ATIVO).HasColumnName("ATIVO");

            // Relationships
            HasOptional(t => t.Contratante)
                .WithMany(t => t.WFD_DESCRICAO_DOCUMENTOS)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasOptional(t => t.WFD_DESCRICAO_DOCUMENTOS_CH)
                .WithMany(t => t.WFD_DESCRICAO_DOCUMENTOS)
                .HasForeignKey(d => d.DESCRICAO_DOCUMENTOS_CH_ID);
            HasRequired(t => t.TipoDeDocumento)
                .WithMany(t => t.WFD_DESCRICAO_DOCUMENTOS)
                .HasForeignKey(d => d.TIPO_DOCUMENTOS_ID);
        }
    }
}