using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public class Usuario
    {
        private Usuario()
        {
            Perfis = new List<Perfil>();
            Papeis = new List<Papel>();
            //Aplicacoes = new List<Aplicacao>();
        }

        public Usuario(string login)
            : this()
        {
            Login = login;
        }

        public Usuario(string login, Contratante contratante)
            : this()
        {
            Login = login;
            Contratante = contratante;
        }

        public int Id { get; private set; }
        public string Login { get; private set; }
        public Contratante Contratante { get; private set; }
        public Aplicacao Aplicacao { get; private set; }
        public List<Perfil> Perfis { get; private set; }
        public List<Papel> Papeis { get; private set; }
        public List<Solicitacao> Solicitacoes { get; private set; }

        public void AdicionarPerfilNumaAplicacao(Aplicacao aplicacao, Perfil perfil)
        {
            aplicacao.AdicionarPerfil(perfil);
            AdicionarPerfil(perfil);
        }

        public void SetContratante(Contratante contratante)
        {
            Contratante = contratante;
        }

        public void SetAplicacao(Aplicacao aplicacao)
        {
            Aplicacao = aplicacao;
        }

        public void AdicionarPerfil(Perfil administrador)
        {
            if (!Contratante.TemEssePerfil(administrador))
                Perfis.Add(administrador);
        }

        public void AdicionarPapel(Papel papelDeSolicitante)
        {
            Papeis.Add(papelDeSolicitante);
        }
    }
}