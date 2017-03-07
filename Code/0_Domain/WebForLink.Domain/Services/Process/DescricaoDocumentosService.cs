using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class DescricaoDocumentosChWebForLinkService : Service<WFD_DESCRICAO_DOCUMENTOS_CH>,
        IDescricaoDocumentosChWebForLinkService
    {
        private readonly IDescricaoDocumentosChWebForLinkRepository _processo;

        public DescricaoDocumentosChWebForLinkService(IDescricaoDocumentosChWebForLinkRepository processo)
            : base(processo)
        {
            try
            {
                _processo = processo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }
    }

    public class FornecedorCategoriaChWebForLinkService : Service<FORNECEDOR_CATEGORIA_CH>,
        IFornecedorCategoriaChWebForLinkService
    {
        private readonly IFornecedorCategoriaChWebForLinkRepository _processo;

        public FornecedorCategoriaChWebForLinkService(IFornecedorCategoriaChWebForLinkRepository processo)
            : base(processo)
        {
            try
            {
                _processo = processo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }
    }

    public class UsuarioSenhasHistWebForLinkService : Service<USUARIO_SENHAS>, IUsuarioSenhasHistWebForLinkService
    {
        private readonly IUsuarioSenhasHistWebForLinkRepository _processo;

        public UsuarioSenhasHistWebForLinkService(IUsuarioSenhasHistWebForLinkRepository processo) : base(processo)
        {
            try
            {
                _processo = processo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public USUARIO_SENHAS BuscarHistoricoPorIdUsuario(int id)
        {
            return _processo.Find(x => x.USUARIO_ID == id).FirstOrDefault();
        }

        public USUARIO_SENHAS BuscarHistoricoPorLogin(string login)
        {
            return _processo.Find(x => x.WFD_USUARIO.LOGIN == login).FirstOrDefault();
        }

        public USUARIO_SENHAS BuscarPorIdComUsuario(int id)
        {
            return _processo.Find(x => x.ID == id).FirstOrDefault();
        }

        public List<USUARIO_SENHAS> Listar6UltimasPorUsuarioId(int idUsuario)
        {
            return _processo.Find(x => x.USUARIO_ID == idUsuario).Take(6).ToList();
        }

        public List<USUARIO_SENHAS> ListarPorIdContratante(int idContratante)
        {
            return _processo.Find(x => x.WFD_USUARIO.CONTRATANTE_ID == idContratante).ToList();
        }

        public void Dispose()
        {
        }
    }

    public class TipoDocumentoWebForLinkService : Service<TipoDeDocumento>, ITipoDocumentoWebForLinkService
    {
        private readonly ITipoDocumentoWebForLinkRepository _processo;

        public TipoDocumentoWebForLinkService(ITipoDocumentoWebForLinkRepository processo) : base(processo)
        {
            try
            {
                _processo = processo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }
    }

    public class DescricaoDocumentosWebForLinkService : Service<DescricaoDeDocumentos>,
        IDescricaoDocumentosWebForLinkService
    {
        private readonly IDescricaoDocumentosWebForLinkRepository _processo;

        public DescricaoDocumentosWebForLinkService(IDescricaoDocumentosWebForLinkRepository processo) : base(processo)
        {
            try
            {
                _processo = processo;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }
    }
}