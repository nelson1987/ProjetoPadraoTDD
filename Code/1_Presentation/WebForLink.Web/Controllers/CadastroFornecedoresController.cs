using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Services;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers
{
    public class CadastroFornecedoresController : ControllerPadrao
    {
        public readonly ISolicitacaoCadastroFornecedorWebForLinkAppService _solicitacaoCadastroFornecedorService;
        public readonly IContratanteConfiguracaoWebForLinkAppService _contratanteConfiguracaoBP;
        public readonly IUsuarioWebForLinkAppService _usuarioBP;

        public CadastroFornecedoresController(
            ISolicitacaoCadastroFornecedorWebForLinkAppService solicitacaoCadastro,
            IContratanteConfiguracaoWebForLinkAppService contratanteConfiguracao,
            IUsuarioWebForLinkAppService usuario)
            :base()
        {
            _solicitacaoCadastroFornecedorService = solicitacaoCadastro;
            _contratanteConfiguracaoBP = contratanteConfiguracao;
            _usuarioBP = usuario;
        }
        // GET: CadastroFornecedores
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
                TextoTermoAceite = _contratanteConfiguracaoBP.BuscarPorIdSolicitacao(idSolicitacao).TERMO_ACEITE
            };

            acesso.TextoTermoAceite = acesso.TextoTermoAceite
                    .Replace("^NomeFornecedor^", acesso.NomeFornecedor)
                    .Replace("^NomeEmpresa^", acesso.NomeEmpresa);

            if (_usuarioBP.VerificaLoginExistente(acesso.DocumentoPjPf))
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
                        Usuario usuarioMapeado = new Usuario
                        {
                            EMAIL = modelo.Email,
                            SENHA = PasswordHash.CreateHash(modelo.Senha),
                            ATIVO = true,
                            PRIMEIRO_ACESSO = false,
                            LOGIN = modelo.DocumentoPjPf,
                            CPF_CNPJ = modelo.DocumentoPjPf,
                            DT_ATIVACAO = DateTime.Now,
                            DT_CRIACAO = DateTime.Now,
                            CONTA_TENTATIVA = 1,
                            CONTRATANTE_ID = null,
                            PRINCIPAL = false,
                            EMAIL_ALTERNATIVO = modelo.EmailAlternativo
                        };
                        var senhaMapeada = Mapper.Map<USUARIO_SENHAS>(usuarioMapeado);
                        try
                        {
                            _usuarioBP.IncluirUsuarioPadraoSenha(usuarioMapeado, senhaMapeada, new int[0], new int[0]);
                            return RedirectToActionPermanent("Acesso","Home",new AcessoVM
                            {
                                Email = modelo.Email,
                                Login = modelo.DocumentoPjPf,
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
        [HttpPost]
        public ActionResult ExibirTermoAceite(CadastrarUsuarioFornecedorVM modelo)
        {
            //verificar se o usuário já realizou termo de aceito em tabela Usuario
            //Se o contratante que ele pertence tem um termo de aceite na tabela WFD_CONTRATANTE_CONFIG
            //Se o contratante que ele pertence não tiver exibir o termo de aceite de contratanteId = 1 - CHConsultoria, que será o termo de aceite padrão
            //se ele aceitar o termo direciona pra página devida;
            //do contrário volta a página sem seguir

            return RedirectToActionPermanent("Acesso", "Home", new AcessoVM
            {
                Email = modelo.Email,
                Login = modelo.DocumentoPjPf,
                Senha = modelo.Senha,
                SolicitacaoId = modelo.SolicitacaoId
            });
        }


        private void ValidarPoliticaSenha(TrocaSenhaEsqueceuVM model)
        {
            if ((model.Senha.Length < 8) || (model.ConfirmaSenha.Length < 8))
            {
                ModelState.AddModelError("model.Senha", "As senhas devem ter no mínimo 8 caracteres");
                ModelState.AddModelError("model.ConfirmaSenha", "As senhas devem ter no mínimo 8 caracteres");
            }
        }
    }
}