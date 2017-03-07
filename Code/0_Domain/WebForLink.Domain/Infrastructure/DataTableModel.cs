namespace WebForLink.Domain.Infrastructure
{
    public class SearchItemDataTable
    {
        public string value { get; set; }
        public bool regex { get; set; }
    }

    public class SearchDataTable
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public SearchItemDataTable search { get; set; }
    }

    public class OrdenacaoDataTable
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class SelectDataTable<TEntity> where TEntity : class
    {
        public int draw { get; set; }
        public SearchDataTable[] columns { get; set; }
        public OrdenacaoDataTable[] order { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public TEntity Dados { get; set; }
    }
}