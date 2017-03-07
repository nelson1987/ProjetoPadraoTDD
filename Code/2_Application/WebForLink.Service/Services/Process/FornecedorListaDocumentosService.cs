using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;

namespace WebForLink.Application.Services.Process
{
    public interface IFornecedorListaDocumentosWebForLinkAppService
    {
        ListaDeDocumentosDeFornecedor BuscarPorID(int id);
        ListaDeDocumentosDeFornecedor BuscarPorID(int idContratante, int id);
        List<ListaDeDocumentosDeFornecedor> BuscarPorCategoriaId(int categoriaId);
    }

    public class FornecedorListaDocumentosWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorListaDocumentosWebForLinkAppService
    {
        private readonly IFornecedorListaDocumentosService _listaDocumentoService;

        public FornecedorListaDocumentosWebForLinkAppService(IFornecedorListaDocumentosService listaDocumentoService)
        {
            try
            {
                _listaDocumentoService = listaDocumentoService;
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
                return _listaDocumentoService.Get(id);
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
                    _listaDocumentoService.Find(d => d.CONTRATANTE_ID == idContratante && d.ID == id, true)
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
                return _listaDocumentoService.Find(c => c.WFD_PJPF_CATEGORIA.Any(cc => cc.ID == categoriaId)).ToList();
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
                    _listaDocumentoService.Find(c => c.CONTRATANTE_ID == contratanteId && c.ID == id, true)
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