namespace WebForLink.Domain.Entities.Categorias
{
    public class CategoriaEmpresaCh
    {
        protected CategoriaEmpresaCh()
        {
        }

        public CategoriaEmpresaCh(string nome)
            : this()
        {
            Nome = nome;
        }

        public CategoriaEmpresaCh(int id, string nome)
            : this(nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
    }
}