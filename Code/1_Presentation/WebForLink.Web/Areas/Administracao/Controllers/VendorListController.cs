using System.Net;
using System.Web.Mvc;

namespace WebForLink.Web.Areas.ConfiguracaoInicial.Controllers
{
    [AllowAnonymous]
    public class VendorListController : Controller
    {
        // GET: ConfiguracaoInicial/VendorList
        public ActionResult Index()
        {
            return View();
        }
        // GET: ConfiguracaoInicial/VendorList
        public ActionResult ConvidarFornecedor(string cnpj)
        {
            return View();
        }
        // GET: ConfiguracaoInicial/VendorList
        /// <summary>
        /// 02.03	Enviar e-mail informando o motivo do convite e link para ele acessar o Webforlink, conhecer o produto e realizar o cadastro de sua Ficha Cadastral e o upload dos documentos solicitados.
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public ActionResult EnviarEmailConvite(string cnpj)
        {
            return View();
        }

        [HttpGet]
        public JsonResult RetornarFichaCadastral(string cnpj, string codCliente)
        {
            var resultado = "";
            if (string.IsNullOrEmpty(cnpj))
                resultado = "problema na string";

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { mensagem = resultado }, JsonRequestBehavior.AllowGet);

            //Response.StatusCode = (int)HttpStatusCode.OK;
        }
    }
}