using System;
using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IUsuarioSenhaHistoricoWebForLinkService
    {
        bool ExecutarBloqueio90Dias(string login);
        USUARIO_SENHAS BuscarPorId(int id);
        USUARIO_SENHAS InserirSenhaUsuario(USUARIO_SENHAS entidade);
        USUARIO_SENHAS BuscarHistoricoPorLogin(string login);
        USUARIO_SENHAS BuscarHistoricoPorIdUsuario(int id);
        List<USUARIO_SENHAS> ListarPorIdContratante(int idContratante);
        List<USUARIO_SENHAS> Listar6UltimasPorUsuarioId(int idUsuario);
    }

    public class UsuarioSenhaHistoricoWebForLinkService : Service<USUARIO_SENHAS>,
        IUsuarioSenhaHistoricoWebForLinkService
    {
        private readonly IUsuarioWebForLinkService _usuarioBP;
        private readonly IUsuarioSenhasHistWebForLinkRepository _usuarioSenhaHistoricoBP;

        public UsuarioSenhaHistoricoWebForLinkService(
            IUsuarioWebForLinkService usuario,
            IUsuarioSenhasHistWebForLinkRepository usuarioSenhaHistorico) : base(usuarioSenhaHistorico)
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

        public void Dispose()
        {
        }

        #region Acesso BM externa

        //private readonly UsuarioService usuarioService = new UsuarioService();

        #endregion
    }
}