using ExpressiveAnnotations.Attributes;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Web.Controllers.NovaFichaCadastral;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.Controllers
{
    public class NovaFichaCadastralVM //: IValidatableObject
    {
        public NovaFichaCadastralVM()
        {
            DadosGerais = new NovoDadosGeraisVM();
            DadosEndereco = new List<NovoDadosEnderecoVM>();
            DadosContato = new List<NovoDadosContatoVM>();
            DadosServico = new List<NovoDadosServicoVM>();
            DadosMaterial = new List<NovoDadosMaterialVM>();
            DadosComplementares = new List<NovoDadosComplementaresVM>();
        }

        public int UsuarioId { get; set; }
        public int ContratanteId { get; set; }
        public int FornecedorId { get; set; }

        [RequiredIf("UsuarioId == 0", ErrorMessage = "UsuarioId não pode ser nulo")]
        public string Teste { get; set; }

        public NovoDadosGeraisVM DadosGerais { get; set; }
        public NovoReceitaCnpjVM ReceitaCnpj { get; set; }
        public NovoReceitaSintegraVM Sintegra { get; set; }
        public NovoReceitaSimplesVM Simples { get; set; }
        public List<NovoDadosEnderecoVM> DadosEndereco { get; set; }
        public List<NovoDadosContatoVM> DadosContato { get; set; }
        public List<NovoDadosServicoVM> DadosServico { get; set; }
        public List<NovoDadosMaterialVM> DadosMaterial { get; set; }
        public List<NovoDadosComplementaresVM> DadosComplementares { get; set; }

        public bool VisualizarBotaoValidarOrgaosPublicos { get; private set; }
        public bool VisualizarBotaoAdicionarEnderecos { get; private set; }
        public bool VisualizarBotaoAdicionarBancos { get; private set; }
        public bool VisualizarBotaoAdicionarComprovanteBancos { get; private set; }
        public bool VisualizarBotaoAdicionarContatos { get; private set; }
        public bool VisualizarBotaoSalvarRascunho { get; private set; }
        public bool VisualizarBotaoSalvar { get; private set; }
        //public bool VisualizarBotaoExcluirEnderecos { get; private set; }
        //public bool VisualizarBotaoExcluirBancos { get; private set; }
        //public bool VisualizarBotaoExcluirComprovanteBancos { get; private set; }
        //public bool VisualizarBotaoExcluirContatos { get; private set; }
        public bool AdicionarEnderecos { get; private set; }
        public bool AdicionarBancos { get; private set; }
        public bool AdicionarContatos { get; private set; }

        public void ValidarComandosDisponiveisTela()
        {
            VisualizarBotaoValidarOrgaosPublicos = false;
            VisualizarBotaoAdicionarEnderecos = false;
            VisualizarBotaoAdicionarBancos = false;
            VisualizarBotaoAdicionarComprovanteBancos = false;
            VisualizarBotaoAdicionarContatos = false;
            VisualizarBotaoSalvarRascunho = false;
            VisualizarBotaoSalvar = false;

            //VisualizarBotaoExcluirEnderecos = false;
            //VisualizarBotaoExcluirBancos = false;
            //VisualizarBotaoExcluirComprovanteBancos = false;
            //VisualizarBotaoExcluirContatos = false;

            //ObrigatoriedadeIncluirEnderecos = false;
            //ObrigatoriedadeIncluirBancos = false;
            //ObrigatoriedadeIncluirComprovanteBancos = false;
            //ObrigatoriedadeIncluirContatos = false;

            AdicionarEnderecos = false;
            AdicionarBancos = false;
            AdicionarContatos = false;
        }

        public static NovaFichaCadastralVM ModelToViewModel(WFD_CONTRATANTE_PJPF ficha)
        {
            var model = new NovaFichaCadastralVM();
            model.ContratanteId = ficha.CONTRATANTE_ID;
            model.FornecedorId = ficha.PJPF_ID;
            model.DadosEndereco.AddRange(ficha.WFD_PJPF_ENDERECO.Select(x => new NovoDadosEnderecoVM
            {
                Endereco = x.ENDERECO,
                Cidade = x.CIDADE
            }).ToList());

            model.DadosEndereco.Add(new NovoDadosEnderecoVM { Pais = "Brasil" });
            return model;
        }
        //public IEnumerable<ValidationResult> Validate(ValidationContext context)
        //{
        //    var validation = new List<ValidationResult>();
        //    if (AdicionarEnderecos && !DadosEndereco.Any())
        //    {
        //        validation.Add(new ValidationResult("Surname is required unless the party is for an organization");
        //    }
        //    return validation;
        //}
    }

    public class NovoReceitaCnpjVM
    {

    }
    public class NovoReceitaSintegraVM
    {

    }
    public class NovoReceitaSimplesVM
    {

    }
    
    public class NovoDadosBancariosVM { }
    public class NovoDadosContatoVM { }
    public class NovoDadosServicoVM { }
    public class NovoDadosMaterialVM { }
    public class NovoDadosComplementaresVM { }

    [AllowAnonymous]
    public class FichaCadastralController : BaseController
    {
        private readonly IContratanteFornecedorWebForLinkAppService _service;

        public FichaCadastralController(IContratanteFornecedorWebForLinkAppService service)
        {
            _service = service;
        }

        // GET: FichaCadastral
        public ActionResult Ficha(string chaveUrl)
        {
            if (string.IsNullOrEmpty(chaveUrl))
                return HttpNotFound();

            var ficha = _service.Get(x => x.ID == 70
                //x.CONTRATANTE_ID == model.ContratanteId &&
                //x.PJPF_ID == model.FornecedorId
                );

            return View(NovaFichaCadastralVM.ModelToViewModel(ficha));
        }
        // GET: FichaCadastral
        [HttpPost]
        public ActionResult Ficha(NovaFichaCadastralVM model)
        {
            return View(model);
        }
    }
}