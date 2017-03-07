using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDORBASE_IMPORTACAO
    {
        public FORNECEDORBASE_IMPORTACAO()
        {
            WFD_PJPF_BASE = new List<FORNECEDORBASE>();
        }

        public int ID { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public string NOME_ARQUIVO { get; set; }
        public DateTime DT_UPLOAD { get; set; }
        public int USUARIO_ID { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual Usuario WFD_USUARIO { get; set; }
        public virtual ICollection<FORNECEDORBASE> WFD_PJPF_BASE { get; set; }
    }
}