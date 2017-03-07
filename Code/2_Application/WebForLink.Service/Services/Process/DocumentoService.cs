using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public interface IDocumentoWebForLinkAppService : IAppService<DocumentosDoFornecedor>
    {
        DocumentosDoFornecedor BuscarPorId(int documentoId);

        DocumentosDoFornecedor CadastrarNovoDocumento(DocumentosDoFornecedor documento, int contratanteId,
            int tipoContratante, int? arquivoId);
    }

    public class DocumentoWebForLinkAppService : AppService<WebForLinkContexto>, IDocumentoWebForLinkAppService
    {
        private readonly IContratantePjpfWebForLinkService _contratanteFornecedor;
        private readonly IFornecedorDocumentoWebForLinkService _documentosFornecedor;

        public DocumentoWebForLinkAppService(IContratantePjpfWebForLinkService contratanteFornecedor,
            IFornecedorDocumentoWebForLinkService fornecedorDocumento)
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
                    _contratanteFornecedor.Get(x => x.CONTRATANTE_ID == contratanteId && x.TP_PJPF == tipoContratante);
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
                BeginTransaction();
                _documentosFornecedor.Add(documento);
                Commit();
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

        public DocumentosDoFornecedor Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DocumentosDoFornecedor Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public DocumentosDoFornecedor GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DocumentosDoFornecedor> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DocumentosDoFornecedor> Find(Expression<Func<DocumentosDoFornecedor, bool>> predicate,
            bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(DocumentosDoFornecedor entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(DocumentosDoFornecedor entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(DocumentosDoFornecedor entity)
        {
            throw new NotImplementedException();
        }
    }
}