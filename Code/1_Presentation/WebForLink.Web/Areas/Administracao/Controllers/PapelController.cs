using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Areas.Administracao.Models;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Exceptions;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Controllers
{
    public class PapelController : ControllerPadrao
    {
        #region Chamadas Para BP
        private readonly PapelWebForLinkAppService _papelService;
        private readonly IContratanteWebForLinkAppService _contratanteService;
        #endregion

        public PapelController(PapelWebForLinkAppService papel, IContratanteWebForLinkAppService contratante)
        {
            _papelService = papel;
            _contratanteService = contratante;
        }
        // GET: Papel
        public ActionResult PapelLst(PapelAdministracaoModel modelo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int pagina = modelo.Pagina ?? 1;
            PesquisaPapelFiltrosDTO filtros = new PesquisaPapelFiltrosDTO()
            {
                ContratanteUsuario = contratanteId,
                Nome = modelo.Nome,
                Sigla = modelo.Sigla,
                ContratanteId = modelo.ContratanteId
            };
            var novoCategoria = _papelService.PesquisarPapel(filtros, pagina, 10);
            List<PapelAdministracaoModel> papeisList =
                Mapper.Map<List<PapelAdministracaoModel>>(novoCategoria.RegistrosPagina, opt => opt.Items["Url"] = Url);
            papeisList.ForEach(x =>
            {
                if (Request.Url == null) return;
                x.UrlEditar = Url.Action("PapelFrm", new
                {
                    chaveurl = Cripto.Criptografar(string.Format("idPapel={0}&Acao=Alterar", x.Id), Key)
                });
                x.UrlExcluir = Url.Action("PapelFrm", new
                {
                    chaveurl = Cripto.Criptografar(string.Format("idPapel={0}&Acao=Excluir", x.Id), Key)
                });
            });
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");

            ViewBag.MensagemSucesso = modelo.MensagemSucesso ?? "";
            ViewBag.Pagina = pagina;
            ViewBag.TotalPaginas = novoCategoria.TotalPaginas;
            ViewBag.TotalRegistros = novoCategoria.TotalRegistros;
            ViewBag.Page = "Papel";
            ViewBag.Title = "Lista de Papel";
            ViewBag.CONTRATANTE_ID = new SelectList(_contratanteService.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL", modelo.ContratanteId);
            return View(papeisList);
        }

        [Authorize]
        public ActionResult PapelFrm(string chaveurl)
        {
            #region MyRegion
            int idPapel = 0;
            string Acao = "";

            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                Int32.TryParse(param.First(p => p.Name == "idPapel").Value, out idPapel);
                Acao = param.First(p => p.Name == "Acao").Value;
            }
            ViewBag.Acao = Acao;

            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            PapelAdministracaoModel modelo;
            ViewBag.CONTRATANTE_ID = new SelectList(_contratanteService.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL", contratanteId);

            //Incluir
            if (string.IsNullOrEmpty(Acao))
            {
                modelo = new PapelAdministracaoModel();
            }
            else
            {
                modelo = Mapper.Map<PapelAdministracaoModel>(_papelService.BuscarPorID(idPapel), opt => opt.Items["Url"] = Url);
                if (modelo == null)
                {
                    return HttpNotFound();
                }
            }

            return View(modelo);

            #endregion
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PapelFrm(PapelAdministracaoModel modelo, string Acao)
        {
            ViewBag.Acao = Acao;
            try
            {
                if (Acao != "Excluir")
                {
                }

                int grupoId = (int)Geral.PegaAuthTicket("Grupo");
                ViewBag.CONTRATANTE_ID = new SelectList(_contratanteService.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL", modelo.ContratanteId);

                if (ModelState.IsValid)
                {
                    if (modelo.Id == 0)
                    {
                        Papel papel = _papelService.BuscarPorSigla(modelo.Sigla);
                        _papelService.InserirPapel(Mapper.Map<Papel>(modelo));
                        return RedirectToAction("PapelLst", "Papel", new { MensagemSucesso = "Papel criado com Sucesso!" });
                    }
                    else if (Acao == "Alterar")
                    {
                        _papelService.AlterarPapel(Mapper.Map<Papel>(modelo));
                        return RedirectToAction("PapelLst", "Papel", new { MensagemSucesso = "Papel alterado com Sucesso!" });
                    }
                    else if (Acao == "Excluir")
                    {
                        _papelService.ExcluirPapel(modelo.Id);
                        return RedirectToAction("PapelLst", "Papel", new { MensagemSucesso = "Papel excluir com Sucesso!" });
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Log.Error(
                        string.Format(
                            "A entidade do Tipo \"{0}\" no estado \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Log.Error(string.Format("- Propriedade : \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage));
                    }
                }
                ViewBag.MensagemErro = "Erro ao tentar incluir um novo Papel!";
                throw new WebForLinkException("Erro ao tentar incluir um novo Papel!");
            }
            catch (Exception ex)
            {
                Log.Error(ex);

                if (Acao == "Excluir")
                {
                    ModelState.AddModelError("", "Não Foi possível excluir este Usuário.");
                }
                else
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(modelo);
        }
    }
}