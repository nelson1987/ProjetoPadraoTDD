using System;
using System.Collections.Generic;
using WebForLink.Application.Interfaces;

namespace WebForLink.Application.Services
{
    public class ControleAcessoAppService : IControleAccessoService
    {
        //private readonly IFornecedorService _service;

        //public ControleAcessoAppService(IFornecedorService albumService)
        //{
        //    //contextoDeSeguranca = ISecurityContext.GetContext();
        //    _service = albumService;
        //}

        public bool AutenticarUsuario(string chave, string senha)
        {
            try
            {
                return true;
                //if (contextoDeSeguranca.CurrentLoggedUser == null)
                //    contextoDeSeguranca.UserAuthenticator.Logon(chave, senha);
                //else
                //    contextoDeSeguranca.UserAuthenticator.ValidateCredentials(chave, senha);
            }
                //catch (InvalidCredentialsException ex)
                //{
                //    throw new Exception(ex.Message, ex);
                //}
            catch (Exception ex)
            {
                throw new Exception("Erro na autenticação do usuário", ex);
            }
        }

        public bool Autenticado()
        {
            throw new NotImplementedException();
        }

        public UsuarioSistema BuscarDadosUsuario(string chave)
        {
            throw new NotImplementedException();
        }

        public UsuarioSistema BuscarDadosUsuarioLogado()
        {
            throw new NotImplementedException();
        }

        public void DesautenticarUsuario()
        {
            throw new NotImplementedException();
        }

        public IList<UsuarioSistema> ListarDadosUsuarios(string[] chave)
        {
            throw new NotImplementedException();
        }

        public IList<PerfilSistema> ListarPerfisAplicacao()
        {
            throw new NotImplementedException();
        }

        public IList<PerfilSistema> ListarPerfisUsuarioLogado()
        {
            throw new NotImplementedException();
        }

        public IList<UsuarioSistema> ListarUsuariosPorPerfil(string perfil)
        {
            throw new NotImplementedException();
        }
    }
}