using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using WebForLink.Application.Interfaces;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Validation;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers
{
    [AllowAnonymous]
    public class ConviteController : Controller
    {
        public static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly ISolicitacaoAppService _solicitacaoAppService;
        private readonly IFichaCadastralAppService _fichaCadastralAppService;
        private readonly IEnderecoAppService _enderecoAppService;
        private readonly IContatoAppService _contatoAppService;
        private readonly IBancoAppService _bancoAppService;

        public ConviteController(ISolicitacaoAppService solicitacaoDocumentoAppService,
            IFichaCadastralAppService fichaCadastralAppService,
            IEnderecoAppService enderecoAppService,
            IBancoAppService bancoAppService,
            IContatoAppService contatoAppService,
            ISolicitacaoAppService solicitacaoAppService)
        {
            _solicitacaoAppService = solicitacaoDocumentoAppService;
            _fichaCadastralAppService = fichaCadastralAppService;
            _enderecoAppService = enderecoAppService;
            _contatoAppService = contatoAppService;
            _bancoAppService = bancoAppService;

        }

        // GET: Convite
        public ActionResult Index()
        {
            ViewBag.HomeHeading = "Conheça o WebForLink, a maior comunidade de fornecedores do Brasil.";
            return View();
        }

        // GET: Convite
        [Route("Convite/{chave}")]
        public ActionResult PaginaInicial(string chave, SolicitacaoConviteVM model)
        {
            //Html.HiddenIndexerInputForModel()
            try
            {
                int id;
                if (model != null && model.Id != 0)
                    id = model.Id;
                else
                    id = _solicitacaoAppService.DescriptografarLinkConvite(chave);

                Solicitacao solicitacao = _solicitacaoAppService.Get(id);
                if (solicitacao == null)
                    return RedirectToAction("Index");

                ViewBag.HomeHeading = string.Format("Olá, {0}, seus documentos foram solicitados pelo cliente {1}.", solicitacao.Solicitado.RazaoSocial, solicitacao.Solicitante.NomeEmpresa);
                ViewBag.FutureBusinness = string.Format("A {0} solicitou sua documentação para futuros negócios: ", solicitacao.Solicitante.NomeEmpresa);
                var modelo = SolicitacaoConviteVM.ToViewModel(solicitacao);
                modelo.AdicionarIdCriptografado(chave);
                try
                {
                    _solicitacaoAppService.Visualizar(solicitacao);
                }
                catch (StatusSolicitacaoException)
                {
                    ValidarExibicaoDeFichaDaSolicitacao(modelo);
                    return View("Index", modelo);
                }
                modelo.Preenchido = false;
                modelo.EhValido = false;
                return View("Index", modelo);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return RedirectToAction("Index");
            }
        }

        private static void ValidarExibicaoDeFichaDaSolicitacao(SolicitacaoConviteVM modelo)
        {
            modelo.Preenchido = true;
        }

        [Route("Convite/_ConviteForm")]
        public ActionResult _ConviteForm(SolicitacaoConviteVM model)
        {
            try
            {
                _contatoAppService.UpdateOrCreate(ContatoVM.ToModelList(model.FichaCadastral.Contatos, model.IdFichaCadastral));
                _enderecoAppService.UpdateOrCreate(EnderecoVM.ToModelList(model.FichaCadastral.Enderecos, model.IdFichaCadastral));
                _bancoAppService.UpdateOrCreate(BancoVM.ToModelList(model.FichaCadastral.Bancos, model.IdFichaCadastral));
                var solicitacaoModel = SolicitacaoConviteVM.ToModel(model);
                var solicitacaoValidation = _solicitacaoAppService.Update(solicitacaoModel);

                if (solicitacaoValidation.EstaValidado)
                    ViewBag.BotaoColor = "green";
                else
                    ViewBag.BotaoColor = "red";
                return PartialView("_ConviteForm", model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ViewBag.BotaoColor = "red";
                return PartialView("_ConviteForm", model);
            }
        }

        [Route("Convite/GetGameListing")]
        public ActionResult GetGameListing(string chave)
        {
            try
            {
                var solicitacao = _solicitacaoAppService.BuscarFichaCadastral(chave);
                SolicitacaoConviteVM model = SolicitacaoConviteVM.ToViewModel(solicitacao);
                BasicoModal(chave, model);
                return PartialView("_ConviteForm", model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return PartialView();
            }
        }

        private void BasicoModal(string chave, SolicitacaoConviteVM model)
        {
            if (model.FichaCadastral.Enderecos.Count == 0)
                model.FichaCadastral.Enderecos.Add(new EnderecoVM());

            if (model.FichaCadastral.Bancos.Count == 0)
                model.FichaCadastral.Bancos.Add(new BancoVM());

            if (model.FichaCadastral.Contatos.Count == 0)
                model.FichaCadastral.Contatos.Add(new ContatoVM());

            model.IdCriptografado = chave;
            model.EhValido = false;
            ViewBag.BotaoColor = "gray";
        }

        [Route("Convite/AdicionarDadosEnderecos")]
        //[HttpPost]
        public ActionResult AdicionarDadosEnderecos(SolicitacaoConviteVM model)
        {
            if (model.FichaCadastral.Enderecos == null)
                model.FichaCadastral.Enderecos = new List<EnderecoVM>();
            model.FichaCadastral.Enderecos.Add(new EnderecoVM() { IdFichaCadastral = model.IdFichaCadastral });
            return PartialView("_ConviteForm", model);
        }

        [Route("Convite/AdicionarDadosContatos")]
        public ActionResult AdicionarDadosContatos(SolicitacaoConviteVM model)
        {
            if (model.FichaCadastral.Contatos == null)
                model.FichaCadastral.Contatos = new List<ContatoVM>();
            model.FichaCadastral.Contatos.Add(new ContatoVM { IdFichaCadastral = model.IdFichaCadastral });
            return PartialView("_ConviteForm", model);
        }

        [Route("Convite/AdicionarDadosBancarios")]
        public ActionResult AdicionarDadosBancarios(SolicitacaoConviteVM model)
        {
            if (model.FichaCadastral.Bancos == null)
                model.FichaCadastral.Bancos = new List<BancoVM>();
            model.FichaCadastral.Bancos.Add(new BancoVM { IdFichaCadastral = model.IdFichaCadastral });
            return PartialView("_ConviteForm", model);
        }

        [Route("Convite/GetDocumentosAnexados")]
        public ActionResult GetDocumentosAnexados(string chave)
        {
            try
            {
                int id = _solicitacaoAppService.DescriptografarLinkConvite(chave);

                Solicitacao solicitacao = _solicitacaoAppService.GetArquivos(id);

                List<ArquivoAnexadoVM> modelo = solicitacao.DocumentoSolicitacao.ToList().Select(
                    x => ArquivoAnexadoVM.ToViewModel(x)
                ).ToList();
                return PartialView("_AnexoForm", modelo);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return PartialView();
            }
        }

        [Route("Convite/_AnexoForm")]
        public ActionResult _AnexoForm(SolicitacaoConviteVM model)
        {
            var solicitacaoValidation = _solicitacaoAppService.Get(model.Id);
            return PartialView("_AnexoForm", model);
        }

        [Route("Convite/FinalizarSolicitacao")]
        public JsonResult FinalizarSolicitacao(string chave)
        {
            try
            {
                int idSolicitacao = _solicitacaoAppService.DescriptografarLinkConvite(chave);
                try
                {
                    _solicitacaoAppService.Finalizar(idSolicitacao);
                }
                catch (StatusSolicitacaoException ex)
                {
                    return new JsonError(ex.Message, JsonRequestBehavior.AllowGet);
                }
                return Json(new { msg = "Solicitação finalizada com sucesso." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return new JsonError("Erro ao tentar finalizar a solicitação.", JsonRequestBehavior.AllowGet);
        }

        [Route("Convite/Assinar")]
        public ActionResult Assinar()
        {
            try
            {
                string planoEscolhido = string.Format("REF_{0}", 1);
                PaymentRequest payment = new PaymentRequest();
                payment.Items.Add(new Item("0001", "WebForLink", 1, 2800.00m));
                payment.Currency = Currency.Brl;
                payment.Shipping = new Shipping();
                payment.Shipping.ShippingType = ShippingType.NotSpecified;
                payment.Reference = planoEscolhido;
                AccountCredentials credentials = new AccountCredentials(
                    "pagseguro@chconsultoria.com.br",
                    "86D588A7611E48FABA6125B049503F5F"
                );
                Uri paymentRedirectUri = payment.Register(credentials);
                return Redirect(paymentRedirectUri.AbsoluteUri);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return RedirectToAction("Index");
        }
        [Route("Adesao")]
        public ActionResult Adesao(string id_pagseguro)
        {
            try
            {
                string planoEscolhido = string.Format("REF_{0}", 1);
                PaymentRequest payment = new PaymentRequest();
                payment.Items.Add(new Item("0001", "WebForLink", 1, 2800.00m));
                payment.Currency = Currency.Brl;
                payment.Shipping = new Shipping();
                payment.Shipping.ShippingType = ShippingType.NotSpecified;
                payment.Reference = planoEscolhido;
                AccountCredentials credentials = new AccountCredentials(
                    "pagseguro@chconsultoria.com.br",
                    "86D588A7611E48FABA6125B049503F5F"
                );
                Uri paymentRedirectUri = payment.Register(credentials);
                return Redirect(paymentRedirectUri.AbsoluteUri);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("Convite/FirstStep")]
        public ActionResult FirstStep(SolicitacaoConviteVM model, FormCollection form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.IdFichaCadastral == 0)
                        return HttpNotFound();

                    var enderecoLista = EnderecoVM.ToModelList(model.FichaCadastral.Enderecos, model.IdFichaCadastral);
                    var enderecos = _fichaCadastralAppService.UpdateAdicionarEndereco(model.IdFichaCadastral, enderecoLista);
                    model.FichaCadastral.Enderecos = EnderecoVM.ToViewModel(enderecos);

                    model.PassoAtual = 2;
                    model.EhValido = true;
                    BasicoModal(model.IdCriptografado, model);

                    return PartialView("_ConviteForm", model);
                }
                model.EhValido = false;
                return PartialView("_ConviteForm", model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("Convite/SecondStep")]
        public ActionResult SecondStep(SolicitacaoConviteVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.IdFichaCadastral == 0)
                        return HttpNotFound();

                    var enderecoLista = ContatoVM.ToModelList(model.FichaCadastral.Contatos, model.IdFichaCadastral);
                    var enderecos = _fichaCadastralAppService.UpdateAdicionarContato(model.IdFichaCadastral, enderecoLista);
                    model.FichaCadastral.Contatos = ContatoVM.ToViewModel(enderecos);

                    model.PassoAtual = 3;
                    model.EhValido = true;
                    BasicoModal(model.IdCriptografado, model);

                    return PartialView("_ConviteForm", model);
                }
                model.EhValido = false;
                return PartialView("_ConviteForm", model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Convite/lastStep")]
        public ActionResult lastStep(SolicitacaoConviteVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Banco> bancos = BancoVM.ToModelList(model.FichaCadastral.Bancos, model.IdFichaCadastral);
                    List<Banco> bancosSalvos = _fichaCadastralAppService.UpdateAdicionarBanco(model.IdFichaCadastral, bancos);
                    model.FichaCadastral.Bancos = BancoVM.ToViewModel(bancosSalvos);

                    model.PassoAtual = 4;
                    model.EhValido = true;
                    BasicoModal(model.IdCriptografado, model);

                    return PartialView("_ConviteForm", model);
                }
                model.EhValido = false;
                return PartialView("_ConviteForm", model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return RedirectToAction("Index");
        }
    }
    public class JsonError : JsonResult
    {
        public JsonError(object obj, JsonRequestBehavior comportamento)
            : base()
        {
            Data = new { error = true, objeto = obj };
            JsonRequestBehavior = comportamento;
        }
    }
}