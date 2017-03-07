namespace WebForLink.Domain.Entities.WebForLink
{
    public class TipoDocumento
    {
        public TipoDocumento(int id, string descricao)
        {

            Id = id;
            Descricao = descricao;
        }
        public int Id { get; private set; }
        public string Descricao { get; private set; }
    }
    public class DescricaoDocumento
    {
        public DescricaoDocumento(int id, string descricao, int tipo)
        {
            Id = id;
            Descricao = descricao;
            Tipo = tipo;
        }
        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public int Tipo { get; private set; }
    }
}
