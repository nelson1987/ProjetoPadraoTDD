namespace WebForLink.Domain.Entities.Status
{
    public class StatusPreCadastro
    {
        protected StatusPreCadastro()
        {
        }

        public StatusPreCadastro(string nome) : this()
        {
            Nome = nome;
        }

        public StatusPreCadastro(int id, string nome) : this(nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
    }
}