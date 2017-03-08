namespace WebForLink.Domain.Entities.Tipos
{
    /// <summary>
    /// Ficha Cadastral de Solicitação feitos no sistema
    /// </summary>
    public class FichaSolicitacao : FichaCadastral
    {
        public Solicitacao Solicitacao { get; private set; }
    }
}
