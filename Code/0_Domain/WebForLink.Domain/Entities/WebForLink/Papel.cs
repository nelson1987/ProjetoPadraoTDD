using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class Papel
    {
        public Papel()
        {
            QIC_QUEST_ABA_PERG_PAPEL = new List<QUESTIONARIO_PAPEL>();
            WFD_SOLICITACAO_TRAMITE = new List<SOLICITACAO_TRAMITE>();
            WFL_FLUXO_SEQ_PRE_REQUIS = new List<FLUXO_SEQUENCIA_PRE_REQUIS>();
            WFL_FLUXO_SEQUENCIA = new List<FLUXO_SEQUENCIA>();
            WFL_FLUXO_SEQUENCIA1 = new List<FLUXO_SEQUENCIA>();
            WFD_USUARIO = new List<Usuario>();
        }

        public int ID { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public string PAPEL_SGL { get; set; }
        public string PAPEL_NM { get; set; }
        public int? PAPEL_TP_ID { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual TipoDePapel TipoDePapel { get; set; }
        public virtual ICollection<QUESTIONARIO_PAPEL> QIC_QUEST_ABA_PERG_PAPEL { get; set; }
        public virtual ICollection<SOLICITACAO_TRAMITE> WFD_SOLICITACAO_TRAMITE { get; set; }
        public virtual ICollection<FLUXO_SEQUENCIA_PRE_REQUIS> WFL_FLUXO_SEQ_PRE_REQUIS { get; set; }
        public virtual ICollection<FLUXO_SEQUENCIA> WFL_FLUXO_SEQUENCIA { get; set; }
        public virtual ICollection<FLUXO_SEQUENCIA> WFL_FLUXO_SEQUENCIA1 { get; set; }
        public virtual ICollection<Usuario> WFD_USUARIO { get; set; }
    }
}