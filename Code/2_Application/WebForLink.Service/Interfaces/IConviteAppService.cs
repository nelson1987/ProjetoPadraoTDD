using System.Collections.Generic;
using WebForLink.Domain.Entities;

namespace WebForLink.Application.Interfaces
{
    public interface IConviteAppService
    {
        Categoria BuscarConvite(int idConvite);
        int DescriptografarLinkConvite(string chaveUrl);
        void EnviarConvite(Categoria convite);
        void ReenviarConvite(int idConviteAnterior, List<Documento> documentos);
        void ValidarConvite(int idConvite);
    }
}
