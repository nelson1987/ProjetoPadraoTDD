using System.Collections.Generic;

namespace WebForLink.Web.Areas.Administracao.Models
{
    public class TipoCadastroAdministracaoModel : ViewModelPadrao
    {
        public TipoCadastroAdministracaoModel()
        {
            ContratanteList = new HashSet<ContratanteAdministracaoModel>();
        }

        public int Id { get; set; }

        public string Nome { get; set; }

        public ICollection<ContratanteAdministracaoModel> ContratanteList { get; set; }
    }
}