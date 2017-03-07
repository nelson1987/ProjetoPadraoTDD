namespace WebForLink.Domain.Entities
{
    public class Perfil
    {
        public Perfil(int id, string nome, string descricao, int contratante)
        {
            Id = id;
            Sigla = nome;
            Descricao = descricao;
            Contratante = contratante;
        }

        public Perfil(string nome, string descricao)
            :this(0, nome, descricao, 1)
        {

        }

        public int Id { get; set; }
        public string Sigla { get; set; }
        public string Descricao { get; set; }
        public int Contratante { get; set; }
    }
}
