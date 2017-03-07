using System;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Web.Controllers.Extensoes;

namespace WebForLink.Web.Controllers
{
    public class OrganizacaoCompraController : ControllerPadrao
    {
        private readonly IContratanteOrganizacaoComprasWebForLinkAppService _organizacaoComprasService;

        public OrganizacaoCompraController(IContratanteOrganizacaoComprasWebForLinkAppService organizacaoCompras)
        {
            try
            {
                _organizacaoComprasService = organizacaoCompras;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        [HttpPost]
        [Authorize]
        public JsonResult Listar(int empresa)
        {
            var listaDD = _organizacaoComprasService.ListarTodosPorIdContratante(empresa).Select(x => new
            {
                Value = x.ID,
                Text = x.ORG_COMPRAS_DSC
            }).ToList();
            return Json(listaDD, JsonRequestBehavior.AllowGet);
        }
    }
}