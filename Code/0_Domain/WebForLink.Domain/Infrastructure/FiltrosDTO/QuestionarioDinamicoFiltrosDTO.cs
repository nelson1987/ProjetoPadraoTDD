namespace WebForLink.Domain.Infrastructure.FiltrosDTO
{
    public class QuestionarioDinamicoFiltrosDTO
    {
        public string UF { get; set; }
        public int ContratanteId { get; set; }
        public int PapelId { get; set; }
        public int? CategoriaId { get; set; }
        public bool Alteracao { get; set; }
        public int SolicitacaoId { get; set; }
        public int FornecedorId { get; set; }
        public int ContratantePJPFId { get; set; }
        public bool HabilitaBotao { get; set; }
    }
}