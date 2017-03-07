using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class Perfil
    {
        public Perfil()
        {
            WAC_FUNCAO = new List<FUNCAO>();
            WFD_USUARIO = new List<Usuario>();
        }

        public int ID { get; set; }
        public string PERFIL_NM { get; set; }
        public string PERFIL_DSC { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual ICollection<FUNCAO> WAC_FUNCAO { get; set; }
        public virtual ICollection<Usuario> WFD_USUARIO { get; set; }
    }
}