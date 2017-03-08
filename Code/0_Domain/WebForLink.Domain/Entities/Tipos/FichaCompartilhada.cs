namespace WebForLink.Domain.Entities.Tipos
{
    /// <summary>
    /// Ficha Cadastral enviada em Compartilhamento
    /// </summary>
    public class FichaCompartilhada : FichaCadastral
    {
        public FichaCompartilhada(Compartilhamento compartilhamento) : base()
        {
            Compartilhamento = compartilhamento;
        }

        public Compartilhamento Compartilhamento { get; private set; }
    }
}
