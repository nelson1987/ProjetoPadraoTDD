using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class CONTRATANTE_CONFIGURACAO
    {
        public int CONTRATANTE_ID { get; set; }
        public bool SOLICITA_DOCS { get; set; }
        public bool SOLICITA_FICHA_CAD { get; set; }
        public byte[] LOGOTIPO { get; set; }
        public string TERMO_ACEITE { get; set; }
        public int? ROBO_CICLO_ATU { get; set; }
        public DateTime? ROBO_DT_PROX_EXEC { get; set; }
        public bool BLOQUEIO_MANUAL { get; set; }
        public int? BLOQUIEO_MANUAL_PRAZO { get; set; }
        public int? TOTAL_TENTATIVA_ROBO { get; set; }
        public int? NIVEIS_CATEGORIA { get; set; }
        public int? QTD_ROBO_SIMULTANEA { get; set; }
        public int PRAZO_ENTREGA_FICHA { get; set; }
        public string FORNECEDOR_CARGA { get; set; }
        public string FORNECEDOR_RETORNO { get; set; }
        public string CLIENTE_CARGA { get; set; }
        public string CLIENTE_RETORNO { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
    }
}