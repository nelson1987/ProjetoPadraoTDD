using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class TipoDeDocumento
    {
        public TipoDeDocumento()
        {
            WFD_DESCRICAO_DOCUMENTOS = new List<DescricaoDeDocumentos>();
        }

        public int ID { get; set; }
        public string DESCRICAO { get; set; }
        public int? CONTRATANTE_ID { get; set; }
        public int? TIPO_DOCUMENTOS_CH_ID { get; set; }
        public bool ATIVO { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual TIPO_DOCUMENTOS_CH WFD_TIPO_DOCUMENTOS_CH { get; set; }
        public virtual ICollection<DescricaoDeDocumentos> WFD_DESCRICAO_DOCUMENTOS { get; set; }

        public bool Valido(TipoDeDocumento TipoDeDocumento)
        {
            return TipoDeDocumento.ATIVO;
        }
    }
}