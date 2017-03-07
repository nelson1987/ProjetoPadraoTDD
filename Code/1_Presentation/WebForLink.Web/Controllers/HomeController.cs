using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Services;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : ControllerPadrao
    {
        #region Chamadas Para BP
        private readonly IUsuarioWebForLinkAppService _usuarioService;
        private readonly IPapelWebForLinkAppService _papelService;
        private readonly IContratanteWebForLinkAppService _contratanteService;
        private readonly IPerfilWebForLinkAppService _perfilService;
        private readonly IFornecedorWebForLinkAppService _pjPfService;
        private readonly IFornecedorContatoWebForLinkAppService _fornecedorContatosService;
        private readonly IUsuarioSenhaHistoricoWebForLinkAppService _usuarioSenhaHistoricoService;
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoService;
        private readonly ISolicitacaoCadastroFornecedorWebForLinkAppService _solicitacaoCadastroFornecedorService;
        private readonly IContratanteConfiguracaoWebForLinkAppService _contratanteConfiguracaoService;
        private readonly IFornecedorBaseWebForLinkAppService _fornecedorBaseService;
        private readonly IConfiguracaoWebForLinkAppService _configuracaoService;
        private readonly IAcessoLogWebForLinkAppService _acessoLogService;
        private readonly IProcessoLoginWebForLinkAppService _processoLoginService;

        #endregion

        #region Constructor
        public HomeController(
            IUsuarioWebForLinkAppService usuario,
        IPapelWebForLinkAppService papel,
        IContratanteWebForLinkAppService contratante,
        IPerfilWebForLinkAppService perfil,
        IFornecedorWebForLinkAppService pjPf,
        IFornecedorContatoWebForLinkAppService pjPfContatos,
        IUsuarioSenhaHistoricoWebForLinkAppService usuarioSenhaHistorico,
        ISolicitacaoWebForLinkAppService solicitacao,
        ISolicitacaoCadastroFornecedorWebForLinkAppService solicitacaoCadastroPjpf,
        IContratanteConfiguracaoWebForLinkAppService contratanteConfiguracao,
        IFornecedorBaseWebForLinkAppService pjPfBase,
        IConfiguracaoWebForLinkAppService configuracaoWebForLink,
        IAcessoLogWebForLinkAppService acesso,
        IProcessoLoginWebForLinkAppService processoLogin)
        {
            _usuarioService = usuario;
            _papelService = papel;
            _contratanteService = contratante;
            _perfilService = perfil;
            _pjPfService = pjPf;
            _fornecedorContatosService = pjPfContatos;
            _usuarioSenhaHistoricoService = usuarioSenhaHistorico;
            _solicitacaoService = solicitacao;
            _solicitacaoCadastroFornecedorService = solicitacaoCadastroPjpf;
            _contratanteConfiguracaoService = contratanteConfiguracao;
            _fornecedorBaseService = pjPfBase;
            _configuracaoService = configuracaoWebForLink;
            _acessoLogService = acesso;
            _processoLoginService = processoLogin;
        }
        #endregion

        #region Index
        public ActionResult Index(string ReturnUrl, string chaveurl)
        {
            Session.Abandon();
            AcessoVM acesso = new AcessoVM();

            if (chaveurl != null)
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);

                int travaLogin = 0;
                Int32.TryParse(param.First(p => p.Name == "TravaLogin").Value, out travaLogin);
                string login = param.First(p => p.Name == "Login").Value;

                acesso.TravaLogin = travaLogin;
                acesso.Login = login;

                ViewBag.ExibeModalAcesso = true;
            }
            else
            {
                ViewBag.ExibeModalAcesso = false;
            }

            acesso.ReturnUrl = ReturnUrl;
            acesso.chaveUrl = chaveurl;
            return View(acesso);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(AcessoVM model)
        {
            try
            {
                if (model.chaveUrl != null)
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(model.chaveUrl, Key);
                    int idSolicitacao;
                    int travaLogin = 0;
                    Int32.TryParse(param.First(p => p.Name == "SolicitacaoID").Value, out idSolicitacao);
                    Int32.TryParse(param.First(p => p.Name == "TravaLogin").Value, out travaLogin);
                    string login = param.First(p => p.Name == "Login").Value;

                    model.SolicitacaoId = idSolicitacao;
                    model.TravaLogin = travaLogin;
                    model.Login = login;
                }
                Usuario usuarioLogado = _usuarioService.BuscarPorLogin(model.Login);
                //Valida Usuário Existe
                if (usuarioLogado == null)
                {
                    Log.Info("Usuário inválido!");
                    FinalizarAcesso();
                    ModelState.AddModelError("", "Usuário inválido!");
                }

                if (ModelState.IsValid)
                {
                    ViewBag.ExibeModalAcesso = true;
                    ValidarBloqueioTempo(usuarioLogado);

                    if (Acessar(model.Login, model.Senha))
                    {
                        _usuarioService.ZerarTentativasLogin(usuarioLogado);
                        if (usuarioLogado.CONTRATANTE_ID == null) //Se for Fornecedor vá pra essa página
                        {
                            if (model.SolicitacaoId == null)
                            {
                                int pjpf = _pjPfService.BuscarIdFornecedorPorCnpj(model.Login);

                                if (pjpf == 0)
                                    model.SolicitacaoId = _solicitacaoCadastroFornecedorService.BuscarIdSolicitacaoPorCnpj(model.Login);
                                else
                                    model.FornecedorId = pjpf;
                            }
                            return Redirect(Url.Action("FichaCadastral", "Documento", new
                            {
                                chaveurl = Cripto.Criptografar(string.Format("SolicitacaoID={0}&FornecedorID={1}&ContratanteID=0", model.SolicitacaoId, model.FornecedorId), Key)
                            }, Request.Url.Scheme));
                        }
                        if (usuarioLogado != null && usuarioLogado.PRIMEIRO_ACESSO == true)
                            return ExecutarPrimeiroAcesso(usuarioLogado);

                        if (Convert.ToBoolean(ConfigurationManager.AppSettings["GravaLogAcesso"]))
                            _acessoLogService.GravarLogAcesso(usuarioLogado.ID, PegaIPAcesso(), PegaNavegadorAcesso());

                        if (string.IsNullOrEmpty(model.ReturnUrl))
                            return RedirectToAction("Index", "HomeAdmin");
                        else
                        {
                            string[] url = model.ReturnUrl.Split(new char[] { '/' });
                            if (url.Length == 2)
                                return RedirectToAction(url[2], url[1]);
                            else
                                return RedirectToAction("Index", "HomeAdmin");
                        }
                    }
                    ViewBag.ExibeModalAcesso = false;

                    var ativo = usuarioLogado != null && (usuarioLogado.ATIVO != false);
                    if (ativo)
                    {
                        _usuarioService.ContabilizarErroLogin(usuarioLogado);
                        ModelState.AddModelError("", "Login ou Senha Inválido!");
                        ViewBag.ExibeModalAcesso = true;
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuário bloqueado!");
                        ViewBag.ExibeModalAcesso = true;
                    }
                }
                else
                {
                    ViewBag.ExibeModalAcesso = true;
                }

                return View(model);
            }
            catch (EntityException ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", "Ocorreu um erro de conexão.");
                FinalizarAcesso();
                ViewBag.ExibeModalAcesso = true;
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", ex.Message);
                FinalizarAcesso();
                ViewBag.ExibeModalAcesso = true;
                return View(model);
            }
        }
        #endregion

        #region Criar Novo Usuário
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CriarUsuario()
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            UsuarioVM modelo = new UsuarioVM
            {
                ListaContratantes = Mapper.Map<List<SelectListItem>>(_contratanteService.ListarTodosPorGrupo(contratanteId)),
                ListaPerfis = Mapper.Map<List<SelectListItem>>(_perfilService.ListarTodosPorContratante(contratanteId)),
                ListaPapeis = Mapper.Map<List<SelectListItem>>(_papelService.ListarTodos(contratanteId)),
            };
            return View(modelo);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CriarUsuario(UsuarioVM model)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            model.ListaContratantes = Mapper.Map<List<SelectListItem>>(_contratanteService.ListarTodosPorGrupo(contratanteId));
            model.ListaPerfis = Mapper.Map<List<SelectListItem>>(_perfilService.ListarTodosPorContratante(contratanteId));
            model.ListaPapeis = Mapper.Map<List<SelectListItem>>(_papelService.ListarTodos(contratanteId));
            try
            {
                ValidarPoliticaSenha(model);
                if (ModelState.IsValid)
                {
                    var usuarioMapeado = Mapper.Map<Usuario>(model);
                    var senhaMapeada = Mapper.Map<USUARIO_SENHAS>(usuarioMapeado);
                    _usuarioService.IncluirUsuarioPadraoSenha(usuarioMapeado, senhaMapeada, model.IdPapel, model.IdPerfil);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return View(model);
        }
        #endregion

        #region CadastrarUsuario
        [HttpGet]
        public ActionResult CadastrarFornecedorIndividual(int id)
        {
            //84422763000110
            return RedirectToAction("CadastrarUsuario", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id=0&tipocadastro=2&cnpj={0}&idContratante={1}", "84422763000110", 6), Key)
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chaveurl"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult CadastrarUsuario(string chaveurl)
        {
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);

            // VERIFICA SE OS PARAMETROS FORAM MONTADOS CORRETAMENTE
            if (param.Count == 0)
            {
                ViewBag.DisplayForm = false;
                ViewBag.DisplaySucesso = false;
                ViewBag.DisplayAlerta = true;
                return View();
            }

            int id;
            int tipoCadastro;
            Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
            Int32.TryParse(param.First(p => p.Name == "tipocadastro").Value, out tipoCadastro);

            UsuarioVM acesso = new UsuarioVM();
            if ((EnumTipoCadastroNovoUsuario)tipoCadastro == EnumTipoCadastroNovoUsuario.Cadastrado)
            {
                Fornecedor fornecedor = _pjPfService.BuscarPorId(id);
                FORNECEDOR_CONTATOS contato = _fornecedorContatosService.BuscarPorContratantePJPFId(fornecedor.ID);

                //VERIFICA SE O Usuario EXISTE
                if (fornecedor == null)
                {
                    ViewBag.DisplayForm = false;
                    ViewBag.DisplaySucesso = false;
                    ViewBag.DisplayAlerta = true;
                    return View();
                }
                if (contato == null)
                {
                    ViewBag.DisplayForm = false;
                    ViewBag.DisplaySucesso = false;
                    ViewBag.DisplayAlerta = true;
                    return View();
                }
                acesso.ID = fornecedor.ID;
                acesso.Cargo = "Fornecedor";
                acesso.CPF = fornecedor.CNPJ;
                acesso.Nome = fornecedor.RAZAO_SOCIAL;
                acesso.Email = contato.EMAIL;
                acesso.IdContratante = fornecedor.CONTRATANTE_ID;
                acesso.TextoTermoAceite = fornecedor.Contratante.WFD_CONTRATANTE_CONFIG.TERMO_ACEITE;
                acesso.TipoCadastroNovoUsuario = (EnumTipoCadastroNovoUsuario)tipoCadastro;
            }
            else
            {
                string cnpj = param.First(p => p.Name == "cnpj").Value;

                int idContratante;
                Int32.TryParse(param.First(p => p.Name == "idContratante").Value, out idContratante);

                acesso.ID = 0;
                acesso.Cargo = "Fornecedor";
                acesso.CPF = cnpj;
                acesso.Nome = string.Empty;
                acesso.Email = "";
                acesso.IdContratante = idContratante;
                acesso.TextoTermoAceite = string.Empty;
                acesso.TipoCadastroNovoUsuario = (EnumTipoCadastroNovoUsuario)tipoCadastro;
                acesso.Login = cnpj;
            }
            ViewBag.DisplayForm = true;
            ViewBag.DisplaySucesso = false;
            ViewBag.DisplayAlerta = false;

            return View(acesso);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CadastrarUsuario(UsuarioVM model)
        {
            try
            {
                ValidarPoliticaSenha(model);
                if (model.TipoCadastroNovoUsuario == EnumTipoCadastroNovoUsuario.PreCadastrado)
                {
                    ModelState.Remove("Nome");
                    ModelState.Remove("CPF");
                }

                if (ModelState.IsValid)
                {
                    var usuarioMapeado = Mapper.Map<Usuario>(model);
                    var senhaMapeada = Mapper.Map<USUARIO_SENHAS>(usuarioMapeado);
                    if (model.TipoCadastroNovoUsuario == EnumTipoCadastroNovoUsuario.PreCadastrado)
                    {
                        usuarioMapeado.CPF_CNPJ = model.CPF;
                        usuarioMapeado.CONTRATANTE_ID = null;
                        _usuarioService.IncluirNovoUsuarioPadraoPreCadastro(usuarioMapeado, senhaMapeada);
                        string chave = _configuracaoService.BuscarChave(1);
                        return RedirectToAction("PopularValidarUsuarioSenha", "PreCadastro", new
                        {
                            area = "Externo",
                            chaveurl = Cripto.Criptografar(string.Format("idContratante={0}&cnpj={1}&senha={2}", model.IdContratante,
                            model.Login, model.Senha), Key)
                        });
                    }
                    _usuarioService.IncluirUsuarioPadraoSenha(usuarioMapeado, senhaMapeada, new int[0], new int[0]);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return View(model);
        }
        #endregion

        #region Registro

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Registro()
        {
            RegistroVM registro = new RegistroVM { TipoCadastro = 1 };
            ViewBag.displayForm = "";
            ViewBag.displaySucesso = "display: none;";

            return View(registro);
        }

        [HttpPost]
        public ActionResult Registro(RegistroVM model)
        {
            if (model.TipoCadastro == 1)
                if (ModelState.Keys.Contains("RazaoSocial"))
                    ModelState.Remove("RazaoSocial");
                else
                {
                    if (string.IsNullOrEmpty(model.CNPJ))
                        ModelState.AddModelError("CNPJ", "Informe o CNPJ!");
                    else
                    {
                        if (!Validacao.ValidaCNPJ(model.CNPJ))
                            ModelState.AddModelError("CNPJ", "CNPJ Inválido!");
                    }
                    if (_contratanteService.ProcurarPorCnpj(model.CNPJ))
                        ModelState.AddModelError("CNPJ", "CNPJ já cadastrado!");
                }

            if (_usuarioService.ValidarPorEmail(model.Email))
                ModelState.AddModelError("EMAIL", "E-mail já cadastrado!");

            //VALIDA CPF
            if (!string.IsNullOrEmpty(model.CPF))
                if (!Validacao.ValidaCPF(model.CPF))
                    ModelState.AddModelError("CPF", "CPF Inválido!");

            if (ModelState.IsValid)
            {
                Contratante contratante = Mapper.Map<Contratante>(model);

                CONTRATANTE_CONFIGURACAO config = new CONTRATANTE_CONFIGURACAO
                {
                    SOLICITA_DOCS = true,
                    SOLICITA_FICHA_CAD = true,
                    WFD_CONTRATANTE = contratante
                };

                Usuario usuario = Mapper.Map<Usuario>(model);

                _usuarioService.IncluirUsuario(contratante, config, usuario);

                if (Acessar(model.Email, model.Senha))
                {
                    return RedirectToAction("Index", "HomeAdmin");
                }

                ViewBag.displayForm = "display: none;";
                ViewBag.displaySucesso = "";
            }
            else
            {
                ViewBag.displayForm = "";
                ViewBag.displaySucesso = "display: none;";
            }

            return View(model);
        }
        #endregion Registro

        #region Acesso

        [AllowAnonymous]
        public ActionResult Acesso(string ReturnUrl, string chaveurl)
        {
            Session.Abandon();
            AcessoVM acesso = new AcessoVM();
            if (chaveurl != null)
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);

                int idSolicitacao = 0;
                int travaLogin = 0;
                int tipoCadastro = 0;
                int idContratante = 0;
                Int32.TryParse(param.First(p => p.Name == "SolicitacaoID").Value, out idSolicitacao);
                Int32.TryParse(param.First(p => p.Name == "TravaLogin").Value, out travaLogin);
                Int32.TryParse(param.First(p => p.Name == "tipocadastro").Value, out tipoCadastro);
                Int32.TryParse(param.First(p => p.Name == "idContratante").Value, out idContratante);
                string login = param.First(p => p.Name == "Login").Value;

                acesso.SolicitacaoId = idSolicitacao;
                acesso.TravaLogin = travaLogin;
                acesso.Login = login;

                if ((int)EnumTipoCadastroNovoUsuario.PreCadastrado == tipoCadastro)
                {
                    acesso.TipoCadastroNovoUsuario = EnumTipoCadastroNovoUsuario.PreCadastrado;
                    acesso.ContratanteId = idContratante;
                }
            }
            acesso.ReturnUrl = ReturnUrl;

            return View(acesso);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Acesso(AcessoVM model)
        {
            string chave = Path.GetRandomFileName().Replace(".", "");
            try
            {
                var usuarioLogado = _usuarioService.BuscarPorLogin(model.Login);

                //Valida Usuário Existe
                if (usuarioLogado == null)
                {
                    Log.Info("Usuário inválido!");
                    FinalizarAcesso();
                    ModelState.AddModelError("", "Usuário inválido!");
                }

                if (ModelState.IsValid)
                {
                    ProcessoLoginDTO processoLogin = _processoLoginService.ExecutarLogin(model.Login, model.Senha);
                    if (usuarioLogado.ATIVO)
                    {
                        if (Acessar(model.Login, model.Senha))
                        {
                            if (model.TipoCadastroNovoUsuario == EnumTipoCadastroNovoUsuario.PreCadastrado)
                            {
                                return RedirectToAction("PopularValidarUsuarioSenha", "PreCadastro", new
                                {
                                    area = "Externo",
                                    chaveurl = Cripto.Criptografar(string.Format("idContratante={0}&cnpj={1}&senha={2}",
                                    model.ContratanteId, model.Login, model.Senha), Key),
                                    returnUrl = model.ReturnUrl
                                });
                            }
                            if (usuarioLogado.CONTRATANTE_ID == null) //Se for Fornecedor vá pra essa página
                            {
                                if (model.SolicitacaoId == null)
                                {
                                    var pjpf = _pjPfService.BuscarIdFornecedorPorCnpj(model.Login);

                                    if (pjpf == 0)
                                        model.SolicitacaoId = _solicitacaoCadastroFornecedorService.BuscarIdSolicitacaoPorCnpj(model.Login);
                                    else
                                        model.FornecedorId = pjpf;

                                }
                                return RedirectToActionPermanent("FichaCadastral", "Documento", new
                                {
                                    chaveurl = Cripto.Criptografar(string.Format("SolicitacaoID={0}&FornecedorID={1}&ContratanteID=0", model.SolicitacaoId, model.FornecedorId), Key)
                                });
                            }
                            if (usuarioLogado.PRIMEIRO_ACESSO == true)
                            {
                                usuarioLogado.TROCAR_SENHA = chave;
                                _usuarioService.AlterarUsuario(usuarioLogado);
                                return RedirectToAction("TrocaSenhaEsqueceu", "Home",
                                    new
                                    {
                                        chaveurl = Cripto.Criptografar(string.Format("id={0}&chave={1}", usuarioLogado.ID, chave), Key)
                                    });
                            }
                            if (string.IsNullOrEmpty(model.ReturnUrl))
                                return RedirectToAction("Index", "HomeAdmin");
                            else
                            {
                                string[] url = model.ReturnUrl.Split(new char[] { '/' });
                                if (url.Length > 0)
                                    return Redirect(model.ReturnUrl);
                                else
                                    return RedirectToAction("Index", "HomeAdmin");
                            }
                        }
                    }
                    else
                    {
                        Log.Error(processoLogin.Mensagem);
                        ModelState.AddModelError("", processoLogin.Mensagem);
                    }
                }
                Log.Info("Erro ao tentar logar!");
                FinalizarAcesso();
                ModelState.AddModelError("", "Erro ao tentar logar confirme login e senha de seu usuário");
                return View(model);
            }
            catch (EntityException ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", "Ocorreu um erro de conexão.");
                FinalizarAcesso();
                return View(model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ModelState.AddModelError("", ex.Message);
                FinalizarAcesso();
                return View(model);
            }
        }

        #endregion Acesso

        #region EsqueceuSenha
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult EsqueceuSenha()
        {
            AcessoVM acesso = new AcessoVM();
            ViewBag.DisplayForm = "";
            ViewBag.DisplaySucesso = "display: none;";

            return View(acesso);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EsqueceuSenha(AcessoVM model)
        {
            ModelState.Remove("Email");
            ModelState.Remove("Senha");
            ViewBag.DisplayForm = "";
            ViewBag.DisplaySucesso = "display: none;";

            if (ModelState.IsValid)
            {
                Usuario usuario = _usuarioService.BuscarPorLogin(model.Login);
                if (usuario != null)
                {
                    // ARMAZENA CHAVE DE TROCA
                    string chave = Path.GetRandomFileName().Replace(".", "");
                    usuario.TROCAR_SENHA = chave;
                    _usuarioService.AlterarUsuario(usuario);

                    // CRIPTOGRAFA A URL QUE SERA ENVIADA AO USUÁRIO

                    string url = Url.Action("TrocaSenhaEsqueceu", "Home", new { chaveurl = Cripto.Criptografar(string.Format("id={0}&chave={1}", usuario.ID, chave), Key) }, Request.Url.Scheme);

                    EmailWebForLink mensagemEmail = new EmailWebForLink(usuario.EMAIL);
                    mensagemEmail.EsquecerSenha(url);

                    _metodosGerais.EnviarEmail(mensagemEmail);

                    ViewBag.DisplayForm = "display: none;";
                    ViewBag.DisplaySucesso = "";
                }
                else
                {
                    ModelState.AddModelError("Email", "E-mail não encontrado!");
                }
            }

            return View(model);
        }
        #endregion EsqueceuSenha

        #region TrocaSenhaEsqueceu

        public ActionResult TrocaSenhaEsqueceu(string chaveurl)
        {

            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);

            // VERIFICA SE OS PARAMETROS FORAM MONTADOS CORRETAMENTE
            if (param.Count == 0)
            {
                ViewBag.DisplayForm = false;
                ViewBag.DisplaySucesso = false;
                ViewBag.DisplayAlerta = true;
                return View();
            }

            int id;
            string chaveEsqueceu;
            Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
            chaveEsqueceu = param.First(p => p.Name == "chave").Value;

            Usuario usuario = _usuarioService.BuscarPorId(id);

            //VERIFICA SE O Usuario EXISTE
            if (usuario == null)
            {
                ViewBag.DisplayForm = false;
                ViewBag.DisplaySucesso = false;
                ViewBag.DisplayAlerta = true;
                return View();
            }

            //VERIFICA SE A CHAVE DE TROCA DE SENHA CONFERE
            if (usuario.TROCAR_SENHA != chaveEsqueceu)
            {
                ViewBag.DisplayForm = false;
                ViewBag.DisplaySucesso = false;
                ViewBag.DisplayAlerta = true;
                return View();
            }

            TrocaSenhaEsqueceuVM acesso = new TrocaSenhaEsqueceuVM();
            acesso.ID = usuario.ID;

            if (usuario.Contratante != null)
                if (usuario.Contratante.TIPO_CADASTRO_ID != null)
                    acesso.TipoCadastro = (int)usuario.Contratante.TIPO_CADASTRO_ID;

            ViewBag.DisplayForm = true;
            ViewBag.DisplaySucesso = false;
            ViewBag.DisplayAlerta = false;
            ViewBag.CPF_CNPJ = (usuario.CPF_CNPJ.Length == 14 ? 1 : 2);

            return View(acesso);
        }

        [HttpPost]
        public ActionResult TrocaSenhaEsqueceu(TrocaSenhaEsqueceuVM model)
        {
            ViewBag.DisplayForm = true;
            ViewBag.DisplaySucesso = false;
            ViewBag.DisplayAlerta = false;

            ValidarPoliticaSenha(model);
            Usuario usuario = _usuarioService.BuscarPorId(model.ID);
            ViewBag.CPF_CNPJ = (usuario.CPF_CNPJ.Length == 14 ? 1 : 2);

            if (ModelState.IsValid)
            {
                if (usuario != null)
                {

                    //if (usuario.CPF_CNPJ != model.CPF.Replace(".", "").Replace("-", "").Replace("/", ""))
                    //{
                    //    if (usuario.CPF_CNPJ.Length == 14)
                    //        ModelState.AddModelError("CNPJ", "CNPJ Inválido!");
                    //    else
                    //        ModelState.AddModelError("CPF", "CPF Inválido!");
                    //}

                    //Validar6UltimasSenhas(model, usuario);

                    if (ModelState.IsValid)
                    {
                        IncluirHistoricoIncluirNovaSenhaUsuario(model, usuario);

                        ViewBag.DisplayForm = false;
                        ViewBag.DisplaySucesso = true;
                        ViewBag.DisplayAlerta = false;
                        return View();
                    }
                }
                else
                {
                    ViewBag.DisplayForm = true;
                    ViewBag.DisplaySucesso = false;
                    ViewBag.DisplayAlerta = false;
                    ModelState.AddModelError("", "Erro ao tentar salvar a nova senha, o identificador do usuário não existe!");
                }
            }

            return View(model);
        }

        #endregion TrocaSenhaEsqueceu

        private ActionResult ExecutarPrimeiroAcesso(Usuario usuario)
        {
            string chave = Path.GetRandomFileName().Replace(".", "");
            usuario.TROCAR_SENHA = chave;
            _usuarioService.ExecutarPrimeiroAcesso(usuario);

            return RedirectToAction("TrocaSenhaEsqueceu", "Home",
                new
                {
                    chaveurl = Cripto.Criptografar(string.Format("id={0}&chave={1}", usuario.ID, chave), Key)
                });
        }

        public ActionResult Alerta()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Error(HttpException ex)
        {
            ViewBag.Exception = Session["httpEx"];
            return View();
        }

        #region CadastrarUsuarioFornecedor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="chaveurl"></param>
        /// <returns></returns>
        public ActionResult CadastrarUsuarioFornecedor(string chaveurl)
        {
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);

            int idSolicitacao = 0;
            Int32.TryParse(param.First(p => p.Name == "SolicitacaoID").Value, out idSolicitacao);

            CadastrarUsuarioFornecedorVM acesso = new CadastrarUsuarioFornecedorVM()
            {
                DocumentoPjPf = param.First(p => p.Name == "Login").Value,
                Email = param.First(p => p.Name == "Email").Value,
                SolicitacaoId = idSolicitacao,
                NomeEmpresa = _solicitacaoCadastroFornecedorService.BuscarRazaoOuNomePorSolicitacao(idSolicitacao),
                NomeFornecedor = "FORNECEDOR",
                TextoTermoAceite = _contratanteConfiguracaoService.BuscarPorIdSolicitacao(idSolicitacao).TERMO_ACEITE
            };

            acesso.TextoTermoAceite = acesso.TextoTermoAceite
                    .Replace("^NomeFornecedor^", acesso.NomeFornecedor)
                    .Replace("^NomeEmpresa^", acesso.NomeEmpresa);

            if (_usuarioService.VerificaLoginExistente(acesso.DocumentoPjPf))
                return RedirectToAction("Acesso", "Home");

            ViewBag.DisplayForm = true;
            ViewBag.DisplaySucesso = false;
            ViewBag.DisplayAlerta = false;

            return View(acesso);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CadastrarUsuarioFornecedor(CadastrarUsuarioFornecedorVM modelo)
        {
            if (!modelo.TermoAceite)
                ModelState.AddModelError("", "O termo de aceite deve ser aceito.");
            try
            {
                if (modelo.Email == modelo.EmailAlternativo)
                    ModelState.AddModelError("EmailAlternativo", "O e-mail alternativo não deve ser igual ao e-mail principal.");

                ValidarPoliticaSenha(modelo);

                if (ModelState.IsValid)
                {
                    if (modelo.TermoAceite)
                    {
                        //int? contratanteId = null;
                        //if (modelo.SolicitacaoId != null)
                        //{
                        //    contratanteId = _solicitacaoService.Get(modelo.SolicitacaoId).CONTRATANTE_ID;
                        //}
                        Usuario usuarioMapeado = new Usuario
                        {
                            EMAIL = modelo.Email,
                            SENHA = PasswordHash.CreateHash(modelo.Senha),
                            ATIVO = true,
                            PRIMEIRO_ACESSO = false,
                            LOGIN = modelo.CnpjSemFormatacao,
                            CPF_CNPJ = modelo.CnpjSemFormatacao,
                            DT_ATIVACAO = DateTime.Now,
                            DT_CRIACAO = DateTime.Now,
                            CONTA_TENTATIVA = 1,
                            CONTRATANTE_ID = null, //contratanteId,
                            PRINCIPAL = false,
                            EMAIL_ALTERNATIVO = modelo.EmailAlternativo
                        };
                        var senhaMapeada = Mapper.Map<USUARIO_SENHAS>(usuarioMapeado);
                        try
                        {
                            _usuarioService.IncluirUsuarioPadraoSenha(usuarioMapeado, senhaMapeada, new int[0], new int[0]);
                            return Acesso(new AcessoVM
                            {
                                Email = modelo.Email,
                                Login = modelo.CnpjSemFormatacao,
                                Senha = modelo.Senha,
                                SolicitacaoId = modelo.SolicitacaoId
                            });
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex);
                        }
                    }
                    return View(modelo);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            ViewBag.DisplayForm = true;
            ViewBag.DisplaySucesso = false;
            ViewBag.DisplayAlerta = false;

            return View(modelo);
        }
        #endregion
        [HttpPost]
        public ActionResult ExibirTermoAceite(CadastrarUsuarioFornecedorVM modelo)
        {
            //verificar se o usuário já realizou termo de aceito em tabela Usuario
            //Se o contratante que ele pertence tem um termo de aceite na tabela WFD_CONTRATANTE_CONFIG
            //Se o contratante que ele pertence não tiver exibir o termo de aceite de contratanteId = 1 - CHConsultoria, que será o termo de aceite padrão
            //se ele aceitar o termo direciona pra página devida;
            //do contrário volta a página sem seguir

            return Acesso(new AcessoVM
            {
                Email = modelo.Email,
                Login = modelo.DocumentoPjPf,
                Senha = modelo.Senha,
                SolicitacaoId = modelo.SolicitacaoId
            });
        }
        #region Compartilhados
        public bool Acessar(string email, string senha)
        {
            Usuario usuario = _usuarioService.BuscarPorLoginParaAcesso(email);

            if (usuario != null)
            {
                if (PasswordHash.ValidatePassword(senha, usuario.SENHA))
                {
                    Autenticado aut = Mapper.Map<Autenticado>(usuario);

                    string caminhoFisico = Server.MapPath("/ImagensUsuarios");
                    string caminhoCompleto = string.Empty;
                    if (usuario.CONTRATANTE_ID != null)
                    {
                        string arquivo = string.Format("ImagemContratante{0}{1}",
                            usuario.Contratante.ID,
                            (string.IsNullOrEmpty(usuario.Contratante.EXTENSAO_IMAGEM)
                            ? ".png"
                            : usuario.Contratante.EXTENSAO_IMAGEM)
                            );

                        caminhoCompleto = caminhoFisico + "\\" + arquivo;

                        aut.Imagem = arquivo;

                        if (usuario.Contratante.WFD_GRUPO.Count > 0)
                            aut.Grupo = usuario.Contratante.WFD_GRUPO.First().ID;
                        if (usuario.Contratante.TIPO_CADASTRO_ID == 2)
                            aut.NomeEmpresa = ((string.IsNullOrEmpty(usuario.Contratante.NOME_FANTASIA))
                                ? usuario.Contratante.RAZAO_SOCIAL
                                : usuario.Contratante.NOME_FANTASIA);
                        else
                            aut.NomeEmpresa = "";
                    }
                    //else
                    //{
                    //    aut.TipoContratante = 2;
                    //}
                    MemoryStream stream1 = new MemoryStream();
                    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Autenticado));
                    ser.WriteObject(stream1, aut);

                    stream1.Position = 0;
                    StreamReader sr = new StreamReader(stream1);
                    string sSr = sr.ReadToEnd();
                    string dados = Cripto.Criptografar(sSr, Key);
                    List<Perfil> lstWacUsuarioPerfil = usuario.WAC_PERFIL.ToList();
                    string roles = string.Empty;
                    foreach (var perfil in lstWacUsuarioPerfil)
                    {
                        foreach (var funcao in perfil.WAC_FUNCAO)
                        {
                            if (!roles.Contains(funcao.CODIGO))
                                roles += funcao.CODIGO + ",";
                        }
                    }
                    _metodosGerais.CriarAuthTicket(dados, roles);

                    if (usuario.TROCAR_SENHA != null)
                    {
                        usuario.TROCAR_SENHA = null;
                        _usuarioService.AlterarUsuario(usuario);
                    }

                    if (usuario.CONTRATANTE_ID != null)
                    {
                        if (usuario.Contratante.LOGO_FOTO != null && System.IO.Directory.Exists(caminhoFisico))
                            System.IO.File.WriteAllBytes(caminhoCompleto, usuario.Contratante.LOGO_FOTO.ToArray());
                    }
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        private void ValidarPoliticaSenha(TrocaSenhaEsqueceuVM model)
        {
            if ((model.Senha.Length < 8) || (model.ConfirmaSenha.Length < 8))
            {
                ModelState.AddModelError("model.Senha", "As senhas devem ter no mínimo 8 caracteres");
                ModelState.AddModelError("model.ConfirmaSenha", "As senhas devem ter no mínimo 8 caracteres");
            }
            //Regex rgx = new Regex(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,10}", RegexOptions.IgnoreCase);
            //MatchCollection matches = rgx.Matches(model.Senha);
            //if (matches.Count <= 0)
            //{
            //    ModelState.AddModelError("", "A senha deve respeitar a política de senha da empresa.");
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        private void ContabilizarErroLogin(Usuario usuario)
        {
            usuario.CONTA_TENTATIVA = usuario.CONTA_TENTATIVA + 1;
            if (usuario.CONTA_TENTATIVA > 9)
                usuario.ATIVO = false;
            _usuarioService.AlterarUsuario(usuario);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuario"></param>
        private void ValidarBloqueioTempo(Usuario usuario)
        {
            USUARIO_SENHAS historicoSenha = usuario.WFD_USUARIO_SENHAS_HIST.OrderBy(x => x.SENHA_DT).LastOrDefault();

            if (historicoSenha != null)
            {
                if (usuario.EXPIRA_EM_DIAS > 0)
                {
                    int valor = (DateTime.Now - historicoSenha.SENHA_DT).Days;
                    if (valor > usuario.EXPIRA_EM_DIAS)
                    {
                        string chave = Path.GetRandomFileName().Replace(".", "");
                        usuario.TROCAR_SENHA = chave;
                        usuario.PRIMEIRO_ACESSO = true;
                        _usuarioService.ZerarTentativasLogin(usuario);
                    }
                }
            }
            else
            {
                try
                {
                    //INSERT INICIAL NA TABELA DE WFD_USUARIO_SENHAS_HIST
                    USUARIO_SENHAS historicoSenhaInclusao = new USUARIO_SENHAS
                    {
                        SENHA = usuario.SENHA,
                        SENHA_DT = DateTime.Now,
                        USUARIO_ID = usuario.ID
                    };
                    _usuarioSenhaHistoricoService.InserirSenhaUsuario(historicoSenhaInclusao);
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);

                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void FinalizarAcesso()
        {
            var httpCookie = Response.Cookies["ASP.NET_SessionId"];
            if (httpCookie != null)
                httpCookie.Expires = DateTime.Now.AddYears(-1);
            FormsAuthentication.SignOut();
            Session.Abandon();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="usuario"></param>
        private void IncluirHistoricoIncluirNovaSenhaUsuario(TrocaSenhaEsqueceuVM model, Usuario usuario)
        {
            string senha = PasswordHash.CreateHash(model.Senha);

            USUARIO_SENHAS historicoSenha = new USUARIO_SENHAS
            {
                SENHA = senha,
                USUARIO_ID = usuario.ID,
                SENHA_DT = DateTime.Now
            };

            usuario.SENHA = senha;
            usuario.TROCAR_SENHA = null;
            usuario.PRIMEIRO_ACESSO = false;
            _usuarioService.IncluirUsuarioIncluirNovaSenhaUsuario(usuario, historicoSenha);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="usuario"></param>
        private void Validar6UltimasSenhas(TrocaSenhaEsqueceuVM model, Usuario usuario)
        {
            List<USUARIO_SENHAS> historicoSenhaList = usuario.WFD_USUARIO_SENHAS_HIST
                                                                        .OrderBy(x => x.SENHA_DT)
                                                                        .Take(6)
                                                                        .ToList();
            foreach (var item in historicoSenhaList)
            {
                if (PasswordHash.ValidatePassword(model.Senha, item.SENHA))
                {
                    ModelState.AddModelError("", "Erro ao tentar salvar a nova senha! A mesma corresponde a uma senha anteriormente cadastrada.");
                }
            }
        }

        private string PegaIPAcesso()
        {
            string clientIp = (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? Request.ServerVariables["REMOTE_ADDR"]).Split(',')[0].Trim();

            return clientIp;
        }

        private string PegaNavegadorAcesso()
        {
            string browser = Request.Browser.Type;
            return browser;
        }

        #endregion
    }
}