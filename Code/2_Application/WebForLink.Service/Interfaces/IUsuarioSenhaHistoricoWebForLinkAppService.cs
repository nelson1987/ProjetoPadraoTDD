using System.Collections.Generic;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Application.Interfaces
{
    public interface IUsuarioSenhaHistoricoWebForLinkAppService : IAppService<USUARIO_SENHAS>
    {
        bool ExecutarBloqueio90Dias(string login);
        USUARIO_SENHAS BuscarPorId(int id);
        USUARIO_SENHAS InserirSenhaUsuario(USUARIO_SENHAS entidade);
        USUARIO_SENHAS BuscarHistoricoPorLogin(string login);
        USUARIO_SENHAS BuscarHistoricoPorIdUsuario(int id);
        List<USUARIO_SENHAS> ListarPorIdContratante(int idContratante);
        List<USUARIO_SENHAS> Listar6UltimasPorUsuarioId(int idUsuario);
    }
}
