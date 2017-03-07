using System.Collections.Generic;

namespace WebForLink.Domain.Infrastructure
{
    public class AbaQuestionarioDinamico
    {
        public int AbaID { get; set; }
        public List<PerguntaAbaDinamico> Perguntas { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int QuestionarioId { get; set; }
    }
}