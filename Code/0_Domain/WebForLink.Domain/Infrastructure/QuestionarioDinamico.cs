using System.Collections.Generic;

namespace WebForLink.Domain.Infrastructure
{
    public class QuestionarioDinamico
    {
        public int QuestionarioID { get; set; }
        public List<AbaQuestionarioDinamico> Abas { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int? ContratanteId { get; set; }
        public bool? ExibeDadosBancarios { get; set; }
        public bool? ExibeDadosContato { get; set; }
        public bool? ExibeDadosGerais { get; set; }
        public bool ExibeInformacaoComplementar { get; set; }
    }
}