using AutoMapper;
using Ninject;
using System;
using System.Web.Mvc;
using Uol.PagSeguro.Constants;
using Uol.PagSeguro.Domain;
using Uol.PagSeguro.Service;
using WebForLink.Application.Interfaces;
using WebForLink.CrossCutting.InversionControl;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Services;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers
{
    [AllowAnonymous]
    public class PlanoController : Controller
    {
        private readonly IConviteAppService _conviteAppService;

        public PlanoController(IConviteAppService conviteAppService)
        {
            _conviteAppService = conviteAppService;
        }

        //public PlanoController()
        //{
        //    var ioc = new IoC();
        //    _conviteAppService = ioc.Kernel.Get<IConviteAppService>();
        //}
        // GET: Plano
        /// <summary>
        ///     Quando um fornecedor NÃO vier convidado do VendorList, será direcionado para essa página.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///     Quando um fornecedor vier convidado do VendorList, será direcionado para essa página.
        /// </summary>
        /// <returns></returns>
        //[ActionName("Convite")]
        //public ActionResult Index(string chaveUrl)
        //{
        //    try
        //    {
        //        int id = _conviteAppService.DescriptografarLinkConvite(chaveUrl);
        //        Categoria conviteModel = _conviteAppService.BuscarConvite(id);
        //        ConviteVM model = ConviteVM.ToViewModel(conviteModel);
        //        //TODO: Gravar dados no banco de fornecedores convidados pelo VendorList
        //        return View("Index", model);
        //    }
        //    catch (Exception ex)
        //    {
        //        return HttpNotFound();
        //    }
        //}

        public ActionResult PagSeguro()
        {
            Criptografia descripto = new Criptografia(EnumCripto.Criptografar, "Pln=1&Sol=12346", "r10X310y");
            string planoEscolhido = string.Format("REF_{0}", descripto.Resultado);
            PaymentRequest payment = new PaymentRequest();
            payment.Items.Add(new Item("0001", "Plano_1", 1, 2430.00m));
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
            //return View();
        }

        public ActionResult StatusCompra(string id_pagseguro)
        {
            id_pagseguro = "C58740AE-DF00-4732-AA48-DBDFB6A3122E";

            // Inicializando credenciais  
            AccountCredentials credentials = new AccountCredentials(
                "pagseguro@chconsultoria.com.br",
                "86D588A7611E48FABA6125B049503F5F"
            );

            // Código identificador da transação    
            string transactionCode = id_pagseguro;

            // Realizando uma consulta de transação a partir do código identificador   
            // para obter o objeto Transaction  
            Transaction transaction = TransactionSearchService.SearchByCode(
                credentials,
                transactionCode
            );
            int status = transaction.TransactionStatus;
            //1   Aguardando pagamento: o comprador iniciou a transação, mas até o momento o PagSeguro não recebeu nenhuma informação sobre o pagamento.WaitingPayment
            //2   Em análise: o comprador optou por pagar com um cartão de crédito e o PagSeguro está analisando o risco da transação.InAnalysis
            //3   Paga: a transação foi paga pelo comprador e o PagSeguro já recebeu uma confirmação da instituição financeira responsável pelo processamento.Paid
            //4   Disponível: a transação foi paga e chegou ao final de seu prazo de liberação sem ter sido retornada e sem que haja nenhuma disputa aberta.	Available
            //5   Em disputa: o comprador, dentro do prazo de liberação da transação, abriu uma disputa.	InDispute
            //6   Devolvida: o valor da transação foi devolvido para o comprador.Refunded
            //7   Cancelada: a transação foi cancelada sem ter sido finalizada.	Cancelled


            return View();
        }
        public ActionResult Transacao(string id_pagseguro)
        {
            return View();
        }

    }
}
namespace WebForLink.Web.ViewModels
{
    public class ConviteVM
    {
        public ConviteVM(ParametroCriptografia criptoCnpj, ParametroCriptografia criptoCliente)
        {
            Cnpj = criptoCnpj != null ? criptoCnpj.Value : "";
            Cliente = criptoCnpj != null ? criptoCnpj.Value : "";
        }

        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string Cliente { get; set; }
        public static ConviteVM ToViewModel(Categoria convite)
        {
            return Mapper.Map<ConviteVM>(convite);
        }
    }
    //    public class Pagamento
    //{
    //    public Pagamento(OrdemPagamento ordem);

    //    public string CpfResponsavel { get; set; }
    //    public string DddResponsavel { get; set; }
    //    public string EmailResponsavel { get; set; }
    //    public string NomeResponsavel { get; set; }
    //    public global::Uol.PagSeguro.Domain.PaymentRequest Payment { get; set; }
    //    public string ReferenciaCompra { get; set; }
    //    public string TelefoneResponsavel { get; set; }
    //    public string UrlRetorno { get; set; }

    //    public global::Uol.PagSeguro.Domain.Sender Comprador();
    //    public global::Uol.PagSeguro.Domain.AccountCredentials Conta();
    //    public void EfetuarPagamento(TipoPlano tipoPlano);
    //}
}