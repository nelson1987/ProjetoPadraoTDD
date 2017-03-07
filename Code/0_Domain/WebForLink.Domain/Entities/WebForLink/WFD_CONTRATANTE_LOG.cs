using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class CONTRATANTE_LOG
    {
        public int ID { get; set; }
        public bool ATIVO { get; set; }
        public DateTime ATIVO_DT { get; set; }
        public int USUARIO_ID { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
    }
}