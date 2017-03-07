using System;
using System.Web.Mvc;

namespace WebForLink.Web.ViewModels
{
    public interface ISecurityService
    {
        bool Autenticado { get; }
        bool Autenticar(string login, string senha);
        void Login(Usuario usuario);
        void Login(string login);
        void LogOut();
    }

    [HandleError(ExceptionType = typeof (NullReferenceException), View = "ConferenceNotFound")]
    public class SecurityService : ISecurityService
    {
        public bool Autenticar(string login, string senha)
        {
            var usuario = new Usuario();

            throw new NotImplementedException();
        }

        public bool Autenticado { get;
            //get { HttpContext. }
        }

        public void Login(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public void Login(string login)
        {
            throw new NotImplementedException();
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }
    }
}