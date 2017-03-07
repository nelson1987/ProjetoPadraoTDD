using Ninject;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.CrossCutting.InversionControl;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Services;
using WebForLink.Web.Filters;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers
{
    /// <summary>
    /// Controller para a Ficha Cadastral do fornecedor Individual
    /// </summary>
    [AllowAnonymous]
    [BreadCrumb]
    public class FichaController : Controller
    {
        private readonly IFornecedorAppService _fornecedorAppService;

        public FichaController(IFornecedorAppService categoriaAppService)
        {
            _fornecedorAppService = categoriaAppService;
        }

        //public FichaController()
        //{
        //    var ioc = new IoC();
        //    _fornecedorAppService = ioc.Kernel.Get<IFornecedorAppService>();
        //}

        // GET: Ficha
        public ActionResult Visualizar()//string chaveUrl)
        {
            var chaveUrl = "cnpj=34151100001706";// O do Jesus
            //chaveUrl = "cnpj=05879626000133";
            Criptografia cripto = new Criptografia(EnumCripto.Criptografar, chaveUrl, "r10X310y");
            var criptografado = cripto.Resultado;

            Criptografia descripto = new Criptografia(EnumCripto.Descriptografar, criptografado, "r10X310y");
            var descriptografado = descripto.Resultado;

            Criptografia LinkDescripto = new Criptografia(EnumCripto.LinkDescriptografar, criptografado, "r10X310y");
            var LinkDescriptografado = LinkDescripto.Resultados;

            var dados = _fornecedorAppService.Find(x => x.Documento == descriptografado.Substring(0, 9), true);
            return View();
        }
        // GET: Ficha
        public ActionResult Index()
        {
            FornecedorIndividualVM model = new FornecedorIndividualVM { Documento = "73.076.015/0001-07", RazaoSocial = "Trufell Peças e Serviços" };
            var validationResult = _fornecedorAppService.Create(FornecedorIndividualVM.ToModel(model));
            //var validationResult = _fornecedorAppService.Create(new Fornecedor("73.076.015/0001-07", "Trufell Peças e Serviços"));

            if (validationResult.EstaValidado)
                return View(new FichaCadastralVM("73.076.015/0001-07", "Trufell Peças e Serviços", "Trufell Group"));
            return View(new FichaCadastralVM("73.076.015/0001-07", "Trufell Peças e Serviços", "Trufell Group"));
            // return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Incluir(FornecedorIndividualVM model)
        {
            if (ModelState.IsValid)
            {
                var validationResult = _fornecedorAppService.Create(FornecedorIndividualVM.ToModel(model));

                if (validationResult.EstaValidado)
                    return RedirectToAction("Index");

                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.Message);
            }
            return View(model);
        }


        public JsonResult ExcluirEndereco(int id)
        {
            HttpStatusCodeResult resultado = new HttpStatusCodeResult(300);
            return Json(string.Format("Excluido Endereço {0}.", id), JsonRequestBehavior.AllowGet);
        }

        #region Enviar Pra Classe Mestra Com Herança em CONTROLLER

        public PartialViewResult PartialViewEndereco()
        {
            return PartialView("~/Views/Ficha/Partials/_IncluirEndereco.cshtml", new DadosEnderecoVM() { });
        }
        #endregion
    }

}