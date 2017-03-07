using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Controllers.Extensoes;

namespace WebForLink.Web.Controllers
{
    public class Grid<TEntity> where TEntity : class
    {
        public Grid(List<TEntity> retorno, int page = 1, int dados = 10)
        {
            TotalLinhas = dados;
            RetornoSemFiltro = retorno;
            Pager = new Pager(retorno.Count(), page, dados);
            UltimoRegistro = Pager.CurrentPage * dados;
            PrimeiroRegistro = ((Pager.CurrentPage - 1) * dados) + 1;
        }
        private List<TEntity> RetornoSemFiltro { get; set; }
        public int TotalRegistros { get { return RetornoSemFiltro.Count; } }
        public int TotalLinhas { get; private set; }
        public int PrimeiroRegistro { get; private set; }
        public int UltimoRegistro { get; private set; }
        public List<TEntity> Retorno
        {
            get
            {
                return RetornoSemFiltro
                    .Skip((Pager.CurrentPage - 1) * Pager.PageSize)
                    .Take(Pager.PageSize)
                    .ToList();
            }
        }
        public Pager Pager { get; set; }
    }

    public class GridDocumento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public string Status { get; set; }
        public DateTime Emissao { get; set; }
        public DateTime? Validade { get; set; }
        public bool Ativo { get; set; }
    }

    public class Pager
    {
        public Pager(int totalItems, int? page, int pageSize = 10)
        {
            // calculate total, start and end pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            var currentPage = page != null ? (int)page : 1;
            var startPage = currentPage - 5;
            var endPage = currentPage + 4;
            if (startPage <= 0)
            {
                endPage -= (startPage - 1);
                startPage = 1;
            }
            if (endPage > totalPages)
            {
                endPage = totalPages;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
    }

    public enum CorValidadeDocumento
    {
        Green = 1,
        Yellow = 2,
        Red = 3
    }
    public class ListaGrid
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
        public string Validade { get; set; }
        public CorValidadeDocumento ValidadeCor { get; private set; }
        public string UrlEditar { get; set; }

        public UrlHelper Url { get; private set; }

        public void Validar(UrlHelper url)
        {
            Url = url;
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            string key = "r10X310y";

            UrlEditar = Url.Action("Editar", "GestaoDocumento", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}", Id), key)
            });
        }

        public static List<ListaGrid> ModelToViewModel(IQueryable<DocumentosDoFornecedor> destinatario, UrlHelper url)
        {
            List<ListaGrid> lista = new List<ListaGrid>();
            foreach (DocumentosDoFornecedor item in destinatario)
            {
                var model = new ListaGrid
                {
                    Id = item.ID,
                    Tipo = item.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO,
                    Nome = item.DescricaoDeDocumentos.DESCRICAO,
                    Validade = item.DATA_VENCIMENTO.HasValue ? item.DATA_VENCIMENTO.Value.ToString() : "Sem Validade"
                };
                RetornarDataDeValidadeDeDocumento(item, model);

                model.Validar(url);
                lista.Add(model);
            }
            return lista;
        }

        private static void RetornarDataDeValidadeDeDocumento(DocumentosDoFornecedor item, ListaGrid model)
        {
            if (item.SEM_VALIDADE)
                model.ValidadeCor = CorValidadeDocumento.Green;
            else if (item.DATA_VENCIMENTO.HasValue)
                if (item.DATA_VENCIMENTO.Value > DateTime.Now)
                    model.ValidadeCor = CorValidadeDocumento.Red;
                else if (item.DATA_VENCIMENTO.Value > DateTime.Today.AddDays(30))
                    model.ValidadeCor = CorValidadeDocumento.Yellow;
                else
                    model.ValidadeCor = CorValidadeDocumento.Green;
        }
    }

    public class DestinatarioVM
    {
        public DestinatarioVM()
        {
            page = 1;
            registros = 10;
        }
        public int page { get; set; }
        public int registros { get; set; }
        [DisplayName("Nome")]
        public string Nome { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }
        [DisplayName("Empresa")]
        public string Empresa { get; set; }
    }
    public class CadastroDocumentoFornecedorIndividualVM
    {
        public CadastroDocumentoFornecedorIndividualVM()
        {
            DocumentosFornecedor = new List<ArquivoFornecedorIndividualVM>();
        }
        public int Id { get; set; }
        public string TipoDeDocumento { get; set; }
        public string DescricaoDeDocumentos { get; set; }
        public string DataDeEmissao { get; set; }
        public string DataDeValidade { get; set; }
        public bool SemValidade { get; set; }
        public bool Ativo { get; set; }
        public List<ArquivoFornecedorIndividualVM> DocumentosFornecedor { get; set; }
        public static CadastroDocumentoFornecedorIndividualVM ModelToViewModel(DocumentosDoFornecedor model)
        {
            CadastroDocumentoFornecedorIndividualVM viewModel = new CadastroDocumentoFornecedorIndividualVM();
            viewModel.Ativo = model.ATIVO;
            viewModel.DataDeEmissao = model.DATA_EMISSAO.HasValue ? model.DATA_EMISSAO.Value.ToString() : "" ;
            viewModel.DataDeValidade = model.DATA_VENCIMENTO.HasValue ? model.DATA_VENCIMENTO.Value.ToString() : "";
            viewModel.DescricaoDeDocumentos = model.DescricaoDeDocumentos.DESCRICAO;
            viewModel.TipoDeDocumento = model.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO;
            viewModel.SemValidade = model.SEM_VALIDADE;
            viewModel.Id = model.ID;
            viewModel.DocumentosFornecedor.Add(new ArquivoFornecedorIndividualVM { Id = 1, Nome = "Nome Teste", Tamanho = "1234 Kb", Local = "~/uanela/" });
            
            return viewModel;
        }
    }
    public class ArquivoFornecedorIndividualVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tamanho { get; set; }
        public string Local { get; set; }
    }
    public class GestaoDocumentoController : ControllerPadrao
    {
        public GestaoDocumentoController()
        {
            lista = new List<ListaGrid>();
        }
        private List<ListaGrid> lista { get; set; }
        // GET: GestaoDocumento
        [HttpGet]
        public ActionResult Index()
        {
            return View(new DestinatarioVM());
        }

        [HttpGet]
        public ActionResult Editar(string chaveurl)
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            var id = Cripto.Descriptografar(chaveurl, Key);
            var descripto = new Criptografia(EnumCripto.LinkDescriptografar, chaveurl, "r10X310y");
            var retorno = 0;
            var parametroCriptografia = descripto.Resultados.FirstOrDefault(x => x.Key == "id");
            if (parametroCriptografia.Value != null)
                int.TryParse(parametroCriptografia.Value, out retorno);
            CadastroDocumentoFornecedorIndividualVM modelo = new CadastroDocumentoFornecedorIndividualVM();
            using (WebForLinkContexto contexto = new WebForLinkContexto())
            {
                modelo = CadastroDocumentoFornecedorIndividualVM.ModelToViewModel(contexto.WFD_PJPF_DOCUMENTOS.FirstOrDefault(x => x.ID == retorno));
            }

            return View(modelo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(CadastroDocumentoFornecedorIndividualVM model)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult Excluir(string chaveurl)
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult Listar(DestinatarioVM filtro)
        {
            try
            {
                using (WebForLinkContexto contexto = new WebForLinkContexto())
                {
                    lista = ListaGrid.ModelToViewModel(contexto.WFD_PJPF_DOCUMENTOS.Where(x => x.ATIVO), Url);
                }
                return PartialView("_boxGrid", new Grid<ListaGrid>(lista, filtro.page, filtro.registros));
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return PartialView("_GridVazio");
            }
        }
    }
}