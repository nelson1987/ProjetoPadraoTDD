namespace WebForLink.Domain.Entities.Tipos
{
    public class RoboSuframa : Robo
    {
        public RoboSuframa(string razaoSocial)
            : base(razaoSocial)
        {
        }

        public string SUFRAMA_ERRO_MENSAGEM { get; private set; }
        public string SUFRAMA_SIT_CADASTRAL { get; private set; }
        public string SUFRAMA_INSCRICAO { get; private set; }
        public string SUFRAMA_TEL { get; private set; }
        public string SUFRAMA_SIT_CADASTRAL_VALIDADE { get; private set; }
        public string SUFRAMA_INCENTIVOS { get; private set; }
        public string SUFRAMA_EMAIL { get; private set; }
        public string SUFRAMA_CONSULTA_DTHR { get; private set; }
        public string SUFRAMA_CONTADOR_TENTATIVA { get; private set; }
        public string SUFRAMA_CERTIFICADO_HTML { get; private set; }
    }
}