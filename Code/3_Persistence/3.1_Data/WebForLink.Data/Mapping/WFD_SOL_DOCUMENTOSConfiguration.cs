using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOL_DOCUMENTOSConfiguration : EntityTypeConfiguration<SolicitacaoDeDocumentos>
    {
        public WFD_SOL_DOCUMENTOSConfiguration()
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
            ToTable("WFL_SOL_DOCUMENTOS");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.DESCRICAO_DOCUMENTO_ID).HasColumnName("DESCRICAO_DOCUMENTO_ID");
            Property(t => t.LISTA_DOCUMENTO_ID).HasColumnName("LISTA_DOCUMENTO_ID");
            Property(t => t.DATA_UPLOAD).HasColumnName("DATA_UPLOAD");
            Property(t => t.ARQUIVO_ID).HasColumnName("ARQUIVO_ID");
            Property(t => t.SITUACAO_ID).HasColumnName("SITUACAO_ID");
            Property(t => t.OBSERVACAO).HasColumnName("OBSERVACAO");
            Property(t => t.DATA_VENCIMENTO).HasColumnName("DATA_VENCIMENTO");
            Property(t => t.NOME_ARQUIVO).HasColumnName("NOME_ARQUIVO");
            Property(t => t.EXTENSAO_ARQUIVO).HasColumnName("EXTENSAO_ARQUIVO");
            Property(t => t.MENSAGEM_ID).HasColumnName("MENSAGEM_ID");
            Property(t => t.PJPF_DOCUMENTO_ID).HasColumnName("PJPF_DOCUMENTO_ID");
            Property(t => t.EXIGE_VALIDADE).HasColumnName("EXIGE_VALIDADE");
            Property(t => t.PERIODICIDADE_ID).HasColumnName("PERIODICIDADE_ID");
            Property(t => t.OBRIGATORIO).HasColumnName("OBRIGATORIO");

            // Relationships
            HasOptional(t => t.WFD_ARQUIVOS)
                .WithMany(t => t.WFD_SOL_DOCUMENTOS)
                .HasForeignKey(d => d.ARQUIVO_ID);
            HasRequired(t => t.DescricaoDeDocumentos)
                .WithMany(t => t.SolicitacaoDeDocumentos)
                .HasForeignKey(d => d.DESCRICAO_DOCUMENTO_ID);
            HasOptional(t => t.DocumentosDoFornecedor)
                .WithMany(t => t.WFD_SOL_DOCUMENTOS)
                .HasForeignKey(d => d.PJPF_DOCUMENTO_ID);
            HasOptional(t => t.ListaDeDocumentosDeFornecedor)
                .WithMany(t => t.WFD_SOL_DOCUMENTOS)
                .HasForeignKey(d => d.LISTA_DOCUMENTO_ID);
            HasOptional(t => t.WFD_SOL_MENSAGEM)
                .WithMany(t => t.WFD_SOL_DOCUMENTOS)
                .HasForeignKey(d => d.MENSAGEM_ID);
            HasOptional(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.SolicitacaoDeDocumentos)
                .HasForeignKey(d => d.SOLICITACAO_ID);
            HasOptional(t => t.WFD_T_PERIODICIDADE)
                .WithMany(t => t.WFD_SOL_DOCUMENTOS)
                .HasForeignKey(d => d.PERIODICIDADE_ID);
        }
    }
}