using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Interfaces;
using WebForLink.Web.ViewModels.Adesao;

namespace WebForLink.Web.Controllers
{
    [AllowAnonymous]
    public class AdesaoController : ControllerPadrao, IAdesaoController
    {
        public IAdesaoWebForLinkAppService _adesao;
        public AdesaoController(IAdesaoWebForLinkAppService adesao)
        {
            _adesao = adesao;
        }
        // GET: Adesao
        public ActionResult Index()
        {
            AdesaoCriacaoVM modelo = new AdesaoCriacaoVM();
            List<AdesaoPropriedadePlanoVM> listStr1 = new List<AdesaoPropriedadePlanoVM>
            {
            new AdesaoPropriedadePlanoVM(true, "Guardar Documentos Digitalizados"),
            new AdesaoPropriedadePlanoVM(true, "Distribuir Documentos aos Clientes"),
            new AdesaoPropriedadePlanoVM(true, "Controlar Validade dos Documentos"),
            new AdesaoPropriedadePlanoVM(true, "Enviar Ficha Cadastral Padrão aos Clientes"),
            new AdesaoPropriedadePlanoVM(false,"Atualizar de Dados Cadastrais Governo (Receita Federal, Sintegra, Simples Nacional,...)"),
            new AdesaoPropriedadePlanoVM(false,"Destacar seus produtos no Vendor List WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(false,"Acessar Estatísticas do Portal"),
            new AdesaoPropriedadePlanoVM(false,"Incluir de Produtos no Portal WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(false, "Publicar de Catálogos de Produtos para Clientes WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(false, "Publicar de Ofertas no Portal WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(false, "Receber Cotação dos Clientes do Portal WEBFORMAT")
            };
            List<AdesaoPropriedadePlanoVM> listStr2 = new List<AdesaoPropriedadePlanoVM>
            {
            new AdesaoPropriedadePlanoVM(true,"Guardar Documentos Digitalizados"),
            new AdesaoPropriedadePlanoVM(true,"Distribuir Documentos aos Clientes"),
            new AdesaoPropriedadePlanoVM(true,"Controlar Validade dos Documentos"),
            new AdesaoPropriedadePlanoVM(true,"Enviar Ficha Cadastral Padrão aos Clientes"),
            new AdesaoPropriedadePlanoVM(true,"Atualizar de Dados Cadastrais Governo (Receita Federal, Sintegra, Simples Nacional,...)"),
            new AdesaoPropriedadePlanoVM(true,"Destacar seus produtos no Vendor List WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(true,"Acessar Estatísticas do Portal"),
            new AdesaoPropriedadePlanoVM(true,"Incluir de Produtos no Portal WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(false, "Publicar de Catálogos de Produtos para Clientes WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(false, "Publicar de Ofertas no Portal WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(false, "Receber Cotação dos Clientes do Portal WEBFORMAT")
            };
            List<AdesaoPropriedadePlanoVM> listStr3 = new List<AdesaoPropriedadePlanoVM>
            {
            new AdesaoPropriedadePlanoVM(true, "Guardar Documentos Digitalizados"),
            new AdesaoPropriedadePlanoVM(true, "Distribuir Documentos aos Clientes"),
            new AdesaoPropriedadePlanoVM(true, "Controlar Validade dos Documentos"),
            new AdesaoPropriedadePlanoVM(true, "Enviar Ficha Cadastral Padrão aos Clientes"),
            new AdesaoPropriedadePlanoVM(true, "Atualizar de Dados Cadastrais Governo (Receita Federal, Sintegra, Simples Nacional,...)"),
            new AdesaoPropriedadePlanoVM(true, "Destacar seus produtos no Vendor List WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(true, "Acessar Estatísticas do Portal"),
            new AdesaoPropriedadePlanoVM(true, "Incluir de Produtos no Portal WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(true, "Publicar de Catálogos de Produtos para Clientes WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(false, "Publicar de Ofertas no Portal WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(false, "Receber Cotação dos Clientes do Portal WEBFORMAT")
            };
            List<AdesaoPropriedadePlanoVM> listStr4 = new List<AdesaoPropriedadePlanoVM>
            {
            new AdesaoPropriedadePlanoVM(true, "Guarde Documentos Digitalizados"),
            new AdesaoPropriedadePlanoVM(true, "Distribua Documentos aos Clientes"),
            new AdesaoPropriedadePlanoVM(true, "Controle Validade dos Documentos"),
            new AdesaoPropriedadePlanoVM(true, "Envie Ficha Cadastral aos Clientes"),
            new AdesaoPropriedadePlanoVM(true, "Dados Cadastrais Governo (Receita Federal, Sintegra, Simples Nacional,...)"),
            new AdesaoPropriedadePlanoVM(true, "Destaque produtos no Vendor List WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(true, "Estatísticas do Portal"),
            new AdesaoPropriedadePlanoVM(true, "Inclua Produtos no Portal WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(true, "Publique Catálogos de Produtos para Clientes WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(true, "Publique Ofertas no Portal WEBFORMAT"),
            new AdesaoPropriedadePlanoVM(true, "Cotações dos Clientes do Portal WEBFORMAT")
            };

            modelo.Planos = new List<AdesaoPlanoVM>()
            {
                new AdesaoPlanoVM(1,"I",  1,  1,  10,  14.99, Key, Request.Url.Scheme, Url,listStr1),
                new AdesaoPlanoVM(2,"II", 5,  1,  50,  19.99, Key, Request.Url.Scheme, Url,listStr2),
                new AdesaoPlanoVM(3,"III",10, 2, 100, 29.99, Key, Request.Url.Scheme, Url,listStr3),
                new AdesaoPlanoVM(4,"IV", 10, 2, 100, 29.99, Key, Request.Url.Scheme, Url,listStr4),
            };
            return View(modelo);
        }
        // GET: Adesao
        [HttpPost]
        public ActionResult Index(AdesaoCriacaoVM modelo)
        {
            return View(modelo);
        }
        // GET: Adesao
        public PartialViewResult CriarAdesao(string chaveUrl)
        {
            int planoId = 0;
            if (!string.IsNullOrEmpty(chaveUrl))
            {
                List<Param> param = Cripto.ReadUrl(chaveUrl, Key);
                Int32.TryParse(param.First(p => p.Name == "planoId").Value, out planoId);
            }
            AdesaoCriacaoFormularioVM modelo = new AdesaoCriacaoFormularioVM();
            modelo.PlanoId = planoId;
            modelo.UsuarioSelecionado = planoId;
            modelo.EspacoSelecionado = planoId;
            modelo.MediaEspaçoDocumentos = planoId != 3
                ? "200"
                : "400";
            modelo.EspacoList = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "1 Gb"},
                new SelectListItem { Value = "2", Text = "10 Gb"},
                new SelectListItem { Value = "3", Text = "20 Gb"}
            };
            modelo.UsuarioList = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "1 usuário"},
                new SelectListItem { Value = "2", Text = "1 a 5 usuários"},
                new SelectListItem { Value = "3", Text = "5 a 10 usuários"},
                new SelectListItem { Value = "4", Text = "10 a 50 usuários"}
            };
            modelo.Total = "R$ 0,00";

            int[] planosAssociado = { planoId, planoId };
            modelo.CalcularTotal(planosAssociado);

            return PartialView("_AdesaoForm", modelo);
        }
        // GET: Adesao
        [HttpPost]
        public ActionResult CriarAdesao(AdesaoCriacaoFormularioVM modelo)
        {
            modelo.EspacoList = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "1 Gb"},
                new SelectListItem { Value = "2", Text = "10 Gb"},
                new SelectListItem { Value = "3", Text = "20 Gb"}
            };
            modelo.UsuarioList = new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "1 usuário"},
                new SelectListItem { Value = "2", Text = "1 a 5 usuários"},
                new SelectListItem { Value = "3", Text = "5 a 10 usuários"},
                new SelectListItem { Value = "4", Text = "10 a 50 usuários"}
            };
            return PartialView("_AdesaoForm", modelo);
        }
        // GET: Adesao
        public ActionResult PreCadastro(string chaveUrl)
        {
            Log.Info("[GET] Adesao/PreCadastro");
            Log.Warn("[GET] Adesao/PreCadastro");
            Log.Error("[GET] Adesao/PreCadastro");
            int planoId = 0;
            if (!string.IsNullOrEmpty(chaveUrl))
            {
                List<Param> param = Cripto.ReadUrl(chaveUrl, Key);
                Int32.TryParse(param.First(p => p.Name == "planoId").Value, out planoId);
            }
            PreCadastroAdesaoVM modelo = new PreCadastroAdesaoVM()
            {
                PlanoEscolhido = planoId
            };

            return View(modelo);
        }
        [HttpPost]
        public ActionResult PreCadastro(PreCadastroAdesaoVM modelo)
        {
            Log.Info("[POST] Adesao/PreCadastro");
            Log.Warn("[POST] Adesao/PreCadastro");
            Log.Error("[POST] Adesao/PreCadastro");
            if (ModelState.IsValid)
            {
                try
                {
                    OrdemPagamento pagamento = Mapper.Map<OrdemPagamento>(modelo);
                    return Redirect(_adesao.CriarNovaAdesao(pagamento));
                }
                catch (AutoMapperMappingException ex)
                {
                    Log.Error(ex);
                    //ModelState.AddModelError("Erro ao tentar criar seu plano. Tente novamente mais tarde.", ex.Message);
                    //ModelState.AddModelError("Mensagem", "Erro ao tentar criar seu plano. Tente novamente mais tarde.");
                    bool envioEmail = _metodosGerais.EnviarEmail("carlos.jesus@chconsultoria.com.br", "AutoMapperMappingException WFL", string.Format("{0}<br/>{1}", ex.Message, ex));
                    ModelState.AddModelError("Mensagem", ex);
                }
                //catch (ServiceWebForLinkException ex)
                //{
                //    Log.Error(ex);
                //    //ModelState.AddModelError("Erro ao tentar criar seu plano. Tente novamente mais tarde.", ex.Message);
                //    //ModelState.AddModelError("Mensagem", "Erro ao tentar criar seu plano. Tente novamente mais tarde.");
                //    bool envioEmail = _metodosGerais.EnviarEmail("carlos.jesus@chconsultoria.com.br", "WFLBusinessException WFL", string.Format("{0}<br/>{1}", ex.Message, ex));
                //    ModelState.AddModelError("Mensagem", ex);
                //}
                catch (Exception ex)
                {
                    Log.Error(ex);
                    //ModelState.AddModelError("Mensagem", "Erro ao tentar criar seu plano. Tente novamente mais tarde.");
                    bool envioEmail = _metodosGerais.EnviarEmail("carlos.jesus@chconsultoria.com.br", "Exception WFL", string.Format("{0}<br/>{1}",ex.Message,ex));
                    ModelState.AddModelError("Mensagem", ex);
                }
            }
            Log.Error("[POST] Adesao/PreCadastro = !ModelState.IsValid");
            return View(modelo);
        }
        [HttpPost]
        public PartialViewResult ValidateUserNameRoute(PreCadastroAdesaoEmpressaVM Empresa)
        {
            return PartialView("_PreCadastro_Empresa", Empresa);
        }

        [HttpPost]
        public ActionResult CriarUsuarios(string chaveUrl)
        {
            try
            {
                //var objetonovo;

                //using (var BpAdesao = new AdesaoBP())
                //{
                //    urlRedirecionaPagSeguro = BpAdesao.ConfirmarAdesao (pagamento);
                //}
            }
            catch (Exception ex)
            {
                Log.Error("Erro ao tentar Criar Usuários", ex);
            }
            return View();
        }
    }
}