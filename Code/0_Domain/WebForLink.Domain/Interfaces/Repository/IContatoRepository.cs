using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Domain.Interfaces.Repository
{
    public interface IContatoRepository : IRepository<Contato>
    {
    }

    public interface IEnderecoRepository : IRepository<Endereco>
    {
    }

    public interface IBancoRepository : IRepository<Banco>
    {
    }

    public interface IArquivoRepository : IRepository<Arquivo>
    {
    }

    public interface ICarrinhoRepository : IRepository<Carrinho>
    {
    }

    public interface IDocumentosSolicitacaoRepository : IRepository<DocumentoSolicitacao>
    {
    }

    public interface IListasDocumentoRepository : IRepository<ListaDocumento>
    {
    }

    public interface IListasSolicitanteRepository : IRepository<ListasSolicitante>
    {
    }

    public interface IResponsavelRepository : IRepository<Responsavel>
    {
    }

    public interface ISolicitadoRepository : IRepository<Solicitado>
    {
    }

    public interface ISolicitanteRepository : IRepository<Solicitante>
    {
    }

    public interface IDocumentoAnexadoRepository : IRepository<DocumentoAnexado>
    {
    }
}