using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IFornecedorListaDocumentosWebForLinkService : IService<ListaDeDocumentosDeFornecedor>
    {
        ListaDeDocumentosDeFornecedor BuscarPorID(int id);
        ListaDeDocumentosDeFornecedor BuscarPorID(int idContratante, int id);
        List<ListaDeDocumentosDeFornecedor> BuscarPorCategoriaId(int categoriaId);
        ListaDeDocumentosDeFornecedor ListarPorContratatanteId(int id, int contratanteId);
    }

    public class FornecedorListaDocumentosWebForLinkService : Service<ListaDeDocumentosDeFornecedor>,
        IFornecedorListaDocumentosWebForLinkService
    {
        private readonly IFornecedorListaDocumentosWebForLinkRepository _listaDocumentoRepository;

        public FornecedorListaDocumentosWebForLinkService(
            IFornecedorListaDocumentosWebForLinkRepository listaDocumentoRepository) : base(listaDocumentoRepository)
        {
            try
            {
                _listaDocumentoRepository = listaDocumentoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public ListaDeDocumentosDeFornecedor BuscarPorID(int id)
        {
            try
            {
                return _listaDocumentoRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public ListaDeDocumentosDeFornecedor BuscarPorID(int idContratante, int id)
        {
            try
            {
                return
                    _listaDocumentoRepository.Find(d => d.CONTRATANTE_ID == idContratante && d.ID == id)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a Lista de Documentos por ID", ex);
            }
        }

        public List<ListaDeDocumentosDeFornecedor> BuscarPorCategoriaId(int categoriaId)
        {
            try
            {
                return
                    _listaDocumentoRepository.Find(c => c.WFD_PJPF_CATEGORIA.Any(cc => cc.ID == categoriaId)).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a lista de Documentos por Categoria", ex);
            }
        }

        public ListaDeDocumentosDeFornecedor ListarPorContratatanteId(int id, int contratanteId)
        {
            try
            {
                return
                    _listaDocumentoRepository.Find(c => c.CONTRATANTE_ID == contratanteId && c.ID == id)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}