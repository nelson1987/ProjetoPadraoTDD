namespace WebForLink.Domain.Entities
{
    public class ConfiguracaoSistema
    {
        private ConfiguracaoSistema()
        {
        }

        public ConfiguracaoSistema(string caminhoArquivo, Contratante contratante)
            : this()
        {
            CaminhoArquivo = caminhoArquivo;
            Contratante = contratante;
        }

        public int Id { get; private set; }
        public string CaminhoArquivo { get; private set; }
        public Contratante Contratante { get; private set; }
    }
}