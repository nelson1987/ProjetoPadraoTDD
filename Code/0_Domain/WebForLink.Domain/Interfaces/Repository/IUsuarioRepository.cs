using WebForLink.Domain.Entities;

namespace WebForLink.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository
    {
        void CriarUsuarioComPlano(int plano, Usuario usuario, Categoria convite, Adesao adesao);

        void CriarUsuarioSemPlano(Usuario usuario, Categoria convite);
    }
}
