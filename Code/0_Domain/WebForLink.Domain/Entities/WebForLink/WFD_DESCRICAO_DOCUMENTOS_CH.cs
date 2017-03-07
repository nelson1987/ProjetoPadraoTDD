using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class WFD_DESCRICAO_DOCUMENTOS_CH
    {
        public WFD_DESCRICAO_DOCUMENTOS_CH()
        {
            WFD_DESCRICAO_DOCUMENTOS = new List<DescricaoDeDocumentos>();
        }

        public int ID { get; set; }
        public int TIPO_DOCUMENTOS_ID { get; set; }
        public string DESCRICAO { get; set; }
        public virtual TIPO_DOCUMENTOS_CH WFD_TIPO_DOCUMENTOS_CH { get; set; }
        public virtual ICollection<DescricaoDeDocumentos> WFD_DESCRICAO_DOCUMENTOS { get; set; }
    }
}