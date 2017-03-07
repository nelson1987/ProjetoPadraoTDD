using System;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IDocumentoWebForLinkService : IService<DocumentosDoFornecedor>
    {
        DocumentosDoFornecedor BuscarPorId(int documentoId);

        DocumentosDoFornecedor CadastrarNovoDocumento(DocumentosDoFornecedor documento, int contratanteId,
            int tipoContratante, int? arquivoId);
    }

    public class DocumentoWebForLinkService : Service<DocumentosDoFornecedor>, IDocumentoWebForLinkService
    {
        private readonly IContratanteFornecedorWebForLinkRepository _contratanteFornecedor;
        private readonly IFornecedorDocumentoWebForLinkRepository _documentosFornecedor;

        public DocumentoWebForLinkService(IContratanteFornecedorWebForLinkRepository contratanteFornecedor,
            IFornecedorDocumentoWebForLinkRepository fornecedorDocumento) : base(fornecedorDocumento)
        {
            try
            {
                _contratanteFornecedor = contratanteFornecedor;
                _documentosFornecedor = fornecedorDocumento;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public DocumentosDoFornecedor CadastrarNovoDocumento(DocumentosDoFornecedor documento, int contratanteId,
            int tipoContratante, int? arquivoId)
        {
            try
            {
                var contratantePjPf =
                    _contratanteFornecedor.Find(x => x.CONTRATANTE_ID == contratanteId && x.TP_PJPF == tipoContratante)
                        .FirstOrDefault();
                if (contratantePjPf == null)
                    throw new ServiceWebForLinkException("Fornecedor não encontrado");

                documento.CONTRATANTE_PJPF_ID = contratantePjPf.ID;
                documento.DATA_UPLOAD = DateTime.Now;
                documento.PJPF_ID = contratantePjPf.PJPF_ID;
                documento.ARQUIVO_ID = arquivoId != 0 ? arquivoId : null;
                documento.NOME_ARQUIVO = null;
                documento.SOLICITACAO_ID = null;
                documento.EXTENSAO_ARQUIVO = null;
                documento.LISTA_DOCUMENTO_ID = null;
                documento.EXIGE_VALIDADE = null;
                documento.PERIODICIDADE_ID = null;
                documento.OBRIGATORIO = null;

                _documentosFornecedor.Add(documento);
                return documento;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar incluir Documentos", ex);
            }
        }

        public DocumentosDoFornecedor BuscarPorId(int documentoId)
        {
            try
            {
                return _documentosFornecedor.Get(documentoId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar Documento", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}