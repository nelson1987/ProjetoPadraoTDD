using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class QuestionarioVM
    {
        public QuestionarioVM()
        {
            AbaList = new List<AbaVM>();
        }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int ContratanteId { get; set; }
        public string Descricao { get; set; }
        public bool? ExibeDadosBancarios { get; set; }
        public bool? ExibeDadosContato { get; set; }
        public bool? ExibeDadosGerais { get; set; }
        public bool? ExibeInformacaoComplementar { get; set; }
        public List<AbaVM> AbaList { get; set; }
        public string EstiloClassCss { get; set; }
        public string EstiloIdCss
        {
            get { return "quest_" + Id; }
        }
    }
}