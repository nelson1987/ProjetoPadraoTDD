namespace WebForLink.Domain.Infrastructure.FiltrosDTO
{
    public class PesquisaContratanteFiltrosDTO
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Estilo { get; set; }
        public string ContratanteCodErp { get; set; }
        public int? TipoCadastroId { get; set; }
        public int ContratanteUsuario { get; set; }
    }
}