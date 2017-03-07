using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Services;
using WebForLink.Web.Areas.Administracao.Models;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Controllers
{
    [WebForLinkFilter]
    public class UsuarioController : ControllerPadrao
    {
        #region Chamadas Para BP
        private readonly IPapelWebForLinkAppService papelBP;
        private readonly IUsuarioWebForLinkAppService usuarioBP;
        private readonly IContratanteWebForLinkAppService contratanteBP;
        private readonly IPerfilWebForLinkAppService perfilBP;
        #endregion
        public UsuarioController(IContratanteWebForLinkAppService contratante, 
            IPerfilWebForLinkAppService perfil,
            IPapelWebForLinkAppService papel, 
            IUsuarioWebForLinkAppService usuario) : base()
        {
            papelBP = papel;
            usuarioBP = usuario;
            contratanteBP = contratante;
            perfilBP = perfil;
        }

        [Authorize]
        public ActionResult UsuarioLst(UsuarioAdministracaoModel modelo)
        {
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            int pagina = modelo.Pagina ?? 1;
            ViewBag.Page = "Usuário";
            ViewBag.Title = "Lista de usuários";
            ViewBag.CONTRATANTE_ID = new SelectList(contratanteBP.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL", modelo.ContratanteId);
            GerenciarContasFiltrosDTO filtros = new GerenciarContasFiltrosDTO()
            {
                CPF = modelo.CPF,
                Login = modelo.Login,
                Email = modelo.Email,
                ContratanteId = modelo.ContratanteId,
                Ativo = modelo.Ativo,
                Administrador = modelo.Administrador
            };
            var pesquisaUsuario = usuarioBP.PesquisarUsuarios(filtros, pagina, 10);
            List<UsuarioAdministracaoModel> usuarioList = Mapper.Map<List<UsuarioAdministracaoModel>>(pesquisaUsuario.RegistrosPagina);

            usuarioList.ForEach(x =>
            {
                if (Request.Url == null) return;
                x.UrlEditar = Url.Action("UsuarioEditarFrm", "Usuario",
                    new
                    {
                        chaveurl = Cripto.Criptografar(string.Format("id={0}", x.Id), Key)
                    }, Request.Url.Scheme);
                x.UrlDetalhar = Url.Action("UsuarioDetalharFrm", "Usuario",
                    new
                    {
                        chaveurl = Cripto.Criptografar(string.Format("id={0}", x.Id), Key)
                    }, Request.Url.Scheme);
                x.UrlExcluir = Url.Action("Delete", "Usuario",
                    new
                    {
                        chaveurl = Cripto.Criptografar(string.Format("id={0}", x.Id), Key)
                    }, Request.Url.Scheme);
            }
            );
            return View(usuarioList);
        }

        [Authorize]
        public ActionResult UsuarioFrm(string chaveurl)
        {
            int idUsuario = 0;
            string Acao = "";

            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                Int32.TryParse(param.First(p => p.Name == "idUsuario").Value, out idUsuario);
                Acao = param.First(p => p.Name == "Acao").Value;
            }

            ViewBag.Acao = Acao;

            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            UsuarioAdministracaoModel modelo;
            ViewBag.CONTRATANTE_ID = new SelectList(contratanteBP.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL", contratanteId);

            //Incluir
            if (string.IsNullOrEmpty(Acao))
            {
                modelo = new UsuarioAdministracaoModel();
                modelo.SelectedGroupsPapel = new int[0];
                modelo.SelectedGroupsPerfil = new int[0];
            }
            else
            {
                modelo = Mapper.Map<UsuarioAdministracaoModel>(usuarioBP.BuscarPorId(idUsuario));
                if (modelo == null)
                {
                    return HttpNotFound();
                }

                modelo.SelectedGroupsPapel = modelo.PapelList.Select(x => x.Id).ToArray();
                modelo.SelectedGroupsPerfil = modelo.PerfilList.Select(x => x.Id).ToArray();
            }

            modelo.PapelList = papelBP.ListarTodos(contratanteId)
                    .Where(x => x.PAPEL_TP_ID == null)
                    .Select(x => new PapelAdministracaoModel
                    {
                        Id = x.ID,
                        Selecionado = modelo.SelectedGroupsPapel.Contains(x.ID),
                        Nome = x.PAPEL_NM,
                        Sigla = x.PAPEL_SGL
                    }).ToList();

            modelo.PerfilList = perfilBP.ListarTodosPorContratante(contratanteId)
                .Select(x => new PerfilAdministracaoModel
                {
                    Id = x.ID,
                    Nome = x.PERFIL_NM,
                    Selecionado = modelo.SelectedGroupsPerfil.Contains(x.ID),
                }).ToList();

            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UsuarioFrm(UsuarioAdministracaoModel modelo, string Acao)
        {
            ViewBag.Acao = Acao;
            try
            {
                if (Acao != "Excluir")
                {
                    if (modelo.Id == 0)
                    {
                        if (usuarioBP.VerificaLoginExistente(modelo.Login))
                        {
                            ModelState.AddModelError("", "O login já está sendo utilizado por outro usuário. Favor informar outro.");
                        }
                    }

                    if (!string.IsNullOrEmpty(modelo.Email))
                    {
                        if (!Validacao.ValidarEmail(modelo.Email))
                            ModelState.AddModelError("Email", "O e-mail informado não está em um formato válido.");
                    }

                    if (modelo.SelectedGroupsPerfil == null)
                        ModelState.AddModelError("ValidationPerfil", "Selecione ao Menos um Perfil.");
                }

                int grupoId = (int)Geral.PegaAuthTicket("Grupo");
                ViewBag.CONTRATANTE_ID = new SelectList(contratanteBP.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL", modelo.ContratanteId);

                if (ModelState.IsValid)
                {
                    if (modelo.Id == 0)
                    {
                        string chave = Path.GetRandomFileName().Replace(".", "");
                        modelo.TrocarSenha = chave;
                        modelo.Ativo = true;
                        modelo.DataCriacao = DateTime.Now;
                        modelo.PrimeiroAcesso = true;
                        modelo.ContaTentativa = 0;
                        modelo.Senha = PasswordHash.CreateHash(modelo.Login);
                        modelo.DataAtivacao = null;
                        modelo.Principal = false;
                        modelo.CPF = Mascara.RemoverMascaraCpfCnpj(modelo.CPF);

                        var usuario = Mapper.Map<Usuario>(modelo);
                        usuarioBP.IncluirUsuarioPadraoSenha(usuario, null, modelo.SelectedGroupsPapel, modelo.SelectedGroupsPerfil);
                        return RedirectToAction("GerenciarContasLst", "Usuario", new { MensagemSucesso = "Usuário criado com Sucesso!" });
                    }
                    else if (Acao == "Alterar")
                    {
                        usuarioBP.AlterarMinhaConta(Mapper.Map<Usuario>(modelo),
                            modelo.SelectedGroupsPapel,
                            modelo.SelectedGroupsPerfil,
                            modelo.ContratanteId);

                        return RedirectToAction("GerenciarContasLst", "Usuario", new { MensagemSucesso = "Usuário alterado com Sucesso!" });
                    }
                    else if (Acao == "Excluir")
                    {
                        usuarioBP.ExcluirUsuario(modelo.Id);

                        return RedirectToAction("GerenciarContasLst", "Usuario", new { MensagemSucesso = "Usuário Excluir com Sucesso!" });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                if (Acao == "Excluir")
                {
                    ModelState.AddModelError("", "Não é possível excluir este Usuário.");
                }
                else
                {
                    throw ex;
                }
            }
            return View(modelo);
        }

        [Authorize]
        public ActionResult GerenciarContasLst(int? Pagina, int? Contratantes, string Nome, string Login, string CPF, string MensagemSucesso)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            int UsuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            int pagina = Pagina ?? 1;

            ViewBag.Nome = Nome;
            ViewBag.Login = Login;
            ViewBag.CPF = CPF;
            ViewBag.Contratantes = Contratantes;

            GerenciarContasFiltrosDTO filtros = new GerenciarContasFiltrosDTO
            {
                UsuarioId = UsuarioId,
                ContratanteUsuario = contratanteId,
                Nome = Nome,
                Login = Login,
                CPF = CPF,
                ContratanteId = Contratantes,
                GrupoId = grupoId
            };
            var pesquisa = usuarioBP.PesquisarUsuarios(filtros, pagina, 10);

            List<UsuarioAdministracaoModel> usuarioList =  UsuarioAdministracaoModel.ModelToViewModel(pesquisa.RegistrosPagina, Url);
            ViewBag.MensagemSucesso = MensagemSucesso ?? "";
            ViewBag.Pagina = pagina;
            ViewBag.TotalPaginas = pesquisa.TotalPaginas;
            ViewBag.TotalRegistros = pesquisa.TotalRegistros;

            ViewBag.Contratantes = new SelectList(contratanteBP.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL");

            return View(usuarioList);
        }

        [Authorize]
        public ActionResult MinhaContaFrm(string MensagemSucesso)
        {
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            ViewBag.MensagemSucesso = MensagemSucesso ?? "";

            UsuarioAdministracaoModel modelo = UsuarioAdministracaoModel.ModelToViewModel(usuarioBP.BuscarPorId(usuarioId), Url);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            ViewBag.CONTRATANTE_ID = new SelectList(contratanteBP.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL", modelo.ContratanteId);

            modelo.PapelList = papelBP.ListarTodos(modelo.ContratanteId)
                .Where(x => x.PAPEL_TP_ID == null)
                .Select(x => new PapelAdministracaoModel
                {
                    Id = x.ID,
                    Selecionado = modelo.PapelList.FirstOrDefault(y => y.Id == x.ID) != null,
                    Nome = x.PAPEL_NM,
                    Sigla = x.PAPEL_SGL
                }).ToList();

            modelo.PerfilList = perfilBP.ListarTodosPorContratante(modelo.ContratanteId)
                .Select(x => new PerfilAdministracaoModel
                {
                    Id = x.ID,
                    Nome = x.PERFIL_NM,
                    Selecionado = modelo.PerfilList.FirstOrDefault(y => y.Id == x.ID) != null,
                }).ToList();

            modelo.SelectedGroupsPapel = modelo.PapelList.Where(x => x.Selecionado).Select(x => x.Id).ToArray();
            modelo.SelectedGroupsPerfil = modelo.PerfilList.Where(x => x.Selecionado).Select(x => x.Id).ToArray();

            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        public ActionResult MinhaContaFrm(UsuarioAdministracaoModel modelo)
        {
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            ViewBag.CONTRATANTE_ID = new SelectList(contratanteBP.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL", modelo.ContratanteId);
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = Mapper.Map<Usuario>(modelo);
                    usuarioBP.AlterarMinhaConta(usuario, modelo.SelectedGroupsPapel, modelo.SelectedGroupsPerfil, modelo.ContratanteId);
                    return RedirectToAction("MinhaContaFrm", new { MensagemSucesso = "Sua Conta foi alterada com sucesso!" });
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return View(modelo);
        }

    }
}