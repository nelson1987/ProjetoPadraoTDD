using System.Collections.Generic;
using WebForLink.Domain.Enums;

namespace WebForLink.Domain.Infrastructure
{
    public class PerguntaAbaDinamico
    {
        public int PerguntaID { get; set; }
        public string Titulo { get; set; }
        public List<RespostasPossiveis> Respostas { get; set; }
        public int PerguntaId { get; set; }
        public bool Visivel { get; set; }
        public bool Bloqueado { get; set; }
        public bool Obrigatorio { get; set; }
        public string Resposta { get; set; }
        public EnumTipoDadoDominio TpDadoDominio { get; set; }
        public List<RespostasPossiveis> ListaSelecionavel { get; set; }
        public int? Tamanho { get; set; }
        public bool EPai { get; set; }
        public int AbaId { get; set; }
        public int? PerguntaPai { get; set; }
        public bool? Dominio { get; set; }
        public string ExibeNome { get; set; }
        public int SolicitacaoId { get; set; }
        public int RespostaId { get; set; }
        public int RespostaFornecedorId { get; set; }
        public string RespostaFornecedor { get; set; }
        public bool PulaLinha { get; set; }
    }
}