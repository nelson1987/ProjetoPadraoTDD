using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Service.Common;

namespace WebForLink.Domain.Interfaces.Service
{
    public interface ISolicitacaoService : IService<Solicitacao>
    {
        Solicitacao GetAllReferencesFichaCadastral(int id);
        Solicitacao BuscarArquivo(int id);
        Solicitacao BuscarFichaCompleta(int id);
    }

    public interface IFichaCadastralService : IService<FichaCadastral>
    {
        void Incluir(FichaCadastral ficha, int idSolicitacao);
        FichaCadastral Incluir(FichaCadastral ficha);
        void IncluirFichaCadastral(FichaCadastral ficha);
        void IncluirArquivo(int idSolicitacao, int idDocumentoSolicitado, string nomeOriginal, int size, string url);
    }
}