using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Interfaces
{
    public interface ISolicitacaoAppService : IAppService<Solicitacao>
    {
        ValidationResult CriarSolicitacao(Solicitacao solicitacao);
        ValidationResult CriarSolicitacao(Solicitacao solicitacao, string mensagemEmail);
        ValidationResult CriarSolicitacao(int idSolicitante, int idSolicitado, string mensagemEmail);
        int DescriptografarLinkConvite(string chaveUrl);
        ValidationResult SalvarFichaCadastral(FichaCadastral fichaCadastral);
        FichaCadastral GetFichaCadastral(int id, bool @readonly = false);
        FichaCadastral GetFichaCadastralPorSolicitacao(int idSolicitacao, bool @readonly = false);
        Solicitacao GetAllReferencesFichaCadastral(int id);
        void Visualizar(Solicitacao idSolicitacao);
        void Finalizar(int idSolicitacao);
        Solicitacao GetArquivos(int id);
        Solicitacao BuscarFichaCadastral(string chave);
    }
}