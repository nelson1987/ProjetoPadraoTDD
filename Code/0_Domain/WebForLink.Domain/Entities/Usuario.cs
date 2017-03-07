using WebForLink.Domain.Interfaces;

namespace WebForLink.Domain.Entities
{
    public class Usuario : IUsuario
    {
        public Usuario()
        {

        }

        public Usuario(string email, string senha)
        {
            Email = email;
            Senha = senha;
        }

        public string Email { get; set; }

        public string Senha { get; private set; }
    }

    public class UsuarioClienteWebFormat : IUsuario
    {
        public string CodCliente { get; private set; }

        public string Email { get; set; }
    }
}