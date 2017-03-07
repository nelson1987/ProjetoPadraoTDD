using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Domain.Interfaces.Repository.ReadOnly
{
    public interface IArquivoReadOnlyRepository : IReadOnlyRepository<Arquivo>
    {
    }

    public interface ICarrinhoReadOnlyRepository : IReadOnlyRepository<Carrinho>
    {
    }

    public interface IDocumentoAnexadoReadOnlyRepository : IReadOnlyRepository<DocumentoAnexado>
    {
    }

    public interface IDocumentosSolicitacaoReadOnlyRepository : IReadOnlyRepository<DocumentoSolicitacao>
    {
    }

    public interface IListaDocumentoReadOnlyRepository : IReadOnlyRepository<ListaDocumento>
    {
    }

    public interface IListasSolicitanteReadOnlyRepository : IReadOnlyRepository<ListasSolicitante>
    {
    }

    public interface IResponsavelReadOnlyRepository : IReadOnlyRepository<Responsavel>
    {
    }

    public interface ISolicitadoReadOnlyRepository : IReadOnlyRepository<Solicitado>
    {
    }

    public interface ISolicitanteReadOnlyRepository : IReadOnlyRepository<Solicitante>
    {
    }

    public interface ISolicitacaoReadOnlyRepository : IReadOnlyRepository<Solicitacao>
    {
        Solicitacao GetAllReferencesFichaCadastral(int id);
    }

    public interface IEnderecoReadOnlyRepository : IReadOnlyRepository<Endereco>
    {
        Endereco Insert(Endereco entity);
        Endereco Update(Endereco entity);
    }

    public interface IContatoReadOnlyRepository : IReadOnlyRepository<Contato>
    {
        Contato Insert(Contato entity);
        Contato Update(Contato entity);
    }

    public interface IBancoReadOnlyRepository : IReadOnlyRepository<Banco>
    {
        Banco Insert(Banco entity);
        Banco Update(Banco entity);
    }
}