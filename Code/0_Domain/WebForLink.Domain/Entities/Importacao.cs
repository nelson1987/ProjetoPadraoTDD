namespace WebForLink.Domain.Entities
{
    public abstract class Importacao
    {
        private Importacao()
        {
        }

        protected Importacao(Contratante contratante, Arquivo arquivo)
            : this()
        {
            Contratante = contratante;
            Arquivo = arquivo;
        }

        public int Id { get; private set; }
        public Contratante Contratante { get; private set; }
        public Arquivo Arquivo { get; private set; }
    }
}