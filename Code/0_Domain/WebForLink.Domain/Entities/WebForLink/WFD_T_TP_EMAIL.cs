using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_EMAIL
    {
        public TIPO_EMAIL()
        {
            WFD_CONTRATANTE_CONFIG_EMAIL = new List<CONTRATANTE_CONFIGURACAO_EMAIL>();
        }

        public int ID { get; set; }
        public string TP_EMAIL_NM { get; set; }
        public virtual ICollection<CONTRATANTE_CONFIGURACAO_EMAIL> WFD_CONTRATANTE_CONFIG_EMAIL { get; set; }
    }
}