namespace WebForLink.Domain.Entities
{
    public abstract class Robo
    {
        private Robo()
        {
        }

        protected Robo(string razaoSocial)
            : this()
        {
            RazaoSocial = razaoSocial;
        }

        public int Id { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Documento { get; private set; }
        public string RadicalDeCnpj { get; private set; }
        public string Latitude { get; private set; }
        public string Longitude { get; private set; }
    }
}