namespace WebForLink.Domain.Entities
{
    public class Contato
    {
        private Contato()
        {
        }

        public Contato(string nome, string email, string telefone, string celular) : this()
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Celular = celular;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Celular { get; private set; }
        public Empresa Empresa { get; private set; }
    }
}