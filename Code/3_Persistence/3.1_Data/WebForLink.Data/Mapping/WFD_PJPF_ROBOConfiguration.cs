using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_ROBOConfiguration : EntityTypeConfiguration<ROBO>
    {
        public WFD_PJPF_ROBOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.CPF)
                .HasMaxLength(18);

            Property(t => t.CNPJ)
                .HasMaxLength(18);

            Property(t => t.RECEITA_FEDERAL_RAZAO_SOCIAL)
                .HasMaxLength(150);

            Property(t => t.RF_NOME_FANTASIA)
                .HasMaxLength(60);

            Property(t => t.RF_NOME)
                .HasMaxLength(150);

            Property(t => t.RF_LOGRADOURO)
                .HasMaxLength(70);

            Property(t => t.RF_NUMERO)
                .HasMaxLength(50);

            Property(t => t.RF_COMPLEMENTO)
                .HasMaxLength(100);

            Property(t => t.RF_BAIRRO)
                .HasMaxLength(50);

            Property(t => t.RF_MUNICIPIO)
                .HasMaxLength(40);

            Property(t => t.RF_UF)
                .HasMaxLength(2);

            Property(t => t.RF_CEP)
                .HasMaxLength(8);

            Property(t => t.RF_SIT_CADASTRAL_CNPJ)
                .HasMaxLength(50);

            Property(t => t.RF_SIT_ESPECIAL_CNPJ)
                .HasMaxLength(60);

            Property(t => t.RF_MOTIVO_CNPJ_SITU_CADASTRAL)
                .HasMaxLength(40);

            Property(t => t.RF_CNAE_COD_PRINCIPAL)
                .HasMaxLength(50);

            Property(t => t.RF_CNAE_DSC_PRINCIPAL)
                .HasMaxLength(150);

            Property(t => t.RF_CNAE_COD_OUTROS)
                .HasMaxLength(700);

            Property(t => t.RF_CNAE_DSC_OUTROS)
                .HasMaxLength(3400);

            Property(t => t.RF_MATRIZ_FILIAL)
                .HasMaxLength(10);

            Property(t => t.RF_COD_NATUREZA_JURIDICA)
                .HasMaxLength(5);

            Property(t => t.RF_DSC_NATUREZA_JURIDICA)
                .HasMaxLength(65);

            Property(t => t.IBGE_COD)
                .HasMaxLength(7);

            Property(t => t.SINTEGRA_ERRO_ORIGINAL)
                .HasMaxLength(50);

            Property(t => t.SINT_IE_QTD)
                .HasMaxLength(2);

            Property(t => t.SINT_IE_MULTIPLA)
                .HasMaxLength(3);

            Property(t => t.SINT_IE_MULTIPLA_CODIGOS)
                .HasMaxLength(100);

            Property(t => t.SINT_IE_MULTIPLA_SITUACAO)
                .HasMaxLength(100);

            Property(t => t.SINT_IE_COD)
                .HasMaxLength(14);

            Property(t => t.SINT_IE_SITU_CADASTRAL)
                .HasMaxLength(35);

            Property(t => t.SINT_IE_SITU_CADSTRAL_DT)
                .HasMaxLength(255);

            Property(t => t.SINT_BAIXA_MOTIVO)
                .HasMaxLength(30);

            Property(t => t.SINT_EMAIL)
                .HasMaxLength(50);

            Property(t => t.SINT_REGIME_APURACAO)
                .HasMaxLength(100);

            Property(t => t.SINT_ENQUADRAMENTO_FISCAL)
                .HasMaxLength(100);

            Property(t => t.SINT_TEL)
                .HasMaxLength(30);

            Property(t => t.SINT_CAD_PROD_RURAL)
                .HasMaxLength(15);

            Property(t => t.SINT_COMPLEMENTO)
                .HasMaxLength(100);

            Property(t => t.SINT_RAZAO_SOCIAL)
                .HasMaxLength(150);

            Property(t => t.SINT_CNPJ)
                .HasMaxLength(20);

            Property(t => t.SINT_BAIRRO)
                .HasMaxLength(50);

            Property(t => t.SINT_LOGRADOURO)
                .HasMaxLength(70);

            Property(t => t.SINT_NUMERO)
                .HasMaxLength(50);

            Property(t => t.SINT_CEP)
                .HasMaxLength(8);

            Property(t => t.SINT_MUNICIPIO)
                .HasMaxLength(40);

            Property(t => t.SINT_UF)
                .HasMaxLength(2);

            Property(t => t.SINT_ATIVIDADE_PRINCIPAL)
                .HasMaxLength(255);

            Property(t => t.SIMPLES_NACIONAL_SITUACAO)
                .HasMaxLength(100);

            Property(t => t.SN_SITUACAO_SIMEI)
                .HasMaxLength(50);

            Property(t => t.SN_PERIODOS_ANTERIORES)
                .HasMaxLength(400);

            Property(t => t.SN_SIMEI_PERIODOS_ANTERIORES)
                .HasMaxLength(200);

            Property(t => t.SN_AGENDAMENTOS)
                .HasMaxLength(100);

            Property(t => t.SN_RAZAO_SOCIAL)
                .HasMaxLength(150);

            Property(t => t.CORREIOS_TP_LOGRADOURO)
                .HasMaxLength(17);

            Property(t => t.CORR_LOGRADOURO)
                .HasMaxLength(70);

            Property(t => t.CORR_COMPLEMENTO)
                .HasMaxLength(40);

            Property(t => t.CORR_BAIRRO)
                .HasMaxLength(50);

            Property(t => t.CORR_BAIRRO_COMPL)
                .HasMaxLength(20);

            Property(t => t.CORR_UF)
                .HasMaxLength(2);

            Property(t => t.CORR_MUNICIPIO)
                .HasMaxLength(40);

            Property(t => t.CORR_CEP)
                .HasMaxLength(8);

            Property(t => t.SUFRAMA_ERRO_MENSAGEM)
                .HasMaxLength(60);

            Property(t => t.SUFRAMA_SIT_CADASTRAL)
                .HasMaxLength(14);

            Property(t => t.SUFRAMA_INSCRICAO)
                .HasMaxLength(9);

            Property(t => t.SUFRAMA_TEL)
                .HasMaxLength(30);

            Property(t => t.SUFRAMA_INCENTIVOS)
                .HasMaxLength(120);

            Property(t => t.SUFRAMA_EMAIL)
                .HasMaxLength(50);

            Property(t => t.LONGITUDE)
                .HasMaxLength(50);

            Property(t => t.LATITUDE)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_PJPF_ROBO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.ROBO_DT_EXEC).HasColumnName("ROBO_DT_EXEC");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.CPF).HasColumnName("CPF");
            Property(t => t.CNPJ).HasColumnName("CNPJ");
            Property(t => t.RECEITA_FEDERAL_RAZAO_SOCIAL).HasColumnName("RECEITA_FEDERAL_RAZAO_SOCIAL");
            Property(t => t.RF_NOME_FANTASIA).HasColumnName("RF_NOME_FANTASIA");
            Property(t => t.RF_NOME).HasColumnName("RF_NOME");
            Property(t => t.RF_LOGRADOURO).HasColumnName("RF_LOGRADOURO");
            Property(t => t.RF_NUMERO).HasColumnName("RF_NUMERO");
            Property(t => t.RF_COMPLEMENTO).HasColumnName("RF_COMPLEMENTO");
            Property(t => t.RF_BAIRRO).HasColumnName("RF_BAIRRO");
            Property(t => t.RF_MUNICIPIO).HasColumnName("RF_MUNICIPIO");
            Property(t => t.RF_UF).HasColumnName("RF_UF");
            Property(t => t.RF_CEP).HasColumnName("RF_CEP");
            Property(t => t.RF_SIT_CADASTRAL_CNPJ).HasColumnName("RF_SIT_CADASTRAL_CNPJ");
            Property(t => t.RF_SIT_CADSTRAL_CNPJ_DT).HasColumnName("RF_SIT_CADSTRAL_CNPJ_DT");
            Property(t => t.RF_SIT_ESPECIAL_CNPJ).HasColumnName("RF_SIT_ESPECIAL_CNPJ");
            Property(t => t.RF_SIT_ESPECIAL_CNPJ_DT).HasColumnName("RF_SIT_ESPECIAL_CNPJ_DT");
            Property(t => t.RF_MOTIVO_CNPJ_SITU_CADASTRAL).HasColumnName("RF_MOTIVO_CNPJ_SITU_CADASTRAL");
            Property(t => t.RF_CNPJ_DT_ABERTURA).HasColumnName("RF_CNPJ_DT_ABERTURA");
            Property(t => t.RF_CNAE_COD_PRINCIPAL).HasColumnName("RF_CNAE_COD_PRINCIPAL");
            Property(t => t.RF_CNAE_DSC_PRINCIPAL).HasColumnName("RF_CNAE_DSC_PRINCIPAL");
            Property(t => t.RF_CNAE_COD_OUTROS).HasColumnName("RF_CNAE_COD_OUTROS");
            Property(t => t.RF_CNAE_DSC_OUTROS).HasColumnName("RF_CNAE_DSC_OUTROS");
            Property(t => t.RF_MATRIZ_FILIAL).HasColumnName("RF_MATRIZ_FILIAL");
            Property(t => t.RF_COD_NATUREZA_JURIDICA).HasColumnName("RF_COD_NATUREZA_JURIDICA");
            Property(t => t.RF_DSC_NATUREZA_JURIDICA).HasColumnName("RF_DSC_NATUREZA_JURIDICA");
            Property(t => t.RF_CONSULTA_DTHR).HasColumnName("RF_CONSULTA_DTHR");
            Property(t => t.IBGE_COD).HasColumnName("IBGE_COD");
            Property(t => t.SINTEGRA_ERRO_ORIGINAL).HasColumnName("SINTEGRA_ERRO_ORIGINAL");
            Property(t => t.SINT_IE_QTD).HasColumnName("SINT_IE_QTD");
            Property(t => t.SINT_IE_MULTIPLA).HasColumnName("SINT_IE_MULTIPLA");
            Property(t => t.SINT_IE_MULTIPLA_CODIGOS).HasColumnName("SINT_IE_MULTIPLA_CODIGOS");
            Property(t => t.SINT_IE_MULTIPLA_SITUACAO).HasColumnName("SINT_IE_MULTIPLA_SITUACAO");
            Property(t => t.SINT_IE_COD).HasColumnName("SINT_IE_COD");
            Property(t => t.SINT_CONSULTA_DTHR).HasColumnName("SINT_CONSULTA_DTHR");
            Property(t => t.SINT_IE_SITU_CADASTRAL).HasColumnName("SINT_IE_SITU_CADASTRAL");
            Property(t => t.SINT_IE_SITU_CADSTRAL_DT).HasColumnName("SINT_IE_SITU_CADSTRAL_DT");
            Property(t => t.SINT_INCLUSAO_DT).HasColumnName("SINT_INCLUSAO_DT");
            Property(t => t.SINT_BAIXA_DT).HasColumnName("SINT_BAIXA_DT");
            Property(t => t.SINT_BAIXA_MOTIVO).HasColumnName("SINT_BAIXA_MOTIVO");
            Property(t => t.SINT_EMAIL).HasColumnName("SINT_EMAIL");
            Property(t => t.SINT_REGIME_APURACAO).HasColumnName("SINT_REGIME_APURACAO");
            Property(t => t.SINT_ENQUADRAMENTO_FISCAL).HasColumnName("SINT_ENQUADRAMENTO_FISCAL");
            Property(t => t.SINT_TEL).HasColumnName("SINT_TEL");
            Property(t => t.SINT_CAD_PROD_RURAL).HasColumnName("SINT_CAD_PROD_RURAL");
            Property(t => t.SINT_COMPLEMENTO).HasColumnName("SINT_COMPLEMENTO");
            Property(t => t.SINT_RAZAO_SOCIAL).HasColumnName("SINT_RAZAO_SOCIAL");
            Property(t => t.SINT_CNPJ).HasColumnName("SINT_CNPJ");
            Property(t => t.SINT_BAIRRO).HasColumnName("SINT_BAIRRO");
            Property(t => t.SINT_LOGRADOURO).HasColumnName("SINT_LOGRADOURO");
            Property(t => t.SINT_NUMERO).HasColumnName("SINT_NUMERO");
            Property(t => t.SINT_CEP).HasColumnName("SINT_CEP");
            Property(t => t.SINT_MUNICIPIO).HasColumnName("SINT_MUNICIPIO");
            Property(t => t.SINT_UF).HasColumnName("SINT_UF");
            Property(t => t.SINT_ATIVIDADE_PRINCIPAL).HasColumnName("SINT_ATIVIDADE_PRINCIPAL");
            Property(t => t.SIMPLES_NACIONAL_SITUACAO).HasColumnName("SIMPLES_NACIONAL_SITUACAO");
            Property(t => t.SN_SITUACAO_SIMEI).HasColumnName("SN_SITUACAO_SIMEI");
            Property(t => t.SN_PERIODOS_ANTERIORES).HasColumnName("SN_PERIODOS_ANTERIORES");
            Property(t => t.SN_SIMEI_PERIODOS_ANTERIORES).HasColumnName("SN_SIMEI_PERIODOS_ANTERIORES");
            Property(t => t.SN_AGENDAMENTOS).HasColumnName("SN_AGENDAMENTOS");
            Property(t => t.SN_RAZAO_SOCIAL).HasColumnName("SN_RAZAO_SOCIAL");
            Property(t => t.CORREIOS_TP_LOGRADOURO).HasColumnName("CORREIOS_TP_LOGRADOURO");
            Property(t => t.CORR_LOGRADOURO).HasColumnName("CORR_LOGRADOURO");
            Property(t => t.CORR_COMPLEMENTO).HasColumnName("CORR_COMPLEMENTO");
            Property(t => t.CORR_BAIRRO).HasColumnName("CORR_BAIRRO");
            Property(t => t.CORR_BAIRRO_COMPL).HasColumnName("CORR_BAIRRO_COMPL");
            Property(t => t.CORR_UF).HasColumnName("CORR_UF");
            Property(t => t.CORR_MUNICIPIO).HasColumnName("CORR_MUNICIPIO");
            Property(t => t.CORR_CEP).HasColumnName("CORR_CEP");
            Property(t => t.SUFRAMA_ERRO_MENSAGEM).HasColumnName("SUFRAMA_ERRO_MENSAGEM");
            Property(t => t.SUFRAMA_SIT_CADASTRAL).HasColumnName("SUFRAMA_SIT_CADASTRAL");
            Property(t => t.SUFRAMA_INSCRICAO).HasColumnName("SUFRAMA_INSCRICAO");
            Property(t => t.SUFRAMA_TEL).HasColumnName("SUFRAMA_TEL");
            Property(t => t.SUFRAMA_SIT_CADASTRAL_VALIDADE).HasColumnName("SUFRAMA_SIT_CADASTRAL_VALIDADE");
            Property(t => t.SUFRAMA_INCENTIVOS).HasColumnName("SUFRAMA_INCENTIVOS");
            Property(t => t.SUFRAMA_EMAIL).HasColumnName("SUFRAMA_EMAIL");
            Property(t => t.RF_CERTIFICADO_HTML).HasColumnName("RF_CERTIFICADO_HTML");
            Property(t => t.SINT_CERTIFICADO_HTML).HasColumnName("SINT_CERTIFICADO_HTML");
            Property(t => t.SN_CONSULTA_DTHR).HasColumnName("SN_CONSULTA_DTHR");
            Property(t => t.SUFRAMA_CONSULTA_DTHR).HasColumnName("SUFRAMA_CONSULTA_DTHR");
            Property(t => t.CORR_CONSULTA_DTHR).HasColumnName("CORR_CONSULTA_DTHR");
            Property(t => t.RF_CONTADOR_TENTATIVA).HasColumnName("RF_CONTADOR_TENTATIVA");
            Property(t => t.SINT_CONTADOR_TENTATIVA).HasColumnName("SINT_CONTADOR_TENTATIVA");
            Property(t => t.SN_CONTADOR_TENTATIVA).HasColumnName("SN_CONTADOR_TENTATIVA");
            Property(t => t.SUFRAMA_CONTADOR_TENTATIVA).HasColumnName("SUFRAMA_CONTADOR_TENTATIVA");
            Property(t => t.CORR_CONTADOR_TENTATIVA).HasColumnName("CORR_CONTADOR_TENTATIVA");
            Property(t => t.SUFRAMA_CERTIFICADO_HTML).HasColumnName("SUFRAMA_CERTIFICADO_HTML");
            Property(t => t.CORR_CERTIFICADO_HTML).HasColumnName("CORR_CERTIFICADO_HTML");
            Property(t => t.RF_CODE_ROBO).HasColumnName("RF_CODE_ROBO");
            Property(t => t.SINT_CODE_ROBO).HasColumnName("SINT_CODE_ROBO");
            Property(t => t.SN_CODE_ROBO).HasColumnName("SN_CODE_ROBO");
            Property(t => t.LONGITUDE).HasColumnName("LONGITUDE");
            Property(t => t.LATITUDE).HasColumnName("LATITUDE");

            // Relationships
            HasOptional(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.ROBO)
                .HasForeignKey(d => d.SOLICITACAO_ID);
        }
    }
}