namespace WebForLink.Domain.Entities
{
    public class Endereco
    {
        private Endereco()
        {
        }

        public Endereco(string rua, Empresa empresa) : this()
        {
            Rua = rua;
            Empresa = empresa;
        }

        public int Id { get; private set; }
        public string Rua { get; private set; }
        public Empresa Empresa { get; private set; }
    }
}