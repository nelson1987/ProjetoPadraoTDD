using System.Collections.Generic;

namespace WebForLink.Domain.Entities.Tipos
{
    public class SolicitacaoModificacaoBanco : Solicitacao
    {
        public SolicitacaoModificacaoBanco(Usuario criador, Empresa solicitado, List<Banco> bancos)
            : base(criador, solicitado)
        {
            Bancos = bancos;
        }
        public List<Banco> Bancos { get; private set; }
        public int PrazoDias { get; private set; }
    }
}
