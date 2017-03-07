using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class AbaVM
    {
        public AbaVM()
        {
            PerguntaList = new List<PerguntaVM>();
        }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int QuestionarioId { get; set; }
        public string Descricao { get; set; }
        public List<PerguntaVM> PerguntaList { get; set; }
    }
}