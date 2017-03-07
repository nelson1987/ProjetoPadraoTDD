using WebForLink.Web.Controllers.Extensoes;

namespace WebForLink.Web.Areas.Administracao.Controllers
{
    public class InteresseController : ControllerPadrao
    {
        /*
        // GET: Administracao/Interesse
        public ActionResult Index()
        {
            return View();
        }
        // POST: /Interesse/Listar/?object:Interesse
        /// <summary>
        /// Lista todos os interesses do usuário
        /// </summary>
        /// <param name="filtroConsulta"></param>
        /// <param name="grid"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public JsonResult Listar(InteresseFiltroGridModel filtroConsulta, GridSettings grid)
        {
            var opPaginacao = Mapper.Map<OpcaoPaginacaoGrid>(grid);
            var filtro = Mapper.Map<InteressePesquisaParametros>(filtroConsulta);
            var retornoConsulta = ConsultarInteresse(filtro, opPaginacao);
            var retorno = Mapper.Map<IList<InteresseGridModel>>(retornoConsulta.RegistrosPagina);
            var result = new GridResult()
            {
                page = grid.Page,
                pageSize = grid.Rows,
                records = retornoConsulta.TotalRegistros,
                rows = retorno.ToArray()

            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Consulta Interesses de acordo com os filtros selecionados         
        /// </summary>
        /// <returns>Retorna os Interesses cadastradas de acordo com o filtro</returns>
        public RetornoPesquisaGrid<Interesse> ConsultarInteresse(InteressePesquisaParametros filtroConsulta, OpcaoPaginacaoGrid paginacao)
        {
            try
            {
                return bmInteresse.ListarPorFiltroPesquisa(filtroConsulta, paginacao);

            }
            catch (Exception ex)
            {
                throw new BINTBusinessException("Erro ao consultar Interesse", ex);
            }
        }
         */
    }
}