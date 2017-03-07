using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_BANCOConfiguration : EntityTypeConfiguration<BancoDoFornecedor>
    {
        public WFD_PJPF_BANCOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.AGENCIA)
                .IsRequired()
                .HasMaxLength(4);

            Property(t => t.AG_DV)
                .HasMaxLength(1);

            Property(t => t.CONTA)
                .IsRequired()
                .HasMaxLength(18);

            Property(t => t.CONTA_DV)
                .IsRequired()
                .HasMaxLength(2);

            Property(t => t.NOME_ARQUIVO)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_PJPF_BANCO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.BANCO_ID).HasColumnName("BANCO_ID");
            Property(t => t.AGENCIA).HasColumnName("AGENCIA");
            Property(t => t.AG_DV).HasColumnName("AG_DV");
            Property(t => t.CONTA).HasColumnName("CONTA");
            Property(t => t.CONTA_DV).HasColumnName("CONTA_DV");
            Property(t => t.ATIVO).HasColumnName("ATIVO");
            Property(t => t.CONTRATANTE_PJPF_ID).HasColumnName("CONTRATANTE_PJPF_ID");
            Property(t => t.ARQUIVO_ID).HasColumnName("ARQUIVO_ID");
            Property(t => t.NOME_ARQUIVO).HasColumnName("NOME_ARQUIVO");
            Property(t => t.DATA_UPLOAD).HasColumnName("DATA_UPLOAD");

            // Relationships
            HasRequired(t => t.T_BANCO)
                .WithMany(t => t.BancosDoFornecedor)
                .HasForeignKey(d => d.BANCO_ID);
            HasOptional(t => t.WFD_ARQUIVOS)
                .WithMany(t => t.WFD_PJPF_BANCO)
                .HasForeignKey(d => d.ARQUIVO_ID);
            HasOptional(t => t.WFD_CONTRATANTE_PJPF)
                .WithMany(t => t.BancoDoFornecedor)
                .HasForeignKey(d => d.CONTRATANTE_PJPF_ID);
        }
    }
}