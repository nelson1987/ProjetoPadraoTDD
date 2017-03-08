namespace WebForLink.Domain.Entities.Tipos
{
    public class RoboReceitaFederal : Robo
    {
        public RoboReceitaFederal(string razaoSocial)
            : base(razaoSocial)
        {
        }
        public string RF_NOME_FANTASIA { get; private set; }
        public string RF_NOME { get; private set; }
        public string RF_LOGRADOURO { get; private set; }
        public string RF_NUMERO { get; private set; }
        public string RF_COMPLEMENTO { get; private set; }
        public string RF_BAIRRO { get; private set; }
        public string RF_MUNICIPIO { get; private set; }
        public string RF_UF { get; private set; }
        public string RF_CEP { get; private set; }
        public string RF_SIT_CADASTRAL_CNPJ { get; private set; }
        public string RF_SIT_CADSTRAL_CNPJ_DT { get; private set; }
        public string RF_SIT_ESPECIAL_CNPJ { get; private set; }
        public string RF_SIT_ESPECIAL_CNPJ_DT { get; private set; }
        public string RF_MOTIVO_CNPJ_SITU_CADASTRAL { get; private set; }
        public string RF_CNPJ_DT_ABERTURA { get; private set; }
        public string RF_CNAE_COD_PRINCIPAL { get; private set; }
        public string RF_CNAE_DSC_PRINCIPAL { get; private set; }
        public string RF_CNAE_COD_OUTROS { get; private set; }
        public string RF_CNAE_DSC_OUTROS { get; private set; }
        public string RF_MATRIZ_FILIAL { get; private set; }
        public string RF_COD_NATUREZA_JURIDICA { get; private set; }
        public string RF_DSC_NATUREZA_JURIDICA { get; private set; }
        public string RF_CONSULTA_DTHR { get; private set; }
        public string RF_CERTIFICADO_HTML { get; private set; }
        public string RF_CONTADOR_TENTATIVA { get; private set; }
        public string RF_CODE_ROBO { get; private set; }
    }
}