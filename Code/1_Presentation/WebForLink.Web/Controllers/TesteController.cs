using ConsultaGeolocation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.Resources;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers
{
    public class PesquisarPageBase<T> where T : class
    {
        //public T Filtros { get; set; }
        public List<T> Resultados { get; set; }
    }
    public interface IBaseVM
    {
        PesquisarPageBase<IBaseVM> Grid { get; set; }
    }
    public class Blog : IBaseVM
    {
        public Blog()
        {
            Grid = new PesquisarPageBase<IBaseVM>();
        }
        public int Id;
        public string Name;
        public string URL;
        public PesquisarPageBase<IBaseVM> Grid { get; set; }
    }
    public class Comment : IBaseVM
    {
        public Comment()
        {
            Grid = new PesquisarPageBase<IBaseVM>();
        }
        public int Id;
        public string Nome;
        public string Enunciado;
        public PesquisarPageBase<IBaseVM> Grid { get; set; }
    }
    public class TesteController : Controller
    {
        public TesteController(IContratanteFornecedorWebForLinkAppService service)
        {
            _ContratantefornecedorService = service;
        }
        private IContratanteFornecedorWebForLinkAppService _ContratantefornecedorService { get; set; }
        // GET: Teste
        public ActionResult Index()
        {
            var pagina = new Blog()
            {
                Grid = new PesquisarPageBase<IBaseVM>()
                {
                    Resultados = new List<IBaseVM>() {
                        new Blog { Name = "ScottGu", URL = "http://weblogs.asp.net/scottgu/"},
                        new Blog { Name = "Scott Hanselman", URL = "http://www.hanselman.com/blog/"},
                        new Blog { Name = "Jon Galloway", URL = "http://www.asp.net/mvc"}
                    }
                }
            };
            return View(pagina);
        }

        [AllowAnonymous]
        public ActionResult Ficha()
        {
            WFD_CONTRATANTE_PJPF fornecedor = _ContratantefornecedorService.BuscaFichaCadastralPagante(13);
            return View(FichaCadastralTesteVM.ViewModelToView(fornecedor));
        }
        [HttpPost]
        public ActionResult Ficha(FichaCadastralTesteVM model)
        {
            if (ModelState.IsValid)
            {
            }
            return View();
        }

        [AllowAnonymous]
        public ActionResult Compartilhar()
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            WFD_CONTRATANTE_PJPF fornecedor = _ContratantefornecedorService.BuscaFichaCadastralPagante(contratanteId);
            return View(FichaCadastralTesteVM.ViewModelToView(fornecedor));
        }
    }
    public interface IBoxesTesteVM { }
    public class BoxesTesteVM
    {
        public static BoxesTesteVM stringToVieModel(string modelo, UrlHelper url)
        {
            return new BoxesTesteVM
            {
                NomeBox = string.Format("Dados {0}", modelo),
                Validation = string.Format("Dados{0}Validation", modelo),
                Collapse = string.Format("dados{0}Collapse", modelo),
                Excluir = string.Format("{0}Excluir", modelo),
                Div = string.Format("div{0}", modelo),
                DivInterna = string.Format("Dados{0}", modelo),
                MensagemInclusao = string.Format("Adicionar Dados de {0}", modelo),
                Validar = string.Format("validaSalvar{0}", modelo),
                DivConfirmacao = string.Format("Confirmacao{0}", modelo),
                BotaoSim = string.Format("btnSim{0}", modelo),
                BotaoNao = string.Format("btnNao{0}", modelo),
                UrlIncluir = url.Action("Incluir", "{0}"),
                UrlCancelar = url.Action("Cancelar", "{0}")
            };
        }
        public string NomeBox { get; set; }
        public string Validation { get; set; }
        public string Collapse { get; set; }
        public string Excluir { get; set; }
        public string Div { get; set; }
        public string DivInterna { get; set; }
        public string UrlIncluir { get; set; }
        public string MensagemInclusao { get; set; }
        public string UrlCancelar { get; set; }
        public string Validar { get; set; }
        public string DivConfirmacao { get; set; }
        public string BotaoSim { get; set; }
        public string BotaoNao { get; set; }
    }
}