namespace WebForLink.Domain.Entities.WebForLink
{
    public class CONFIGURACAO
    {
        public int ID { get; set; }
        public string ROBO_IMPORTACAO { get; set; }
        public string ROBO_GOVERNANCA { get; set; }
        public int QTD_ACESSO_ROBO_SIMULTANEO { get; set; }
        public string CHAVE_CRIPTO { get; set; }
        public string CHAVE_WEBSERVICE { get; set; }
        public string CAMINHO_ARQUIVOS { get; set; }
    }
}