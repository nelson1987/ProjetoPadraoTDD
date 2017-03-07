using System.Collections.Generic;

namespace WebForLink.Web.ViewModels.Adesao
{
    public class AdesaoCriacaoVM
    {
        public AdesaoCriacaoVM()
        {
            Planos = new List<AdesaoPlanoVM>();
            ExibeModalAcesso = false;
            Acesso = new AcessoVM();
        }

        public int Id { get; set; }
        public List<AdesaoPlanoVM> Planos { get; set; }
        public bool ExibeModalAcesso { get; private set; }
        public AcessoVM Acesso { get; private set; }
    }
}