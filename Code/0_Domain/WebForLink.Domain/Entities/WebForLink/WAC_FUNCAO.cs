using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FUNCAO
    {
        public FUNCAO()
        {
            FUNCOES = new List<FUNCAO>();
            WAC_PERFIL = new List<Perfil>();
            WFD_CONTRATANTE = new List<Contratante>();
        }

        public int ID { get; set; }
        public string CODIGO { get; set; }
        public int APLICACAO_ID { get; set; }
        public string FUNCAO_NM { get; set; }
        public string FUNCAO_TELA { get; set; }
        public string FUNCAO_DSC { get; set; }
        public int? FUNCAO_PAI { get; set; }
        public virtual APLICACAO WAC_APLICACAO { get; set; }
        public virtual ICollection<FUNCAO> FUNCOES { get; set; }
        public virtual FUNCAO FUNCAOPRINCIPAL { get; set; }
        public virtual ICollection<Perfil> WAC_PERFIL { get; set; }
        public virtual ICollection<Contratante> WFD_CONTRATANTE { get; set; }
    }
}