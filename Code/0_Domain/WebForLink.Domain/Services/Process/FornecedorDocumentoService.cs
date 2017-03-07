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
    public interface IFornecedorDocumentoWebForLinkService : IService<DocumentosDoFornecedor>
    {
        Fornecedor BuscarPorPJPFId(int pjpfId);
        DocumentosDoFornecedor BuscarPorIdContratanteId(int contratanteId, int id);

        List<WFD_CONTRATANTE_PJPF> BuscarDocumentoOutrosContratantes(int ContratanteId, int FornecedorId,
            int DocumentoCHId);

        void AlterarDocumentos(DocumentosDoFornecedor entidade);
        List<DocumentosDoFornecedor> ListarDescricaoDeDocumentosUtilizadasPorContratante(int v);
    }

    public class FornecedorDocumentoWebForLinkService : Service<DocumentosDoFornecedor>,
        IFornecedorDocumentoWebForLinkService
    {
        private readonly IContratanteFornecedorWebForLinkRepository _contratanteFornecedor;
        private readonly IFornecedorWebForLinkRepository _fornecedorRepository;
        private readonly IFornecedorDocumentoWebForLinkRepository _fornecedorDocumentosRepository;

        public FornecedorDocumentoWebForLinkService(
            IContratanteFornecedorWebForLinkRepository contratanteFornecedor,
            IFornecedorWebForLinkRepository fornecedor,
            IFornecedorDocumentoWebForLinkRepository fornecedorDocumentos)
            : base(fornecedorDocumentos)
        {
            try
            {
                _contratanteFornecedor = contratanteFornecedor;
                _fornecedorRepository = fornecedor;
                _fornecedorDocumentosRepository = fornecedorDocumentos;
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
                var pjpf = _fornecedorRepository.Get(pjpfId);

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
                    _fornecedorDocumentosRepository.Get(
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
            _fornecedorDocumentosRepository.Update(entidade);
        }

        public void Dispose()
        {
        }

        public List<DocumentosDoFornecedor> ListarDescricaoDeDocumentosUtilizadasPorContratante(int v)
        {
           return _fornecedorDocumentosRepository
                .Find(x => x.DescricaoDeDocumentos.CONTRATANTE_ID == v)
                .OrderBy(x=>/* new{ TipoDescricao = */x.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO/*, Descricao = x.DescricaoDeDocumentos.DESCRICAO }*/)
                .ToList();
        }
    }
}