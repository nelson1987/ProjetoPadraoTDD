namespace WebForLink.Domain.Interfaces.Repository
{
    public interface IAdesaoRepository
    {
        void CriarAdesao();

        void CriarAdesaoComPlano(); //TODO: Incluir na tabela de UsuariosPagantes

        void CriarAdesaoSemPlano();
    }
}
