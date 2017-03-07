using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public interface IFornecedorDocumentoWebForLinkAppService
    {
        Fornecedor BuscarPorPJPFId(int pjpfId);
        DocumentosDoFornecedor BuscarPorIdContratanteId(int contratanteId, int id);

        List<WFD_CONTRATANTE_PJPF> BuscarDocumentoOutrosContratantes(int ContratanteId, int FornecedorId,
            int DocumentoCHId);

        void AlterarDocumentos(DocumentosDoFornecedor entidade);
        List<DocumentosDoFornecedor> ListarDescricaoDeDocumentosUtilizadasPorContratante(int v);
    }

    public class FornecedorDocumentoWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorDocumentoWebForLinkAppService
    {
        private readonly IContratantePjpfWebForLinkService _contratanteFornecedor;
        private readonly IFornecedorWebForLinkService _fornecedorService;
        private readonly IFornecedorDocumentoWebForLinkService _fornecedorDocumentosService;

        public FornecedorDocumentoWebForLinkAppService(
            IContratantePjpfWebForLinkService contratanteFornecedor,
            IFornecedorWebForLinkService fornecedor,
            IFornecedorDocumentoWebForLinkService fornecedorDocumentos)
        {
            try
            {
                _contratanteFornecedor = contratanteFornecedor;
                _fornecedorService = fornecedor;
                _fornecedorDocumentosService = fornecedorDocumentos;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public Fornecedor BuscarPorPJPFId(int pjpfId)
        {
            try
            {
                var pjpf = _fornecedorService.Get(pjpfId);

                return pjpf;
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar os documentos.", e);
            }
        }

        public DocumentosDoFornecedor BuscarPorIdContratanteId(int contratanteId, int id)
        {
            try
            {
                return
                    _fornecedorDocumentosService.Get(
                        d => d.WFD_CONTRATANTE_PJPF.CONTRATANTE_ID == contratanteId && d.ID == id);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar os documentos.", e);
            }
        }

        public List<WFD_CONTRATANTE_PJPF> BuscarDocumentoOutrosContratantes(int ContratanteId, int FornecedorId,
            int DocumentoCHId)
        {
            var contratantePJPF =
                _contratanteFornecedor.Find(
                    x =>
                        x.CONTRATANTE_ID != ContratanteId && x.PJPF_ID == FornecedorId &&
                        x.WFD_PJPF_DOCUMENTOS.Any(
                            y => y.DescricaoDeDocumentos.DESCRICAO_DOCUMENTOS_CH_ID == DocumentoCHId)).ToList();

            return contratantePJPF;
        }

        public void AlterarDocumentos(DocumentosDoFornecedor entidade)
        {
            BeginTransaction();
            _fornecedorDocumentosService.Update(entidade);
            Commit();
        }

        public void Dispose()
        {
        }

        public List<DocumentosDoFornecedor> ListarDescricaoDeDocumentosUtilizadasPorContratante(int v)
        {
           return _fornecedorDocumentosService.ListarDescricaoDeDocumentosUtilizadasPorContratante(v);
        }
    }
}