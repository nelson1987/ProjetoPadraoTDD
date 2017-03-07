using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPFConfiguration : EntityTypeConfiguration<Fornecedor>
    {
        public WFD_PJPFConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.RAZAO_SOCIAL)
                .HasMaxLength(255);

            Property(t => t.CNPJ)
                .HasMaxLength(20);

            Property(t => t.CPF)
                .HasMaxLength(20);

            Property(t => t.TELEFONE)
                .HasMaxLength(20);

            Property(t => t.NOME)
                .HasMaxLength(100);

            Property(t => t.NOME_FANTASIA)
                .HasMaxLength(255);

            //Property(t => t.CNAE)
            //    .HasMaxLength(50);

            Property(t => t.INSCR_ESTADUAL)
                .HasMaxLength(20);

            Property(t => t.INSCR_MUNICIPAL)
                .HasMaxLength(20);

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

            Property(t => t.EMAIL)
                .HasMaxLength(255);

            Property(t => t.RF_SIT_CADASTRAL_CNPJ)
                .HasMaxLength(50);

            Property(t => t.IBGE_COD)
                .HasMaxLength(7);

            Property(t => t.SINT_IE_COD)
                .HasMaxLength(14);

            Property(t => t.SINT_IE_SITU_CADASTRAL)
                .HasMaxLength(35);

            Property(t => t.SIMPLES_NACIONAL_SITUACAO)
                .HasMaxLength(100);

            Property(t => t.SUFRAMA_SIT_CADASTRAL)
                .HasMaxLength(14);

            Property(t => t.SUFRAMA_INSCRICAO)
                .HasMaxLength(9);

            // Table & Column Mappings
            ToTable("WFL_PJPF");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.RAZAO_SOCIAL).HasColumnName("RAZAO_SOCIAL");
            Property(t => t.CNPJ).HasColumnName("CNPJ");
            Property(t => t.CPF).HasColumnName("CPF");
            Property(t => t.TELEFONE).HasColumnName("TELEFONE");
            Property(t => t.ATIVO).HasColumnName("ATIVO");
            Property(t => t.NOME).HasColumnName("NOME");
            Property(t => t.NOME_FANTASIA).HasColumnName("NOME_FANTASIA");
            //Property(t => t.CNAE).HasColumnName("CNAE");
            Property(t => t.INSCR_ESTADUAL).HasColumnName("INSCR_ESTADUAL");
            Property(t => t.INSCR_MUNICIPAL).HasColumnName("INSCR_MUNICIPAL");
            Property(t => t.ENDERECO).HasColumnName("ENDERECO");
            Property(t => t.NUMERO).HasColumnName("NUMERO");
            Property(t => t.COMPLEMENTO).HasColumnName("COMPLEMENTO");
            Property(t => t.CEP).HasColumnName("CEP");
            Property(t => t.BAIRRO).HasColumnName("BAIRRO");
            Property(t => t.CIDADE).HasColumnName("CIDADE");
            Property(t => t.UF).HasColumnName("UF");
            Property(t => t.PAIS).HasColumnName("PAIS");
            Property(t => t.EMAIL).HasColumnName("EMAIL");
            Property(t => t.TIPO_PJPF_ID).HasColumnName("TIPO_PJPF_ID");
            Property(t => t.SITUACAO_ID).HasColumnName("SITUACAO_ID");
            Property(t => t.ROBO_ID).HasColumnName("ROBO_ID");
            Property(t => t.RF_SIT_CADASTRAL_CNPJ).HasColumnName("RF_SIT_CADASTRAL_CNPJ");
            Property(t => t.RF_SIT_CADASTRAL_CNPJ_DT).HasColumnName("RF_SIT_CADASTRAL_CNPJ_DT");
            Property(t => t.RF_CONSULTA_DTHR).HasColumnName("RF_CONSULTA_DTHR");
            Property(t => t.IBGE_COD).HasColumnName("IBGE_COD");
            Property(t => t.SINT_IE_COD).HasColumnName("SINT_IE_COD");
            Property(t => t.SINT_DTHR_CONSULTA).HasColumnName("SINT_DTHR_CONSULTA");
            Property(t => t.SINT_IE_SITU_CADASTRAL).HasColumnName("SINT_IE_SITU_CADASTRAL");
            Property(t => t.SINT_IE_SITU_CADASTRAL_DT).HasColumnName("SINT_IE_SITU_CADASTRAL_DT");
            Property(t => t.SIMPLES_NACIONAL_SITUACAO).HasColumnName("SIMPLES_NACIONAL_SITUACAO");
            Property(t => t.SUFRAMA_SIT_CADASTRAL).HasColumnName("SUFRAMA_SIT_CADASTRAL");
            Property(t => t.SUFRAMA_INSCRICAO).HasColumnName("SUFRAMA_INSCRICAO");
            Property(t => t.SUFRAMA_SIT_CADASTRAL_VALIDADE).HasColumnName("SUFRAMA_SIT_CADASTRAL_VALIDADE");
            Property(t => t.DT_NASCIMENTO).HasColumnName("DT_NASCIMENTO");
            Property(t => t.DT_ATUALIZACAO_UNSPSC).HasColumnName("DT_ATUALIZACAO_UNSPSC");

            // Relationships
            HasOptional(t => t.T_UF)
                .WithMany(t => t.WFD_PJPF)
                .HasForeignKey(d => d.UF);
            HasRequired(t => t.Contratante)
                .WithMany(t => t.WFD_PJPF)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasOptional(t => t.ROBO)
                .WithMany(t => t.WFD_PJPF)
                .HasForeignKey(d => d.ROBO_ID);
        }
    }
}