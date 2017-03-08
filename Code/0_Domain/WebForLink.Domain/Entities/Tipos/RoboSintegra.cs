namespace WebForLink.Domain.Entities.Tipos
{
    public class RoboSintegra : Robo
    {
        public RoboSintegra(string razaoSocial)
            : base(razaoSocial)
        {
        }
        public string SINTEGRA_ERRO_ORIGINAL { get; private set; }
        public string SINT_IE_QTD { get; private set; }
        public string SINT_IE_MULTIPLA { get; private set; }
        public string SINT_IE_MULTIPLA_CODIGOS { get; private set; }
        public string SINT_IE_MULTIPLA_SITUACAO { get; private set; }
        public string SINT_IE_COD { get; private set; }
        public string SINT_CONSULTA_DTHR { get; private set; }
        public string SINT_IE_SITU_CADASTRAL { get; private set; }
        public string SINT_IE_SITU_CADSTRAL_DT { get; private set; }
        public string SINT_INCLUSAO_DT { get; private set; }
        public string SINT_BAIXA_DT { get; private set; }
        public string SINT_BAIXA_MOTIVO { get; private set; }
        public string SINT_EMAIL { get; private set; }
        public string SINT_REGIME_APURACAO { get; private set; }
        public string SINT_ENQUADRAMENTO_FISCAL { get; private set; }
        public string SINT_TEL { get; private set; }
        public string SINT_CAD_PROD_RURAL { get; private set; }
        public string SINT_COMPLEMENTO { get; private set; }
        public string SINT_RAZAO_SOCIAL { get; private set; }
        public string SINT_CNPJ { get; private set; }
        public string SINT_BAIRRO { get; private set; }
        public string SINT_LOGRADOURO { get; private set; }
        public string SINT_NUMERO { get; private set; }
        public string SINT_CEP { get; private set; }
        public string SINT_MUNICIPIO { get; private set; }
        public string SINT_UF { get; private set; }
        public string SINT_ATIVIDADE_PRINCIPAL { get; private set; }
        public string SINT_CERTIFICADO_HTML { get; private set; }
        public string SINT_CONTADOR_TENTATIVA { get; private set; }
        public string SINT_CODE_ROBO { get; private set; }
    }
}