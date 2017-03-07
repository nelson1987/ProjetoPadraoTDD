using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_SOLICITACAO_DOCUMENTOSConfiguration :
        EntityTypeConfiguration<FORNECEDOR_SOLICITACAO_DOCUMENTOS>
    {
        public WFD_PJPF_SOLICITACAO_DOCUMENTOSConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.OBSERVACAO)
                .HasMaxLength(1024);

            Property(t => t.NOME_ARQUIVO)
                .HasMaxLength(255);

            Property(t => t.EXTENSAO_ARQUIVO)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("WFL_PJPF_SOLICITACAO_DOCUMENTOS");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PJPF_SOLICITACAO_ID).HasColumnName("PJPF_SOLICITACAO_ID");
            Property(t => t.PJPF_ID).HasColumnName("PJPF_ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.LISTA_DOCUMENTOS_ID).HasColumnName("LISTA_DOCUMENTOS_ID");
            Property(t => t.DATA_UPLOAD).HasColumnName("DATA_UPLOAD");
            Property(t => t.PJPF_ARQUIVO_ID).HasColumnName("PJPF_ARQUIVO_ID");
            Property(t => t.SITUACAO_ID).HasColumnName("SITUACAO_ID");
            Property(t => t.OBSERVACAO).HasColumnName("OBSERVACAO");
            Property(t => t.DATA_VENCIMENTO).HasColumnName("DATA_VENCIMENTO");
            Property(t => t.NOME_ARQUIVO).HasColumnName("NOME_ARQUIVO");
            Property(t => t.EXTENSAO_ARQUIVO).HasColumnName("EXTENSAO_ARQUIVO");

            // Relationships
            HasOptional(t => t.WFD_PJPF_BASE)
                .WithMany(t => t.WFD_PJPF_SOLICITACAO_DOCUMENTOS)
                .HasForeignKey(d => d.PJPF_ID);
            HasRequired(t => t.WFD_PJPF_LISTA_DOCUMENTOS)
                .WithMany(t => t.WFD_PJPF_SOLICITACAO_DOCUMENTOS)
                .HasForeignKey(d => d.LISTA_DOCUMENTOS_ID);
            HasRequired(t => t.WFD_PJPF_SOLICITACAO)
                .WithMany(t => t.WFD_PJPF_SOLICITACAO_DOCUMENTOS)
                .HasForeignKey(d => d.PJPF_SOLICITACAO_ID);
            HasOptional(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.WFD_PJPF_SOLICITACAO_DOCUMENTOS)
                .HasForeignKey(d => d.SOLICITACAO_ID);
        }
    }
}