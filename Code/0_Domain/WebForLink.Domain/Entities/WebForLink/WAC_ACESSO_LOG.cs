using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class WAC_ACESSO_LOG
    {
        public int USUARIO_ID { get; set; }
        public DateTime DATA { get; set; }
        public string IP { get; set; }
        public string NAVEGADOR { get; set; }
        public virtual Usuario WFD_USUARIO { get; set; }
    }
}