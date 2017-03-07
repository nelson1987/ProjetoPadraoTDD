using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class SOLICITACAO_TRAMITE
    {
        public int ID { get; set; }
        public int SOLICITACAO_ID { get; set; }
        public int PAPEL_ID { get; set; }
        public int SOLICITACAO_STATUS_ID { get; set; }
        public DateTime? TRAMITE_DT_INI { get; set; }
        public DateTime? TRMITE_DT_FIM { get; set; }
        public int? USUARIO_ID { get; set; }
        public int? FLUXO_SEQ_EXC_ID { get; set; }
        public int? GRUPO_DESTINO { get; set; }
        public string OBS { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual SOLICITACAO_STATUS WFD_SOLICITACAO_STATUS { get; set; }
        public virtual Usuario WFD_USUARIO { get; set; }
        public virtual Papel Papel { get; set; }

        public bool Aguardando(SOLICITACAO_TRAMITE tramite)
        {
            return tramite.SOLICITACAO_STATUS_ID == 1;
        }

        public bool Aprovado(SOLICITACAO_TRAMITE tramite)
        {
            return tramite.SOLICITACAO_STATUS_ID == 2;
        }

        public bool Reprovado(SOLICITACAO_TRAMITE tramite)
        {
            return tramite.SOLICITACAO_STATUS_ID == 3;
        }

        public bool Concluído(SOLICITACAO_TRAMITE tramite)
        {
            return tramite.SOLICITACAO_STATUS_ID == 4;
        }

        public bool EmAprovacao(SOLICITACAO_TRAMITE tramite)
        {
            return tramite.SOLICITACAO_STATUS_ID == 5;
        }

        public bool Cancelada(SOLICITACAO_TRAMITE tramite)
        {
            return tramite.SOLICITACAO_STATUS_ID == 6;
        }
    }
}