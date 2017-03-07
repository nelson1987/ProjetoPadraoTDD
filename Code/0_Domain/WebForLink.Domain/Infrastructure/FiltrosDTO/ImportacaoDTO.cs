namespace WebForLink.Domain.Infrastructure.FiltrosDTO
{
    public class ImportacaoDTO
    {
        public string Cnpj { get; set; }
        public int SolicitacaoId { get; set; }
        public int ContratanteId { get; set; }
        public string Email { get; set; }
        public int TipoFornecedor { get; set; }
    }
}