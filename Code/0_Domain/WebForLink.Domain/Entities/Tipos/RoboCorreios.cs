namespace WebForLink.Domain.Entities.Tipos
{
    public class RoboCorreios : Robo
    {
        public RoboCorreios(string razaoSocial)
            : base(razaoSocial)
        {
        }
        public string CORREIOS_TP_LOGRADOURO { get; private set; }
        public string CORR_LOGRADOURO { get; private set; }
        public string CORR_COMPLEMENTO { get; private set; }
        public string CORR_BAIRRO { get; private set; }
        public string CORR_BAIRRO_COMPL { get; private set; }
        public string CORR_UF { get; private set; }
        public string CORR_MUNICIPIO { get; private set; }
        public string CORR_CEP { get; private set; }
        public string CORR_CONSULTA_DTHR { get; private set; }
        public string CORR_CONTADOR_TENTATIVA { get; private set; }
        public string CORR_CERTIFICADO_HTML { get; private set; }
    }
}
