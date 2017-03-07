using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class DESTINATARIO
    {
        public DESTINATARIO()
        {
            MEU_COMPARTILHAMENTOS = new List<Compartilhamentos>();
        }

        public int ID { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public string OBS { get; set; }
        public DateTime? VALIDADE { get; set; }
        public string EMPRESA { get; set; }
        public bool ATIVO { get; set; }
        public bool EMAIL_AVULSO { get; set; }
        public string SOBRENOME { get; set; }
        public string TELEFONE_FIXO { get; set; }
        public string CELULAR { get; set; }
        public string TELEFONE_TRABALHO { get; set; }
        public string FAX { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual ICollection<Compartilhamentos> MEU_COMPARTILHAMENTOS { get; set; }
    }
}