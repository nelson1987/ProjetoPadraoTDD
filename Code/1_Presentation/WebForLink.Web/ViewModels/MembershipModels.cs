using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;

namespace WebForLink.Web.ViewModels
{
    public interface ITranspetroPrincipal : IPrincipal
    {
        string Chave { get; set; }
        string Nome { get; set; }
        string Email { get; set; }
        TranspetroPrincipalPerfil PerfilCorrente { get; set; }
        IList<TranspetroPrincipalPerfil> Perfis { get; set; }
    }

    public class TranspetroPrincipal : ITranspetroPrincipal
    {
        private TranspetroPrincipalPerfil perfilCorrente;

        public TranspetroPrincipal(string chave)
        {
            Identity = new GenericIdentity(chave);
            Chave = chave;
        }

        public bool FornecedorIndividual { get; set; }
        public string Chave { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public TranspetroPrincipalPerfil PerfilCorrente
        {
            get
            {
                if (perfilCorrente == null)
                {
                    perfilCorrente = Perfis[0];
                }
                return perfilCorrente;
            }
            set { perfilCorrente = value; }
        }

        public IList<TranspetroPrincipalPerfil> Perfis { get; set; }
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return PerfilCorrente.Descricao.Equals(role, StringComparison.InvariantCultureIgnoreCase);
        }

        public bool Administrador()
        {
            if (Perfis == null || Perfis.Count() == 0)
            {
                return false;
            }
            return
                Perfis.Where(x => x.Descricao.Equals("administrador", StringComparison.InvariantCultureIgnoreCase))
                    .Count() > 0;
        }
    }

    public class TranspetroPrincipalPerfil
    {
        public string IdPerfil { get; set; }
        public string Descricao { get; set; }
    }
}