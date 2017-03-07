using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class Fluxo
    {
        public Fluxo()
        {
            WFD_SOLICITACAO = new List<SOLICITACAO>();
            WFL_FLUXO_SEQUENCIA = new List<FLUXO_SEQUENCIA>();
        }

        public int ID { get; set; }
        public string FLUXO_NM { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public int APLICACAO_ID { get; set; }
        public int PAPEL_INI_FLUXO { get; set; }
        public int FLUXO_TP_ID { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual TipoDeFluxo WflTTpDeFluxo { get; set; }
        public virtual ICollection<SOLICITACAO> WFD_SOLICITACAO { get; set; }
        public virtual ICollection<FLUXO_SEQUENCIA> WFL_FLUXO_SEQUENCIA { get; set; }

        public bool CadastroFornecedor(Fluxo item)
        {
            return item.FLUXO_TP_ID == 10 || item.FLUXO_TP_ID == 20 || item.FLUXO_TP_ID == 30 || item.FLUXO_TP_ID == 40 ||
                   item.FLUXO_TP_ID == 50;
        }
    }
}