using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Domain.Interfaces.Repository
{
    public interface ISolicitacaoRepository : IRepository<Solicitacao>
    {
        Solicitacao BuscarArquivo(int id);
        Solicitacao BuscarFichaCompleta(int id);
    }

    public interface IFichaCadastralRepository : IRepository<FichaCadastral>
    {
        void IncluirArquivo(int idSolicitacao, int idDocumentoSolicitado, string nomeOriginal, int size, string url);
    }
}