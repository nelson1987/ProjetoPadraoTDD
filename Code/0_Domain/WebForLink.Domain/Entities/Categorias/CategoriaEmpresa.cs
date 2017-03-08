namespace WebForLink.Domain.Entities.Categorias
{
    public class CategoriaEmpresa
    {
        private CategoriaEmpresa()
        {
        }

        public CategoriaEmpresa(string codigo, string descricao)
            : this()
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        public int Id { get; private set; }
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public CategoriaEmpresa CategoriaPai { get; private set; }
        public CategoriaEmpresaCh CategoriaCh { get; private set; }
        public Contratante Contratante { get; private set; }
        public bool Ativo { get; private set; }
    }
}