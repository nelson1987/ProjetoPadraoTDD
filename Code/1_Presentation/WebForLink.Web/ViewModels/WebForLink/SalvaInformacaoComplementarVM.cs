using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class SalvaInformacaComplementarVM
    {
        public int PerguntaId { get; set; }
        public int SolicitacaoId { get; set; }
        public int RespostaId { get; set; }
        public string Resposta { get; set; }
        public List<PerguntaVM> PerguntaList { get; set; }
    }
}