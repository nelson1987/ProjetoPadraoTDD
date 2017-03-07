using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class USUARIO_SENHAS
    {
        public int ID { get; set; }
        public int USUARIO_ID { get; set; }
        public string SENHA { get; set; }
        public DateTime SENHA_DT { get; set; }
        public virtual Usuario WFD_USUARIO { get; set; }
    }
}