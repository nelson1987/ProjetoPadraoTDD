using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_DOCUMENTOSConfiguration : EntityTypeConfiguration<DocumentosDoFornecedor>
    {
        public WFD_PJPF_DOCUMENTOSConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.NOME_ARQUIVO)
                .HasMaxLength(255);

            Property(t => t.EXTENSAO_ARQUIVO)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("WFL_PJPF_DOCUMENTOS");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PJPF_ID).HasColumnName("PJPF_ID");
            Property(t => t.ARQUIVO_ID).HasColumnName("ARQUIVO_ID");
            Property(t => t.NOME_ARQUIVO).HasColumnName("NOME_ARQUIVO");
            Property(t => t.DESCRICAO_DOCUMENTO_ID).HasColumnName("DESCRICAO_DOCUMENTO_ID");
            Property(t => t.DATA_UPLOAD).HasColumnName("DATA_UPLOAD");
            Property(t => t.DATA_VENCIMENTO).HasColumnName("DATA_VENCIMENTO");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.EXTENSAO_ARQUIVO).HasColumnName("EXTENSAO_ARQUIVO");
            Property(t => t.CONTRATANTE_PJPF_ID).HasColumnName("CONTRATANTE_PJPF_ID");
            Property(t => t.LISTA_DOCUMENTO_ID).HasColumnName("LISTA_DOCUMENTO_ID");
            Property(t => t.EXIGE_VALIDADE).HasColumnName("EXIGE_VALIDADE");
            Property(t => t.PERIODICIDADE_ID).HasColumnName("PERIODICIDADE_ID");
            Property(t => t.OBRIGATORIO).HasColumnName("OBRIGATORIO");
            Property(t => t.DATA_EMISSAO).HasColumnName("DATA_EMISSAO");
            Property(t => t.ATIVO).HasColumnName("ATIVO");
            Property(t => t.SEM_VALIDADE).HasColumnName("SEM_VALIDADE");

            // Relationships
            HasOptional(t => t.WFD_ARQUIVOS)
                .WithMany(t => t.WFD_PJPF_DOCUMENTOS)
                .HasForeignKey(d => d.ARQUIVO_ID);
            HasOptional(t => t.WFD_CONTRATANTE_PJPF)
                .WithMany(t => t.WFD_PJPF_DOCUMENTOS)
                .HasForeignKey(d => d.CONTRATANTE_PJPF_ID);
            HasRequired(t => t.DescricaoDeDocumentos)
                .WithMany(t => t.DocumentosDeFornecedor)
                .HasForeignKey(d => d.DESCRICAO_DOCUMENTO_ID);
            HasRequired(t => t.WFD_PJPF)
                .WithMany(t => t.DocumentosDoFornecedor)
                .HasForeignKey(d => d.PJPF_ID);
            HasOptional(t => t.WFD_PJPF_LISTA_DOCUMENTOS)
                .WithMany(t => t.WFD_PJPF_DOCUMENTOS)
                .HasForeignKey(d => d.LISTA_DOCUMENTO_ID);
            HasOptional(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.DocumentosDoFornecedor)
                .HasForeignKey(d => d.SOLICITACAO_ID);
            HasOptional(t => t.WFD_T_PERIODICIDADE)
                .WithMany(t => t.WFD_PJPF_DOCUMENTOS)
                .HasForeignKey(d => d.PERIODICIDADE_ID);
        }
    }
}