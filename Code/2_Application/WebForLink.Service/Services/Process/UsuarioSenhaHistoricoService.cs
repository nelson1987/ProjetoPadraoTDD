using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public interface ISolicitacaoTramiteWebForLinkAppService : IAppService<SOLICITACAO_TRAMITE>
    {
        SOLICITACAO_TRAMITE BuscarTramitePorSolicitacaoIdPapelId(int solicitacaoId, int papelId);
        bool SolicitacaoAprovadaPorUmAprovador(int solicitacao);
        bool SolicitacaoFornecedorFinalizou(int solicitacao);
        bool AlterarTramite();
    }
    public interface ITipoBloqueioRoboWebForLinkAppService
    {
        List<TIPO_FUNCAO_BLOQUEIO> ListarTodosPorCodigoFuncaoBloqueio();
        TIPO_FUNCAO_BLOQUEIO BuscarPorID(int id);
    }

    public interface ITipoCadastroWebForLinkAppService
    {
        TIPO_CADASTRO_FORNECEDOR BuscarPorID(int id);
        List<TIPO_CADASTRO_FORNECEDOR> ListarTodos();
    }

    public interface ITipoDocumentosChWebForLinkAppService
    {
    }
    public interface ITipoGrupoWebForLinkAppService
    {
        List<TIPO_GRUPO> ListarGruposPorVisao(int visaoId);
    }
    


    public class UsuarioSenhaHistoricoWebForLinkAppService : AppService<WebForLinkContexto>,
        IUsuarioSenhaHistoricoWebForLinkAppService
    {
        private readonly IUsuarioWebForLinkService _usuarioBP;
        private readonly IUsuarioSenhasHistWebForLinkService _usuarioSenhaHistoricoBP;

        public UsuarioSenhaHistoricoWebForLinkAppService(IUsuarioWebForLinkService usuario,
            IUsuarioSenhasHistWebForLinkService usuarioSenhaHistorico)
        {
            try
            {
                _usuarioBP = usuario;
                _usuarioSenhaHistoricoBP = usuarioSenhaHistorico;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="login"></param>
        /// <returns>bool</returns>
        public bool ExecutarBloqueio90Dias(string login)
        {
            var historicoSenha = BuscarHistoricoPorLogin(login);
            if (historicoSenha != null)
            {
                var valor = (DateTime.Now - historicoSenha.SENHA_DT).Days;
                if (valor > 89)
                {
                    _usuarioBP.Bloqueio90Dias(historicoSenha.WFD_USUARIO);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>WFD_USUARIO_SENHAS_HIST</returns>
        public USUARIO_SENHAS BuscarPorId(int id)
        {
            try
            {
                return _usuarioSenhaHistoricoBP.BuscarPorIdComUsuario(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns>WFD_USUARIO_SENHAS_HIST</returns>
        public USUARIO_SENHAS InserirSenhaUsuario(USUARIO_SENHAS entidade)
        {
            try
            {
                _usuarioSenhaHistoricoBP.Add(entidade);
                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir uma senha de usuário", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="login"></param>
        /// <returns>WFD_USUARIO_SENHAS_HIST</returns>
        public USUARIO_SENHAS BuscarHistoricoPorLogin(string login)
        {
            try
            {
                return _usuarioSenhaHistoricoBP.BuscarHistoricoPorLogin(login);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns>WFD_USUARIO_SENHAS_HIST</returns>
        public USUARIO_SENHAS BuscarHistoricoPorIdUsuario(int id)
        {
            try
            {
                return _usuarioSenhaHistoricoBP.BuscarHistoricoPorIdUsuario(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idContratante"></param>
        /// <returns></returns>
        public List<USUARIO_SENHAS> ListarPorIdContratante(int idContratante)
        {
            try
            {
                return _usuarioSenhaHistoricoBP.ListarPorIdContratante(idContratante);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário por Contratante", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<USUARIO_SENHAS> Listar6UltimasPorUsuarioId(int idUsuario)
        {
            try
            {
                return _usuarioSenhaHistoricoBP.Listar6UltimasPorUsuarioId(idUsuario);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao cadastrar Perguntas", ex);
            }
        }

        public USUARIO_SENHAS Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public USUARIO_SENHAS Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public USUARIO_SENHAS GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<USUARIO_SENHAS> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<USUARIO_SENHAS> Find(Expression<Func<USUARIO_SENHAS, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(USUARIO_SENHAS entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(USUARIO_SENHAS entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(USUARIO_SENHAS entity)
        {
            throw new NotImplementedException();
        }

        #region Acesso BM externa

        //private readonly UsuarioService usuarioService = new UsuarioService();

        #endregion
    }
}