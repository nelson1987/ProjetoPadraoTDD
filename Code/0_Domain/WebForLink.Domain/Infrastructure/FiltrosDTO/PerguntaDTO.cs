using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;

namespace WebForLink.Domain.Infrastructure
{
    public class PerguntaDTO
    {
        public PerguntaDTO()
        {
        }

        public PerguntaDTO(int perguntaId, bool visivel, bool bloqueado, bool obrigatorio)
        {
            PerguntaId = perguntaId;
            Visivel = visivel;
            Bloqueado = bloqueado;
            Obrigatorio = obrigatorio;
        }

        public int Id { get; set; }
        public int AbaId { get; set; }
        public bool Dominio { get; set; }
        public string Titulo { get; set; }
        public bool EPai { get; set; }
        public int? PerguntaPai { get; set; }
        public bool Leitura { get; set; }
        public bool Obrigatorio { get; set; }
        public string Resposta { get; set; }
        public int RespostaId { get; set; }
        public List<QUESTIONARIO_RESPOSTA> DominioList { get; set; }
        public bool Escrita { get; set; }
        public int PerguntaId { get; set; }
        public bool Visivel { get; set; }
        public bool Bloqueado { get; set; }
        public EnumTipoDadoDominio TpDadoDominio { get; set; }
        public object ListaSelecionavel { get; set; }
    }
}