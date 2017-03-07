using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class OutrosDadosVM
    {
        public int ID { get; set; }
        public string DescricaoAlteracao { get; set; }

        public virtual TIPO_VISAO Visao { get; set; }
        public virtual TIPO_GRUPO Grupo { get; set; }
        public virtual TIPO_DESCRICAO Descricao { get; set; }
    }
}