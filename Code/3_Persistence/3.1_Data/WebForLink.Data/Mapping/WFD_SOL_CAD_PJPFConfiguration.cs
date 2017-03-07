using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOL_CAD_PJPFConfiguration : EntityTypeConfiguration<SolicitacaoCadastroFornecedor>
    {
        public WFD_SOL_CAD_PJPFConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.CPF)
                .HasMaxLength(18);

            Property(t => t.CNPJ)
                .HasMaxLength(18);

            Property(t => t.RAZAO_SOCIAL)
                .HasMaxLength(140);

            Property(t => t.NOME)
                .HasMaxLength(100);

            Property(t => t.NOME_FANTASIA)
                .HasMaxLength(100);

            Property(t => t.CNAE)
                .HasMaxLength(20);

            Property(t => t.INSCR_ESTADUAL)
                .HasMaxLength(18);

            Property(t => t.INSCR_MUNICIPAL)
                .HasMaxLength(18);

            Property(t => t.TP_LOGRADOURO)
                .HasMaxLength(10);

            Property(t => t.ENDERECO)
                .HasMaxLength(60);

            Property(t => t.NUMERO)
                .HasMaxLength(10);

            Property(t => t.COMPLEMENTO)
                .HasMaxLength(50);

            Property(t => t.CEP)
                .HasMaxLength(50);

            Property(t => t.BAIRRO)
                .HasMaxLength(40);

            Property(t => t.CIDADE)
                .HasMaxLength(35);

            Property(t => t.UF)
                .HasMaxLength(2);

            Property(t => t.PAIS)
                .HasMaxLength(2);

            Property(t => t.OBSERVACAO)
                .HasMaxLength(255);

            Property(t => t.COD_PJPF_ERP)
                .HasMaxLength(100);

            Property(t => t.CLIENTE)
                .HasMaxLength(255);

            Property(t => t.GRUPO_EMPRESA)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_SOL_CAD_PJPF");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.CATEGORIA_ID).HasColumnName("CATEGORIA_ID");
            Property(t => t.ORG_COMPRAS_ID).HasColumnName("ORG_COMPRAS_ID");
            Property(t => t.PJPF_TIPO).HasColumnName("PJPF_TIPO");
            Property(t => t.CPF).HasColumnName("CPF");
            Property(t => t.CNPJ).HasColumnName("CNPJ");
            Property(t => t.RAZAO_SOCIAL).HasColumnName("RAZAO_SOCIAL");
            Property(t => t.NOME).HasColumnName("NOME");
            Property(t => t.NOME_FANTASIA).HasColumnName("NOME_FANTASIA");
            Property(t => t.CNAE).HasColumnName("CNAE");
            Property(t => t.INSCR_ESTADUAL).HasColumnName("INSCR_ESTADUAL");
            Property(t => t.INSCR_MUNICIPAL).HasColumnName("INSCR_MUNICIPAL");
            Property(t => t.TP_LOGRADOURO).HasColumnName("TP_LOGRADOURO");
            Property(t => t.ENDERECO).HasColumnName("ENDERECO");
            Property(t => t.NUMERO).HasColumnName("NUMERO");
            Property(t => t.COMPLEMENTO).HasColumnName("COMPLEMENTO");
            Property(t => t.CEP).HasColumnName("CEP");
            Property(t => t.BAIRRO).HasColumnName("BAIRRO");
            Property(t => t.CIDADE).HasColumnName("CIDADE");
            Property(t => t.UF).HasColumnName("UF");
            Property(t => t.PAIS).HasColumnName("PAIS");
            Property(t => t.OBSERVACAO).HasColumnName("OBSERVACAO");
            Property(t => t.EhExpansao).HasColumnName("EXPANSAO");
            Property(t => t.EXPANSAO_PARA_CONTR_ID).HasColumnName("EXPANSAO_PARA_CONTR_ID");
            Property(t => t.COD_PJPF_ERP).HasColumnName("COD_PJPF_ERP");
            Property(t => t.ROBO_ID).HasColumnName("ROBO_ID");
            Property(t => t.CLIENTE).HasColumnName("CLIENTE");
            Property(t => t.GRUPO_EMPRESA).HasColumnName("GRUPO_EMPRESA");
            Property(t => t.DT_NASCIMENTO).HasColumnName("DT_NASCIMENTO");

            // Relationships
            HasRequired(t => t.WFD_PJPF_CATEGORIA)
                .WithMany(t => t.WFD_SOL_CAD_PJPF)
                .HasForeignKey(d => d.CATEGORIA_ID);
            HasOptional(t => t.WFD_PJPF_ROBO)
                .WithMany(t => t.WFD_SOL_CAD_PJPF)
                .HasForeignKey(d => d.ROBO_ID);
            HasRequired(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.SolicitacaoCadastroFornecedor)
                .HasForeignKey(d => d.SOLICITACAO_ID);
        }
    }
}