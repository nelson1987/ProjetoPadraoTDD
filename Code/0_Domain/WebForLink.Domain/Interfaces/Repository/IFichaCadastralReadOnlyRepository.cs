using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Domain.Interfaces.Repository
{
    public interface IFichaCadastralReadOnlyRepository : IReadOnlyRepository<FichaCadastral>
    {
        void IncluirFichaCadastral(FichaCadastral ficha);
        void Incluir(FichaCadastral ficha, int idSolicitacao);
    }
}