namespace WebForLink.Web.ViewModels
{
    public class DadosBloqueioVM
    {
        public int ID { get; set; }
        public int SolicitacaoID { get; set; }
        public int? ContratanteFornecedorID { get; set; }
        public string Lancamento { get; set; }
        public bool Compra { get; set; }
        public int? Motivo { get; set; }
        public string MotivoQualidade { get; set; }
        public string MotivoSolicitacao { get; set; }
        public int ContratanteID { get; set; }
        public int? FornecedorID { get; set; }       
    }
}