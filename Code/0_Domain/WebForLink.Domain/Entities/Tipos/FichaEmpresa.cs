namespace WebForLink.Domain.Entities.Tipos
{
    /// <summary>
    /// Ficha Cadastral Comum das empresas do sistema
    /// </summary>
    public class FichaEmpresa : FichaCadastral
    {
        public Empresa Empresa { get; private set; }
    }
}
