using AutoMapper;
using LinqKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Services;
using WebForLink.Web.Areas.Externo.Models;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Areas.Externo.Controllers
{
    public class PreCadastroController : ControllerPadrao
    {
        #region Propriedades Privadas
        private readonly IEnderecoWebForLinkAppService enderecoBP;
        private readonly IFornecedorWebForLinkAppService pjpfBp;
        private readonly IFornecedorBaseWebForLinkAppService pjpfBaseBp;
        private readonly IUsuarioWebForLinkAppService usuarioBP;
        private readonly IConfiguracaoWebForLinkAppService wFDConfigBP;
        private readonly IContratanteWebForLinkAppService contratanteBP;
        private readonly IPreCadastroWebForLinkAppService processoPreCadastro;
        private readonly IServicosMateriaisWebForLinkAppService _unspscBP;
        private readonly IProcessoLoginWebForLinkAppService _processoLoginService;
        #endregion

        public PreCadastroController(
            IEnderecoWebForLinkAppService endereco,
        IFornecedorWebForLinkAppService pjpf,
        IFornecedorBaseWebForLinkAppService pjpfBase,
        IUsuarioWebForLinkAppService usuario,
        IConfiguracaoWebForLinkAppService wFdConfig,
        IContratanteWebForLinkAppService contratante,
        IPreCadastroWebForLinkAppService preCadastro,
        IServicosMateriaisWebForLinkAppService servicosMateriais,
        IProcessoLoginWebForLinkAppService processoLogin)
        {
            _processoLoginService = processoLogin;
            enderecoBP = endereco;
            pjpfBp = pjpf;
            pjpfBaseBp = pjpfBase;
            usuarioBP = usuario;
            wFDConfigBP = wFdConfig;
            contratanteBP = contratante;
            processoPreCadastro = preCadastro;
            _unspscBP = servicosMateriais;
            ViewBag.TipoEndereco = new SelectList(enderecoBP.ListarTodosTiposEnderecosPorNome(), "ID", "NM_TP_ENDERECO");
            ViewBag.UF = new SelectList(enderecoBP.ListarTodosPorNome(), "UF_SGL", "UF_NM");
            ViewBag.Imagem = null;
        }

        // GET: Externo/PreCadastro/
        public ActionResult Index(string chaveurl)
        {
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            int idContratante;
            Int32.TryParse(param.First(p => p.Name == "idContratante").Value, out idContratante);
            InclusaoLinkExternoVM modelo = new InclusaoLinkExternoVM(idContratante, param.First(p => p.Name == "idChave").Value, chaveurl);
            ViewBag.ExibirFicha = false;
            modelo.isValidarSenha = true;
            ViewBag.Imagem = RetornarEnderecoImagemContratante(idContratante);
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Index(InclusaoLinkExternoVM model)
        {
            var modelCpfCnpj = Mascara.RemoverMascaraCpfCnpj(model.CPF);

            var filtroPesquisaFornecedor = PredicateBuilder.New<Fornecedor>();
            filtroPesquisaFornecedor = filtroPesquisaFornecedor.And(x => x.CNPJ == modelCpfCnpj || x.CPF == modelCpfCnpj);

            DadosExternoPreCadastro preCadastro = new DadosExternoPreCadastro(pjpfBaseBp.ListarPorDocumento(modelCpfCnpj), modelCpfCnpj, model.IdContratante);
            preCadastro.PopularDados();

            switch (preCadastro.PreCadastroEnum)
            {
                case CasosPreCadastroEnum.PreCadastradoOutroContratante:
                    model.FichaCadastral = new FichaCadastralWebForLinkVM(model.IdContratante, CasosPreCadastroEnum.PreCadastradoOutroContratante);
                    break;
                case CasosPreCadastroEnum.PreCadastradoProprio:
                    model.FichaCadastral = PopularFichaCadastral(preCadastro.FornecedorBaseProprio, model.IdContratante, CasosPreCadastroEnum.PreCadastradoProprio, model.Link);
                    break;
                default:
                    break;
            }
            ViewBag.ExibirFicha = true;
            ViewBag.Imagem = RetornarEnderecoImagemContratante(model.IdContratante);
            return View(model);
        }

        [HttpPost]
        public ActionResult ValidarUsuarioSenhaPreCadastro(InclusaoLinkExternoVM model)
        {
            var modelCpfCnpj = model.isCNPJ
                ? model.CNPJ.Replace(".", "").Replace("-", "").Replace("/", "")
                : model.CPF.Replace(".", "").Replace("-", "");
            //Validando apenas Login
            if (!model.isValidarSenha)
                ModelState.Remove("Senha");

            if (model.isCNPJ)
                ModelState.Remove("CPF");
            else
                ModelState.Remove("CNPJ");

            if (ModelState.IsValid)
            {
                string urlRetorno = Url.Action("Index", "PreCadastro", new { area = "Externo", chaveurl = model.Link });
                Usuario usuario = usuarioBP.BuscarPorLogin(modelCpfCnpj);
                if (usuario != null)
                    return RedirectToAction("Acesso", "Home",
                                new
                                {
                                    area = "",
                                    chaveurl = Cripto.Criptografar(string.Format("tipocadastro={0}&cnpj={1}&idContratante={2}&Login={1}&SolicitacaoID=0&TravaLogin=1",
                                        (int)EnumTipoCadastroNovoUsuario.PreCadastrado, modelCpfCnpj, model.IdContratante), Key),
                                    ReturnUrl = urlRetorno
                                });
                else //RedirectToAction CRIAR USUÁRIO
                    return RedirectToAction("CadastrarUsuario", "Home",
                                new
                                {
                                    area = "",
                                    chaveurl = Cripto.Criptografar(string.Format("id=0&tipocadastro={0}&cnpj={1}&idContratante={2}",
                                    (int)EnumTipoCadastroNovoUsuario.PreCadastrado, modelCpfCnpj, model.IdContratante), Key)
                                });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult PopularValidarUsuarioSenha(string chaveurl, string returnUrl)
        {
            try
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                int idContratante;
                Int32.TryParse(param.First(p => p.Name == "idContratante").Value, out idContratante);
                string cnpj = param.First(p => p.Name == "cnpj").Value;
                string senha = param.First(p => p.Name == "senha").Value;

                InclusaoLinkExternoVM model = new WebForLink.Web.Areas.Externo.Models.InclusaoLinkExternoVM()
                {
                    IdContratante = idContratante,
                    CPF = cnpj,
                    isValidarSenha = false,
                    Senha = senha
                };
                if (!Acessar(model.CPF, model.Senha, model.IdContratante))
                {
                    throw new Exception("usuário Inválido");
                }
                var filtroPesquisaFornecedor = PredicateBuilder.New<Fornecedor>();
                filtroPesquisaFornecedor = filtroPesquisaFornecedor.And(x => x.CNPJ == model.CPF || x.CPF == model.CPF);

                DadosExternoPreCadastro preCadastro = new DadosExternoPreCadastro(pjpfBaseBp.ListarPorDocumento(model.CPF), model.CPF, model.IdContratante);
                preCadastro.PopularDados();

                switch (preCadastro.PreCadastroEnum)
                {
                    case CasosPreCadastroEnum.PreCadastradoOutroContratante:
                        model.FichaCadastral = new FichaCadastralWebForLinkVM(model.IdContratante, CasosPreCadastroEnum.PreCadastradoOutroContratante);
                        model.FichaCadastral.ChaveUrl = returnUrl;
                        break;
                    case CasosPreCadastroEnum.PreCadastradoProprio:
                        model.FichaCadastral = PopularFichaCadastral(preCadastro.FornecedorBaseProprio, model.IdContratante, CasosPreCadastroEnum.PreCadastradoProprio, returnUrl);
                        break;
                    default:
                        break;
                }
                model.FichaCadastral.CNPJ_CPF = PontuarCnpjCpf(cnpj);
                ViewBag.ExibirFicha = true;
                ViewBag.Imagem = RetornarEnderecoImagemContratante(model.IdContratante);
                return View("Index", model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return View();
            }
        }

        [HttpPost]
        public ActionResult ValidarUsuarioSenha(InclusaoLinkExternoVM model)
        {
            try
            {
                ViewBag.NomeEmpresa = contratanteBP.BuscarPorId(model.IdContratante).RAZAO_SOCIAL;
                ViewBag.ExibirFicha = false;

                //Validando apenas Login
                if (!model.isValidarSenha)
                    ModelState.Remove("Senha");

                if (ModelState.IsValid)
                {
                    Usuario usuario = usuarioBP.BuscarPorLogin(model.CPF);
                    if (!model.isValidarSenha)
                        if (usuario != null)
                            return RedirectToAction("Acesso", "Home",
                                        new
                                        {
                                            area = "",
                                            chaveurl = Cripto.Criptografar(string.Format("id={0}&tipocadastro={1}&cnpj={2}&idContratante={3}&Login={2}&SolicitacaoID=0&TravaLogin=0",
                                                0, (int)EnumTipoCadastroNovoUsuario.PreCadastrado, model.CPF, model.IdContratante), Key)
                                        });
                        else //RedirectToAction CRIAR USUÁRIO
                            return RedirectToAction("CadastrarUsuario", "Home",
                                        new
                                        {
                                            area = "",
                                            chaveurl = Cripto.Criptografar(string.Format("id={0}&tipocadastro={1}&cnpj={2}&idContratante={3}",
                                                0, (int)EnumTipoCadastroNovoUsuario.PreCadastrado, model.CPF, model.IdContratante), Key)
                                        });
                    else
                    {
                        if (usuario == null)
                            return RedirectToAction("CadastrarUsuario", "Home",
                                        new
                                        {
                                            area = "",
                                            chaveurl = Cripto.Criptografar(string.Format("id={0}&tipocadastro={1}&cnpj={2}&idContratante={3}",
                                                0, (int)EnumTipoCadastroNovoUsuario.PreCadastrado, model.CPF, model.IdContratante), Key)
                                        });
                        ProcessoLoginDTO processoLogin = _processoLoginService.ExecutarLogin(model.CPF, model.Senha);
                        if (!processoLogin.Status)
                            return RedirectToAction("CadastrarUsuario", "Home",
                                        new
                                        {
                                            area = "",
                                            chaveurl = Cripto.Criptografar(string.Format("id={0}&tipocadastro={1}&cnpj={2}&idContratante={3}",
                                                0, (int)EnumTipoCadastroNovoUsuario.PreCadastrado, model.CPF, model.IdContratante), Key)
                                        });
                        else
                        {
                            ModelState.Remove("FichaCadastral");

                            var filtroPesquisaFornecedor = PredicateBuilder.New<Fornecedor>();
                            filtroPesquisaFornecedor = filtroPesquisaFornecedor.And(x => x.CNPJ == model.CPF || x.CPF == model.CPF);

                            DadosExternoPreCadastro preCadastro = new DadosExternoPreCadastro(pjpfBaseBp.ListarPorDocumento(model.CPF), model.CPF, model.IdContratante);
                            preCadastro.PopularDados();

                            switch (preCadastro.PreCadastroEnum)
                            {
                                case CasosPreCadastroEnum.PreCadastradoOutroContratante:
                                    model.FichaCadastral = new FichaCadastralWebForLinkVM(model.IdContratante, CasosPreCadastroEnum.PreCadastradoOutroContratante);
                                    break;
                                case CasosPreCadastroEnum.PreCadastradoProprio:
                                    model.FichaCadastral = PopularFichaCadastral(preCadastro.FornecedorBaseProprio, model.IdContratante, CasosPreCadastroEnum.PreCadastradoProprio, model.Link);
                                    break;
                                //case CasosPreCadastroEnum.CadastradoOutroContratante:
                                //    model.FichaCadastral = PopularFichaCadastral(preCadastro.FornecedorList.FirstOrDefault(), true, model.IdContratante, CasosPreCadastroEnum.CadastradoOutroContratante);
                                //    break;
                                //case CasosPreCadastroEnum.CadastradoProprio:
                                //    model.FichaCadastral = PopularFichaCadastral(preCadastro.FornecedorProprio, false, model.IdContratante, CasosPreCadastroEnum.CadastradoProprio);
                                //    break;
                                default:
                                    break;
                            }
                        }
                    }

                }
                ViewBag.ExibirFicha = true;
                return PartialView("~/Areas/Externo/Views/PreCadastro/Index.cshtml", model);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return View();
            }
        }

        private ActionResult ExibirInputSenha(InclusaoLinkExternoVM model, bool modelError)
        {
            if (modelError)
                ModelState.AddModelError("Senha", "Senha Incorreta");

            ModelState.Remove("isValidarSenha"); // Se for removido o modelo não aceitará receber novo valor
            model.isValidarSenha = true;
            return PartialView("~/Areas/Externo/Views/PreCadastro/_PreCadastro_ValidarUsuario_Editavel.cshtml", model);
        }

        private ActionResult ExibirInputCnpj(InclusaoLinkExternoVM model, bool usuarioInvalido)
        {
            if (usuarioInvalido)
                return RedirectToAction("CadastrarUsuario", "Home",
                            new
                            {
                                area = "",
                                chaveurl = Cripto.Criptografar(string.Format("id={0}&tipocadastro={1}&cnpj={2}&idContratante={3}",
                                    0, (int)EnumTipoCadastroNovoUsuario.PreCadastrado, model.CPF, model.IdContratante), Key)
                            });

            ModelState.AddModelError("CNPJ", "CNPJ Inválido. Usuário não encontrado.");
            ModelState.Remove("isValidarSenha"); // Se for removido o modelo não aceitará receber novo valor
            model.isValidarSenha = false;
            return PartialView("~/Areas/Externo/Views/PreCadastro/_PreCadastro_ValidarUsuario_Editavel.cshtml", model);
        }

        public ActionResult Incluir(FichaCadastralWebForLinkVM modelo, string ServicosSelecionados, string MateriaisSelecionados)
        {
            try
            {
                //UNSPSC
                modelo.FornecedoresUnspsc = new List<FornecedorUnspscVM>();
                modelo.FornecedoresUnspsc = PreencheModelUnspsc(modelo.PJPFID, ServicosSelecionados, MateriaisSelecionados, modelo.SolicitacaoID);

                int validacao = IncluirDadosTabelaPjPfBase(modelo, 1);
                ViewBag.ExibirFicha = true;
                modelo.PjpfBaseId = validacao;
                ViewBag.RetSucessoBancos = validacao != 0 ? 1 : -1;
                if (validacao == 0)
                    return Json(new { MensagemErro = "Não foi possível alterar o pré-cadastro" });
                return PartialView("~/Areas/Externo/Views/Shared/_PreCadastro_FichaCadastral.cshtml", modelo);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return View();
        }

        public ActionResult LinkLst()
        {
            string chave = wFDConfigBP.BuscarChave(1);
            List<LinkExternoVM> modelo = contratanteBP.ListarTodos()
                .Select(x => new LinkExternoVM
                {
                    IdContratante = x.ID,
                    NomeContratante = x.RAZAO_SOCIAL
                }).ToList();
            modelo.ForEach(x =>
            {
                x.Chave = chave;
                x.Link = Url.Action("Index", "PreCadastro", new
                {
                    chaveurl = Cripto.Criptografar(string.Format("idContratante={0}&idChave={1}", x.IdContratante, chave), Key)
                }, Request.Url.Scheme);
            });
            return View(modelo);
        }

        private string RetornarEnderecoImagemContratante(int contratanteId)
        {
            string caminhoFisico = Server.MapPath("/ImagensUsuarios");
            string caminhoCompleto = string.Empty;
            Contratante contratante = contratanteBP.BuscarPorId(contratanteId);
            string arquivo = "ImagemContratante" + contratanteId + (string.IsNullOrEmpty(contratante.EXTENSAO_IMAGEM)
                ? ".png"
                : contratante.EXTENSAO_IMAGEM);

            caminhoCompleto = caminhoFisico + "\\" + arquivo;

            return arquivo;
        }

        private bool Acessar(string email, string senha, int contratanteId)
        {
            Usuario usuario = usuarioBP.BuscarPorLoginParaAcesso(email);

            if (usuario != null)
            {
                if (PasswordHash.ValidatePassword(senha, usuario.SENHA))
                {
                    Autenticado aut = Mapper.Map<Autenticado>(usuario);

                    string caminhoFisico = Server.MapPath("/ImagensUsuarios");
                    string caminhoCompleto = string.Empty;
                    Contratante contratante = contratanteBP.BuscarPorId(contratanteId);
                    string arquivo = "ImagemContratante" + contratanteId + (string.IsNullOrEmpty(contratante.EXTENSAO_IMAGEM)
                        ? ".png"
                        : contratante.EXTENSAO_IMAGEM);

                    caminhoCompleto = caminhoFisico + "\\" + arquivo;

                    aut.Imagem = arquivo;
                    aut.NomeEmpresa = ((string.IsNullOrEmpty(contratante.NOME_FANTASIA))
                        ? contratante.RAZAO_SOCIAL
                        : contratante.NOME_FANTASIA);

                    if (contratante.WFD_GRUPO.Count > 0)
                        aut.Grupo = contratante.WFD_GRUPO.First().ID;

                    string nomeUsuario = User.Identity.Name;
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
                        usuarioBP.AlterarUsuario(usuario);
                    }

                    if (usuario.CONTRATANTE_ID != null)
                    {
                        if (usuario.Contratante.LOGO_FOTO != null && System.IO.Directory.Exists(caminhoFisico))
                            System.IO.File.WriteAllBytes(caminhoCompleto, usuario.Contratante.LOGO_FOTO.ToArray());
                    }

                    _metodosGerais.AuthenticateRequest();

                    return true;
                }
            }

            return false;
        }

        private static string PontuarCnpjCpf(string documento)
        {
            return documento.Length == 14
                ? string.Format("{0}.{1}.{2}/{3}-{4}", documento.Substring(0, 2), documento.Substring(2, 3), documento.Substring(5, 3), documento.Substring(8, 4), documento.Substring(12, 2))
                : string.Format("{0}.{1}.{2}-{3}", documento.Substring(0, 3), documento.Substring(3, 3), documento.Substring(6, 3), documento.Substring(9, 2));
        }

        private int IncluirDadosTabelaPjPfBase(FichaCadastralWebForLinkVM modelo, int acao)
        {
            try
            {
                modelo.CNPJ_CPF = modelo.CNPJ_CPF.Replace(".", "").Replace("-", "").Replace("/", "");
                processoPreCadastro.FornecedorBase = Mapper.Map<FORNECEDORBASE>(modelo);
                modelo.DadosEnderecos.ToList().ForEach(
                    x =>
                    {
                        if (x.T_UF != null)
                            x.UF = x.T_UF.UF_SGL;
                    });
                processoPreCadastro.FornecedorBaseEndereco = Mapper.Map<List<FORNECEDORBASE_ENDERECO>>(modelo.DadosEnderecos.ToList());
                processoPreCadastro.FornecedorBaseContato = Mapper.Map<List<FORNECEDORBASE_CONTATOS>>(modelo.DadosContatos.ToList());
                processoPreCadastro.FornecedoresBaseUnspsc = Mapper.Map<List<FORNECEDORBASE_UNSPSC>>(modelo.FornecedoresUnspsc.ToList());
                processoPreCadastro.DocumentoFornecedor = processoPreCadastro.FornecedorBase.CPF ?? processoPreCadastro.FornecedorBase.CNPJ;
                processoPreCadastro.ContratanteId = modelo.ContratanteID;
                processoPreCadastro.PjpfBaseId = modelo.PjpfBaseId;
                try
                {
                    processoPreCadastro.IncluirPreCadastro(modelo.PreCadastroEnum, acao);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    return 0;
                }
                return modelo.PjpfBaseId == 0 ? processoPreCadastro.PjpfBaseId : modelo.PjpfBaseId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Se ele já existir em Fornecedor será exibido bloqueado TRUE: Editavel - False:Bloqueado.
        /// <para>Se não ele será exibido o primeiro da lista que contém este Id de Contratante.</para>
        /// </summary>
        /// <param name="cnpjUsuario">cnpj/cpf do Usuario logado</param>
        /// <param name="fornecedor">Entidade PJPF de fornecedor</param>
        /// <param name="isEdicao">Se bloqueará edição</param>
        /// <returns></returns>
        public FichaCadastralWebForLinkVM PopularFichaCadastral(Fornecedor fornecedor, bool isEdicao, int idContratante, CasosPreCadastroEnum preCadastroEnum)
        {
            FichaCadastralWebForLinkVM fichaCadastral = Mapper.Map<FichaCadastralWebForLinkVM>(fornecedor);
            fichaCadastral.ContratanteID = idContratante;
            fichaCadastral.HabilitaEdicao = isEdicao;
            fichaCadastral.IsPjpfProprio = preCadastroEnum == CasosPreCadastroEnum.CadastradoProprio;
            fichaCadastral.IsPjpfBaseProprio = false;
            PopularFichaCadastral(preCadastroEnum, fichaCadastral);
            fichaCadastral.FornecedoresUnspsc =
    Mapper.Map<List<FORNECEDOR_UNSPSC>, List<FornecedorUnspscVM>>(fornecedor.FornecedorServicoMaterialList.ToList());
            return fichaCadastral;
        }

        /// <summary>
        /// Se ele já existir em FORNECEDORBASE será exibido apenas com os dados que tiverem com o mesmo IdContratante.
        /// </summary>
        /// <param name="fornecedor"></param>
        /// <param name="idContratante"></param>
        /// <returns></returns>
        public FichaCadastralWebForLinkVM PopularFichaCadastral(FORNECEDORBASE fornecedor, int idContratante, CasosPreCadastroEnum preCadastroEnum, string chaveUrl)
        {
            try
            {
                FichaCadastralWebForLinkVM fichaCadastral = Mapper.Map<FichaCadastralWebForLinkVM>(fornecedor);
                fichaCadastral.ContratanteID = idContratante;
                fichaCadastral.IsPjpfProprio = false;
                fichaCadastral.IsPjpfBaseProprio = preCadastroEnum == CasosPreCadastroEnum.PreCadastradoProprio;
                PopularFichaCadastral(preCadastroEnum, fichaCadastral);
                fichaCadastral.FornecedoresUnspsc =
                    Mapper.Map<List<FORNECEDORBASE_UNSPSC>, List<FornecedorUnspscVM>>(fornecedor.WFD_PJPF_BASE_UNSPSC.ToList());
                fichaCadastral.ChaveUrl = chaveUrl;
                return fichaCadastral;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return new FichaCadastralWebForLinkVM();
            }
        }

        private static void PopularFichaCadastral(CasosPreCadastroEnum preCadastroEnum, FichaCadastralWebForLinkVM fichaCadastralMapeada)
        {
            fichaCadastralMapeada.PreCadastroEnum = preCadastroEnum;
            fichaCadastralMapeada.DadosBancarios = fichaCadastralMapeada.DadosBancarios.Any() ? fichaCadastralMapeada.DadosBancarios : new List<DadosBancariosVM> { new DadosBancariosVM() { } };
            fichaCadastralMapeada.DadosContatos = fichaCadastralMapeada.DadosContatos.Any() ? fichaCadastralMapeada.DadosContatos : new List<DadosContatoVM>() { new DadosContatoVM() { } };
            fichaCadastralMapeada.DadosEnderecos = fichaCadastralMapeada.DadosEnderecos.Any() ? fichaCadastralMapeada.DadosEnderecos : new List<DadosEnderecosVM> { new DadosEnderecosVM() { } };
        }

        public List<FornecedorUnspscVM> PreencheModelUnspsc(int FornecedorId, string ServicosSelecionados, string MateriaisSelecionados, int? idSolicitacao)
        {
            string[] servicos = ServicosSelecionados.Split(new Char[] { '|' });
            string[] materiais = MateriaisSelecionados.Split(new Char[] { '|' });
            int[] vUnspsc = servicos.Concat(materiais).Where(x => !String.IsNullOrEmpty(x)).Select(int.Parse).ToArray();

            
            var unspscs = _unspscBP.BuscarListaPorID(vUnspsc);

            var listaUnspsc = new List<FornecedorUnspscVM>();

            unspscs.ForEach(x => listaUnspsc.Add(new FornecedorUnspscVM
            {
                Niv = (int)x.NIV,
                SolicitacaoId = idSolicitacao,
                UsnpscCodigo = x.UNSPSC_COD,
                UsnpscDescricao = x.UNSPSC_DSC,
                UsnpscId = x.ID,
                FornecedorId = FornecedorId
            }));

            return listaUnspsc;
        }
    }
}