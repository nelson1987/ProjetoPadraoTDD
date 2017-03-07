using System.Collections.Generic;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Application.Interfaces
{
    public interface IServicosMateriaisWebForLinkAppService : IAppService<TIPO_UNSPSC>
    {
        void ManterMeusMateriaisServicos(List<FORNECEDOR_UNSPSC> unspsc, int pjpfId);
        TIPO_UNSPSC BuscarPorID(int id);
        List<TIPO_UNSPSC> BuscarListaPorID(int[] ids);
        List<TIPO_UNSPSC> BuscarListaPorID(string[] servicos, string[] materiais);

        List<TIPO_UNSPSC> BuscarServicoPorDescricao(string descricao, List<TIPO_UNSPSC> grupo1, List<TIPO_UNSPSC> grupo2,
            List<TIPO_UNSPSC> grupo3);

        List<TIPO_UNSPSC> BuscarServicoGrupo1();
        List<TIPO_UNSPSC> BuscarServicoGrupo2();
        List<TIPO_UNSPSC> BuscarServicoGrupo3();
        List<TIPO_UNSPSC> BuscarServico(int codigo, int niv);

        List<TIPO_UNSPSC> BuscarMaterialPorDescricao(string descricao, List<TIPO_UNSPSC> grupo1,
            List<TIPO_UNSPSC> grupo2, List<TIPO_UNSPSC> grupo3);

        List<TIPO_UNSPSC> BuscarMaterialGrupo1();
        List<TIPO_UNSPSC> BuscarMaterialGrupo2();
        List<TIPO_UNSPSC> BuscarMaterialGrupo3();
        List<TIPO_UNSPSC> BuscarMaterial(int codigo, int niv);
    }
}
