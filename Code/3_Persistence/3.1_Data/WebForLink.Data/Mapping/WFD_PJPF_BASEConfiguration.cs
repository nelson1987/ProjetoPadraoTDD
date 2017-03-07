using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_BASEConfiguration : EntityTypeConfiguration<FORNECEDORBASE>
    {
        public WFD_PJPF_BASEConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.CPF)
                .HasMaxLength(11);

            Property(t => t.NOME)
                .HasMaxLength(255);

            Property(t => t.CNPJ)
                .HasMaxLength(20);

            Property(t => t.RAZAO_SOCIAL)
                .HasMaxLength(255);

            Property(t => t.NOME_FANTASIA)
                .HasMaxLength(255);

            Property(t => t.CNAE)
                .HasMaxLength(20);

            Property(t => t.INSCR_ESTADUAL)
                .HasMaxLength(20);

            Property(t => t.INSCR_MUNICIPAL)
                .HasMaxLength(20);

            Property(t => t.ENDERECO)
                .HasMaxLength(255);

            Property(t => t.NUMERO)
                .HasMaxLength(10);

            Property(t => t.COMPLEMENTO)
                .HasMaxLength(10);

            Property(t => t.CEP)
                .HasMaxLength(9);

            Property(t => t.BAIRRO)
                .HasMaxLength(50);

            Property(t => t.CIDADE)
                .HasMaxLength(50);

            Property(t => t.UF)
                .HasMaxLength(2);

            Property(t => t.NOME_CONTATO)
                .HasMaxLength(255);

            Property(t => t.EMAIL)
                .HasMaxLength(254);

            Property(t => t.TELEFONE)
                .HasMaxLength(20);

            Property(t => t.CELULAR)
                .HasMaxLength(20);

            Property(t => t.COD_ERP)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("WFL_PRECADASTRO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PLANILHA_ID).HasColumnName("PLANILHA_ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.PJPF_TIPO).HasColumnName("PJPF_TIPO");
            Property(t => t.CATEGORIA_ID).HasColumnName("CATEGORIA_ID");
            Property(t => t.ROBO_ID).HasColumnName("ROBO_ID");
            Property(t => t.CPF).HasColumnName("CPF");
            Property(t => t.NOME).HasColumnName("NOME");
            Property(t => t.DT_NASCIMENTO).HasColumnName("DT_NASCIMENTO");
            Property(t => t.CNPJ).HasColumnName("CNPJ");
            Property(t => t.RAZAO_SOCIAL).HasColumnName("RAZAO_SOCIAL");
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
            Property(t => t.NOME_CONTATO).HasColumnName("NOME_CONTATO");
            Property(t => t.EMAIL).HasColumnName("EMAIL");
            Property(t => t.TELEFONE).HasColumnName("TELEFONE");
            Property(t => t.CELULAR).HasColumnName("CELULAR");
            Property(t => t.ATIVO).HasColumnName("ATIVO");
            Property(t => t.DT_IMPORTACAO).HasColumnName("DT_IMPORTACAO");
            Property(t => t.EXECUTA_ROBO).HasColumnName("EXECUTA_ROBO");
            Property(t => t.DT_SOLICITACAO_ROBO).HasColumnName("DT_SOLICITACAO_ROBO");
            Property(t => t.ROBO_EXECUTADO).HasColumnName("ROBO_EXECUTADO");
            Property(t => t.ROBO_TENTATIVAS_EXCEDIDAS).HasColumnName("ROBO_TENTATIVAS_EXCEDIDAS");
            Property(t => t.COD_ERP).HasColumnName("COD_ERP");
            Property(t => t.NOVO_PJPF).HasColumnName("NOVO_PJPF");
            Property(t => t.DECISAO_BLOQUEIO).HasColumnName("DECISAO_BLOQUEIO");
            Property(t => t.PRECADASTRO).HasColumnName("PRECADASTRO");
            Property(t => t.STATUS_PRECADASTRO).HasColumnName("STATUS_PRECADASTRO");

            // Relationships
            HasOptional(t => t.T_UF)
                .WithMany(t => t.WFD_PJPF_BASE)
                .HasForeignKey(d => d.UF);
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFD_PJPF_BASE)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasOptional(t => t.WFD_PJPF_BASE_IMPORTACAO)
                .WithMany(t => t.WFD_PJPF_BASE)
                .HasForeignKey(d => d.PLANILHA_ID);
            HasOptional(t => t.WFD_PJPF_CATEGORIA)
                .WithMany(t => t.WFD_PJPF_BASE)
                .HasForeignKey(d => d.CATEGORIA_ID);
            HasOptional(t => t.ROBO)
                .WithMany(t => t.WFD_PJPF_BASE)
                .HasForeignKey(d => d.ROBO_ID);
            HasOptional(t => t.WFD_T_STATUS_PRECADASTRO)
                .WithMany(t => t.WFD_PJPF_BASE)
                .HasForeignKey(d => d.STATUS_PRECADASTRO);
        }
    }
}