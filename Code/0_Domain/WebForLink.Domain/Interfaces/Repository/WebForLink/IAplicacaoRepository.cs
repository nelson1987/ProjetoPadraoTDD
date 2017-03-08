using System.Collections.Generic;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Domain.Interfaces.Repository
{
    public interface IUsuarioWebForLinkRepository : IRepository<Usuario>
    {
        Usuario BuscarPorLoginParaAcesso(string login);
        Usuario BuscarPorLoginParaAcesso(string login, string senha);
        Usuario BuscarPorCpf(string cpf);
        Usuario BuscarPorEmail(string email);
        Usuario BuscarPorDocumento(string documento);
        Usuario ZerarTentativasLogin(Usuario usuario);
        List<Usuario> ListarPorIdContratante(int idContratante);
        
    }
}