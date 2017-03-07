using System;
using System.IO;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Service.Infrastructure;

namespace WebForLink.Domain.Services.Process
{
    public interface IProcessoLoginWebForLinkService
    {
        ProcessoLoginDTO ExecutarLogin(string login, string senha);
    }

    public class ProcessoLoginWebForLinkService : IProcessoLoginWebForLinkService
    {
        private readonly IUsuarioSenhaHistoricoWebForLinkService _usuarioSenhaHistoricoService;
        private readonly IUsuarioWebForLinkService _usuarioService;

        public ProcessoLoginWebForLinkService(IUsuarioWebForLinkService usuario,
            IUsuarioSenhaHistoricoWebForLinkService usuarioSenhaHistorico)
        {
            _usuarioService = usuario;
            _usuarioSenhaHistoricoService = usuarioSenhaHistorico;
        }

        public ProcessoLoginDTO ExecutarLogin(string login, string senha)
        {
            try
            {
                var usuarioLogado = _usuarioService.BuscarPorLogin(login);

                //Valida se Usuário Existe
                if (usuarioLogado == null)
                    throw new ProcessoLoginException("Usuário inválido!");

                ValidarBloqueioTempo(usuarioLogado);

                if (Acessar(login, senha))
                {
                    if (usuarioLogado.CONTA_TENTATIVA > 0)
                        _usuarioService.ZerarTentativasLogin(usuarioLogado);

                    return new ProcessoLoginDTO {Status = true, Usuario = usuarioLogado.ID};
                }
                var ativo = usuarioLogado != null && usuarioLogado.ATIVO;
                if (ativo)
                {
                    _usuarioService.ContabilizarErroLogin(usuarioLogado);
                    throw new ProcessoLoginException("Login ou Senha Inválido!");
                }
                throw new ProcessoLoginException("Usuário bloqueado!");
            }
            catch (ProcessoLoginException ex)
            {
                return new ProcessoLoginDTO {Status = false, Mensagem = ex.Message};
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao tentar logar.", ex);
            }
        }

        private bool Acessar(string email, string senha)
        {
            var usuario = _usuarioService.BuscarPorLoginParaAcesso(email);

            if (usuario != null && BusinessPasswordHash.ValidatePassword(senha, usuario.SENHA))
                return true;

            return false;
        }

        private void ValidarBloqueioTempo(Usuario usuario)
        {
            var historicoSenha = usuario.WFD_USUARIO_SENHAS_HIST.OrderBy(x => x.SENHA_DT).LastOrDefault();

            if (historicoSenha != null)
            {
                if (usuario.EXPIRA_EM_DIAS > 0)
                {
                    var valor = (DateTime.Now - historicoSenha.SENHA_DT).Days;
                    if (valor > usuario.EXPIRA_EM_DIAS)
                    {
                        var chave = Path.GetRandomFileName().Replace(".", "");
                        usuario.TROCAR_SENHA = chave;
                        usuario.PRIMEIRO_ACESSO = true;
                        //usuarioBP.ZerarTentativasLogin(usuario);
                    }
                }
            }
            else
            {
                try
                {
                    //INSERT INICIAL NA TABELA DE WFD_USUARIO_SENHAS_HIST
                    var historicoSenhaInclusao = new USUARIO_SENHAS
                    {
                        SENHA = usuario.SENHA,
                        SENHA_DT = DateTime.Now,
                        USUARIO_ID = usuario.ID
                    };
                    _usuarioSenhaHistoricoService.InserirSenhaUsuario(historicoSenhaInclusao);
                }
                catch (Exception ex)
                {
                    throw new ServiceWebForLinkException("Erro ao tentar Validar Bloqueio de Tempo.", ex);
                    //Log.Error(ex.Message);
                }
            }
        }
    }
}