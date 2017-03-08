namespace WebForLink.Domain.Entities.Tipos
{
    /// <summary>
    /// Ficha Cadastral de Empresa não saneada pelo sistema
    /// </summary>
    public class FichaPreCadastro : FichaCadastral
    {
        public FichaPreCadastro(Importacao compartilhamento) : base()
        {
            Importacao = compartilhamento;
        }
        public Importacao Importacao { get; private set; }
    }
}
