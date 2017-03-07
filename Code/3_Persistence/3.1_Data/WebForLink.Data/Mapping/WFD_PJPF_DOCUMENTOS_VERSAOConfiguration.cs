using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_DOCUMENTOS_VERSAOConfiguration : EntityTypeConfiguration<VersionamentoDeDocumentoDoFornecedor>
    {
        public WFD_PJPF_DOCUMENTOS_VERSAOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.NOME_ARQUIVO)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_PJPF_DOCUMENTOS_VERSAO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.DOCUMENTO_ID).HasColumnName("DOCUMENTO_ID");
            Property(t => t.ARQUIVO_ID).HasColumnName("ARQUIVO_ID");
            Property(t => t.DATA_UPLOAD).HasColumnName("DATA_UPLOAD");
            Property(t => t.DATA_VENCIMENTO).HasColumnName("DATA_VENCIMENTO");
            Property(t => t.NOME_ARQUIVO).HasColumnName("NOME_ARQUIVO");
            Property(t => t.USUARIO_ID).HasColumnName("USUARIO_ID");

            // Relationships
            HasRequired(t => t.DocumentosDoFornecedor)
                .WithMany(t => t.WFD_PJPF_DOCUMENTOS_VERSAO)
                .HasForeignKey(d => d.DOCUMENTO_ID);
            HasRequired(t => t.Usuario)
                .WithMany(t => t.WFD_PJPF_DOCUMENTOS_VERSAO)
                .HasForeignKey(d => d.USUARIO_ID);
        }
    }
}