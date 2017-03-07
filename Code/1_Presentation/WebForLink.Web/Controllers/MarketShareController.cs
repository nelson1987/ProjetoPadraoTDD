using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebForLink.Web.ViewModels.MarketShare;

namespace WebForLink.Web.Controllers
{
    public class MarketShareController : Controller
    {
        //public static ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //private readonly ISolicitacaoAppService _solicitacaoAppService;
        //private readonly IFichaCadastralAppService _fichaCadastralAppService;
        //private readonly IEnderecoAppService _enderecoAppService;
        //private readonly IContatoAppService _contatoAppService;
        //private readonly IBancoAppService _bancoAppService;

        //public ConviteController(ISolicitacaoAppService solicitacaoDocumentoAppService,
        //    IFichaCadastralAppService fichaCadastralAppService,
        //    IEnderecoAppService enderecoAppService,
        //    IBancoAppService bancoAppService,
        //    IContatoAppService contatoAppService,
        //    ISolicitacaoAppService solicitacaoAppService)
        //{
        //    _solicitacaoAppService = solicitacaoDocumentoAppService;
        //    _fichaCadastralAppService = fichaCadastralAppService;
        //    _enderecoAppService = enderecoAppService;
        //    _contatoAppService = contatoAppService;
        //    _bancoAppService = bancoAppService;

        //}
        // GET: MarketShare
        public ActionResult Index()
        {
            var samarco = new ClienteMarketShareVM { Nome = "SAMARCO", Sigla = "SMC" };
            var cutrale = new ClienteMarketShareVM { Nome = "CUTRALE", Sigla = "CTL" };
            var leao = new ClienteMarketShareVM { Nome = "LEÃO", Sigla = "GLO" };
            var andina = new ClienteMarketShareVM { Nome = "ANDINA", Sigla = "AND" };

            var rioDeJaneiro = new EstadoMarketShareVM { UF = "RJ", Nome = "Rio de Janeiro" };
            var minasGerais = new EstadoMarketShareVM { UF = "MG", Nome = "Minas Gerais" };
            var bahia = new EstadoMarketShareVM { UF = "MG", Nome = "Bahia" };
            var para = new EstadoMarketShareVM { UF = "PA", Nome = "Pará" };

            var padariaSensacao = new FornecedorMarketShareVM
            {
                Id = 1,
                Tipo = 1,
                Clientes = new List<ClienteMarketShareVM> { cutrale, leao },
                Estados = new List<EstadoMarketShareVM> {  rioDeJaneiro }
            };
            var supermercadoGuanabara = new FornecedorMarketShareVM
            {
                Id = 2,
                Tipo = 1,
                Clientes = new List<ClienteMarketShareVM> { leao, andina },
                Estados = new List<EstadoMarketShareVM> { rioDeJaneiro }
            };
            var valeRioDoce = new FornecedorMarketShareVM
            {
                Id = 3,
                Tipo = 1,
                Clientes = new List<ClienteMarketShareVM> { samarco },
                Estados = new List<EstadoMarketShareVM> { rioDeJaneiro, minasGerais, bahia, para }
            };

            var listaFornecedores = new List<FornecedorMarketShareVM>()
            {
                supermercadoGuanabara,
                valeRioDoce
            };
            return View(/*new ComparadorMarketShareVM<FornecedorMarketShareVM>(padariaSensacao, listaFornecedores)*/);
        }
    }
}