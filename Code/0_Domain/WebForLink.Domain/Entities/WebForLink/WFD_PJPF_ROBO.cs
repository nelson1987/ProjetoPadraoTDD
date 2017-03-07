using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class ROBO
    {
        public ROBO()
        {
            WFD_PJPF = new List<Fornecedor>();
            WFD_PJPF_BASE = new List<FORNECEDORBASE>();
            WFD_SOL_CAD_PJPF = new List<SolicitacaoCadastroFornecedor>();
        }

        public int ID { get; set; }
        public DateTime ROBO_DT_EXEC { get; set; }
        public int? SOLICITACAO_ID { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        #region Receita Federal
        public string RECEITA_FEDERAL_RAZAO_SOCIAL { get; set; }
        public string RF_NOME_FANTASIA { get; set; }
        public string RF_NOME { get; set; }
        public string RF_LOGRADOURO { get; set; }
        public string RF_NUMERO { get; set; }
        public string RF_COMPLEMENTO { get; set; }
        public string RF_BAIRRO { get; set; }
        public string RF_MUNICIPIO { get; set; }
        public string RF_UF { get; set; }
        public string RF_CEP { get; set; }
        public string RF_SIT_CADASTRAL_CNPJ { get; set; }
        public DateTime? RF_SIT_CADSTRAL_CNPJ_DT { get; set; }
        public string RF_SIT_ESPECIAL_CNPJ { get; set; }
        public DateTime? RF_SIT_ESPECIAL_CNPJ_DT { get; set; }
        public string RF_MOTIVO_CNPJ_SITU_CADASTRAL { get; set; }
        public DateTime? RF_CNPJ_DT_ABERTURA { get; set; }
        public string RF_CNAE_COD_PRINCIPAL { get; set; }
        public string RF_CNAE_DSC_PRINCIPAL { get; set; }
        public string RF_CNAE_COD_OUTROS { get; set; }
        public string RF_CNAE_DSC_OUTROS { get; set; }
        public string RF_MATRIZ_FILIAL { get; set; }
        public string RF_COD_NATUREZA_JURIDICA { get; set; }
        public string RF_DSC_NATUREZA_JURIDICA { get; set; }
        public DateTime? RF_CONSULTA_DTHR { get; set; }
        public string RF_CERTIFICADO_HTML { get; set; }
        public int? RF_CONTADOR_TENTATIVA { get; set; }
        public int? RF_CODE_ROBO { get; set; } 
        #endregion
        #region Sintegra
        public string SINTEGRA_ERRO_ORIGINAL { get; set; }
        public string SINT_IE_QTD { get; set; }
        public string SINT_IE_MULTIPLA { get; set; }
        public string SINT_IE_MULTIPLA_CODIGOS { get; set; }
        public string SINT_IE_MULTIPLA_SITUACAO { get; set; }
        public string SINT_IE_COD { get; set; }
        public DateTime? SINT_CONSULTA_DTHR { get; set; }
        public string SINT_IE_SITU_CADASTRAL { get; set; }
        public string SINT_IE_SITU_CADSTRAL_DT { get; set; }
        public DateTime? SINT_INCLUSAO_DT { get; set; }
        public DateTime? SINT_BAIXA_DT { get; set; }
        public string SINT_BAIXA_MOTIVO { get; set; }
        public string SINT_EMAIL { get; set; }
        public string SINT_REGIME_APURACAO { get; set; }
        public string SINT_ENQUADRAMENTO_FISCAL { get; set; }
        public string SINT_TEL { get; set; }
        public string SINT_CAD_PROD_RURAL { get; set; }
        public string SINT_COMPLEMENTO { get; set; }
        public string SINT_RAZAO_SOCIAL { get; set; }
        public string SINT_CNPJ { get; set; }
        public string SINT_BAIRRO { get; set; }
        public string SINT_LOGRADOURO { get; set; }
        public string SINT_NUMERO { get; set; }
        public string SINT_CEP { get; set; }
        public string SINT_MUNICIPIO { get; set; }
        public string SINT_UF { get; set; }
        public string SINT_ATIVIDADE_PRINCIPAL { get; set; }
        public string SINT_CERTIFICADO_HTML { get; set; }
        public int? SINT_CONTADOR_TENTATIVA { get; set; }
        public int? SINT_CODE_ROBO { get; set; }
        #endregion        
        #region Simples Nacional
        public string SIMPLES_NACIONAL_SITUACAO { get; set; }
        public string SN_SITUACAO_SIMEI { get; set; }
        public string SN_PERIODOS_ANTERIORES { get; set; }
        public string SN_SIMEI_PERIODOS_ANTERIORES { get; set; }
        public string SN_AGENDAMENTOS { get; set; }
        public string SN_RAZAO_SOCIAL { get; set; }
        public DateTime? SN_CONSULTA_DTHR { get; set; }
        public int? SN_CONTADOR_TENTATIVA { get; set; }
        public int? SN_CODE_ROBO { get; set; } 
        #endregion
        #region Correios
        public string CORREIOS_TP_LOGRADOURO { get; set; }
        public string CORR_LOGRADOURO { get; set; }
        public string CORR_COMPLEMENTO { get; set; }
        public string CORR_BAIRRO { get; set; }
        public string CORR_BAIRRO_COMPL { get; set; }
        public string CORR_UF { get; set; }
        public string CORR_MUNICIPIO { get; set; }
        public string CORR_CEP { get; set; }
        public DateTime? CORR_CONSULTA_DTHR { get; set; }
        public int? CORR_CONTADOR_TENTATIVA { get; set; }
        public string CORR_CERTIFICADO_HTML { get; set; } 
        #endregion
        #region Suframa
        public string SUFRAMA_ERRO_MENSAGEM { get; set; }
        public string SUFRAMA_SIT_CADASTRAL { get; set; }
        public string SUFRAMA_INSCRICAO { get; set; }
        public string SUFRAMA_TEL { get; set; }
        public string SUFRAMA_INCENTIVOS { get; set; }
        public string SUFRAMA_EMAIL { get; set; }
        public string SUFRAMA_CERTIFICADO_HTML { get; set; } 
        public int? SUFRAMA_CONTADOR_TENTATIVA { get; set; }
        public DateTime? SUFRAMA_SIT_CADASTRAL_VALIDADE { get; set; }
        public DateTime? SUFRAMA_CONSULTA_DTHR { get; set; }
        #endregion
        public string IBGE_COD { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual ICollection<Fornecedor> WFD_PJPF { get; set; }
        public virtual ICollection<FORNECEDORBASE> WFD_PJPF_BASE { get; set; }
        public virtual ICollection<SolicitacaoCadastroFornecedor> WFD_SOL_CAD_PJPF { get; set; }
    }
}