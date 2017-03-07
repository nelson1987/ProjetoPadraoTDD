using System;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Web.Controllers.Extensoes;

namespace WebForLink.Web.Controllers
{
    public class OutrosDadosController : ControllerPadrao
    {
        private readonly ITipoGrupoWebForLinkAppService _wfdTGrupoBP;
        private readonly IDescricaoWebForLinkAppService _descricaoBP;

        public OutrosDadosController(ITipoGrupoWebForLinkAppService grupo, IDescricaoWebForLinkAppService descricao)
        {
            try
            {
                _wfdTGrupoBP = grupo;
                _descricaoBP = descricao;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        [Authorize]
        public JsonResult ListarOutrosDadosGrupo(int outrosDadosVisao)
        {
            var listaDD = _wfdTGrupoBP.ListarGruposPorVisao(outrosDadosVisao).Select(x => new
            {
                Value = x.ID,
                Text = x.GRUPO_NM
            }).ToList();
            return Json(listaDD, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult ListarOutrosDadosDescricao(int outrosDadosGrupo)
        {
            var listaDD = _descricaoBP.ListarPorGrupoId(outrosDadosGrupo).Select(x => new
            {
                Value = x.ID,
                Text = x.DESCRICAO_NM
            }).ToList();
            return Json(listaDD, JsonRequestBehavior.AllowGet);
        }

    }
}