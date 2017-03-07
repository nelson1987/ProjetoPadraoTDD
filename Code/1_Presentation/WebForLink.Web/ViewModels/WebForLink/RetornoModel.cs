namespace WebForLink.Web.ViewModels
{
    public class RetornoModel
    {
        public int TipoAcao { get; set; }
        public int CodigoSolicitacao { get; set; }
        public int Empresa { get; set; }
        public string CodigoSap { get; set; }
        public int CodigoRetorno { get; set; }
        public string DescricaoErro { get; set; }
        public string TextoRetorno { get; set; }
    }
}