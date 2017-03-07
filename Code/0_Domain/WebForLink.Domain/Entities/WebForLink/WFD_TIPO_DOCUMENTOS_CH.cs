using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TIPO_DOCUMENTOS_CH
    {
        public TIPO_DOCUMENTOS_CH()
        {
            WFD_DESCRICAO_DOCUMENTOS_CH = new List<WFD_DESCRICAO_DOCUMENTOS_CH>();
            WFD_TIPO_DOCUMENTOS = new List<TipoDeDocumento>();
        }

        public int ID { get; set; }
        public string DESCRICAO { get; set; }
        public virtual ICollection<WFD_DESCRICAO_DOCUMENTOS_CH> WFD_DESCRICAO_DOCUMENTOS_CH { get; set; }
        public virtual ICollection<TipoDeDocumento> WFD_TIPO_DOCUMENTOS { get; set; }
    }
}