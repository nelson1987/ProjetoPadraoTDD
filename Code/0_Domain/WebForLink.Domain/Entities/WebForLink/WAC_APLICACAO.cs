using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class APLICACAO
    {
        public APLICACAO()
        {
            WAC_FUNCAO = new List<FUNCAO>();
        }

        public int ID { get; set; }
        public string APLICACAO_NM { get; set; }
        public string APLICACAO_DSC { get; set; }
        public virtual ICollection<FUNCAO> WAC_FUNCAO { get; set; }
    }
}