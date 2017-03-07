using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOL_MOD_ENDERECOConfiguration : EntityTypeConfiguration<SOLICITACAO_MODIFICACAO_ENDERECO>
    {
        public WFD_SOL_MOD_ENDERECOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ENDERECO)
                .HasMaxLength(255);

            Property(t => t.NUMERO)
                .HasMaxLength(50);

            Property(t => t.COMPLEMENTO)
                .HasMaxLength(255);

            Property(t => t.CEP)
                .HasMaxLength(9);

            Property(t => t.BAIRRO)
                .HasMaxLength(100);

            Property(t => t.CIDADE)
                .HasMaxLength(100);

            Property(t => t.UF)
                .HasMaxLength(2);

            Property(t => t.PAIS)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("WFL_SOL_MOD_ENDERECO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.TP_ENDERECO_ID).HasColumnName("TP_ENDERECO_ID");
            Property(t => t.ENDERECO).HasColumnName("ENDERECO");
            Property(t => t.NUMERO).HasColumnName("NUMERO");
            Property(t => t.COMPLEMENTO).HasColumnName("COMPLEMENTO");
            Property(t => t.CEP).HasColumnName("CEP");
            Property(t => t.BAIRRO).HasColumnName("BAIRRO");
            Property(t => t.CIDADE).HasColumnName("CIDADE");
            Property(t => t.UF).HasColumnName("UF");
            Property(t => t.PAIS).HasColumnName("PAIS");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.PJPF_ID).HasColumnName("PJPF_ID");
            Property(t => t.PJPF_ENDERECO_ID).HasColumnName("PJPF_ENDERECO_ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");

            // Relationships
            HasOptional(t => t.T_UF)
                .WithMany(t => t.WFD_SOL_MOD_ENDERECO)
                .HasForeignKey(d => d.UF);
            HasOptional(t => t.WFD_PJPF)
                .WithMany(t => t.SolicitacaoModificacaoEnderecoList)
                .HasForeignKey(d => d.PJPF_ID);
            HasOptional(t => t.WFD_PJPF_ENDERECO)
                .WithMany(t => t.WFD_SOL_MOD_ENDERECO)
                .HasForeignKey(d => d.PJPF_ENDERECO_ID);
            HasOptional(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.WFD_SOL_MOD_ENDERECO)
                .HasForeignKey(d => d.SOLICITACAO_ID);
            HasRequired(t => t.WFD_T_TP_ENDERECO)
                .WithMany(t => t.WFD_SOL_MOD_ENDERECO)
                .HasForeignKey(d => d.TP_ENDERECO_ID);
        }
    }
}