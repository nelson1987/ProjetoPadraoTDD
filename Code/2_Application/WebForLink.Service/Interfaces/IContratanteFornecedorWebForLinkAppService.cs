using System;
using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Service.Common;

namespace WebForLink.Application.Interfaces
{
    public interface IContratanteFornecedorWebForLinkAppService : IService<WFD_CONTRATANTE_PJPF>
    {
        List<WFD_CONTRATANTE_PJPF> buscarPorCnpj(string documento);
        WFD_CONTRATANTE_PJPF BuscarPorID(int id);
        WFD_CONTRATANTE_PJPF BuscarPorPjPfId(int id);
        List<WFD_CONTRATANTE_PJPF> ListarPorPjPfId(int id);
        WFD_CONTRATANTE_PJPF BuscarPjpfPorContratanteComEndereco(int contratantePjpfId);
        WFD_CONTRATANTE_PJPF BuscaFichaCadastralPagante(int contratanteId);
    }
}
