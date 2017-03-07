using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Service.Common;

namespace WebForLink.Domain.Interfaces.Service
{
    public interface IEnderecoService : IService<Endereco>
    {
        Endereco UpdateReadOnly(Endereco entity);
        Endereco InsertReadOnly(Endereco entity);
        void ExcluirEndereco(int id);
    }

    public interface IContatoService : IService<Contato>
    {
        Contato UpdateReadOnly(Contato entity);
        Contato InsertReadOnly(Contato entity);
    }

    public interface IBancoService : IService<Banco>
    {
        Banco UpdateReadOnly(Banco entity);
        Banco InsertReadOnly(Banco entity);
    }

    public interface IArquivoService : IService<Arquivo>
    {
    }

    public interface ICarrinhoService : IService<Carrinho>
    {
    }

    public interface IDocumentoAnexadoService : IService<DocumentoAnexado>
    {
    }

    public interface IDocumentosSolicitacaoService : IService<DocumentoSolicitacao>
    {
    }

    public interface IListasDocumentoService : IService<ListaDocumento>
    {
    }

    public interface IListasSolicitanteService : IService<ListasSolicitante>
    {
    }

    public interface IResponsavelService : IService<Responsavel>
    {
    }

    public interface ISolicitadoService : IService<Solicitado>
    {
    }

    public interface ISolicitanteService : IService<Solicitante>
    {
    }
}