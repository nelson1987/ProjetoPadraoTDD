using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Areas.Administracao.Models;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Controllers
{
    public class AplicacaoController : ControllerPadrao
    {
        #region Chamadas Para BP
        private readonly IAplicacaoWebForLinkAppService _aplicacaoBP;
        #endregion
        public AplicacaoController(IAplicacaoWebForLinkAppService aplicacao)
        {
            _aplicacaoBP = aplicacao;
        }

        #region Index
        // GET: Administracao/Aplicacao
        public ActionResult AplicacaoLst(AplicacaoAdministracaoModel modelo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int pagina = modelo.Pagina ?? 1;
            PesquisaAplicacaoFiltrosDTO filtros = new PesquisaAplicacaoFiltrosDTO()
            {
                ContratanteUsuario = contratanteId,
                Nome = modelo.Nome,
                Descricao = modelo.Descricao
            };
            var pesquisa = _aplicacaoBP.PesquisarAplicacao(filtros, pagina, 10);
            IList<AplicacaoAdministracaoModel> aplicacaoList = Mapper.Map<IList<AplicacaoAdministracaoModel>>(pesquisa.RegistrosPagina, opt => opt.Items["Url"] = Url);
            ViewBag.Page = "Aplicação";
            ViewBag.Title = "Lista de Aplicações";
            ViewBag.MensagemSucesso = modelo.MensagemSucesso ?? "";
            ViewBag.Pagina = pagina;
            ViewBag.TotalPaginas = pesquisa.TotalPaginas;
            ViewBag.TotalRegistros = pesquisa.TotalRegistros;
            return View(aplicacaoList);
        }
        #endregion

        #region Detalhar
        // GET: Administracao/Aplicacao/Details/5
        public ActionResult AplicacaoDetalharFrm(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id;
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "idAplicacao").Value, out id);
            AplicacaoAdministracaoModel modelo = Mapper.Map<AplicacaoAdministracaoModel>(_aplicacaoBP.BuscarPorId(id), opt => opt.Items["Url"] = Url);

            if (modelo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Page = "Aplicação";
            ViewBag.Title = "Detalhar Aplicação";
            return View(modelo);
        }

        #endregion

        #region Criação
        // GET: Administracao/Aplicacao/Create
        public ActionResult AplicacaoCriarFrm()
        {
            ViewBag.Page = "Aplicação";
            ViewBag.Title = "Criação de aplicação";
            return View();
        }

        // POST: Administracao/Aplicacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AplicacaoCriarFrm(AplicacaoAdministracaoModel modelo)
        {
            ModelState.Remove("ID");
            if (string.IsNullOrEmpty(modelo.Nome))
                ModelState.AddModelError(modelo.Nome, "Nome da aplicação é um campo obrigatório.");

            if (ModelState.IsValid)
            {
                if (_aplicacaoBP.BuscarPorNome(modelo.Nome) != null)
                    ModelState.AddModelError(modelo.Nome, "Nome da aplicação já existente em nossa base.");

                if (ModelState.IsValid)
                {
                    _aplicacaoBP.InserirAplicacao(Mapper.Map<APLICACAO>(modelo));
                    return RedirectToAction("AplicacaoLst");
                }
            }

            ViewBag.Page = "Aplicação";
            ViewBag.Title = "Criação de aplicação";
            return View(modelo);
        }

        #endregion

        #region Edição
        // GET: WAC_APLICACAO/Edit/5
        public ActionResult AplicacaoEditarFrm(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id;
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "idAplicacao").Value, out id);
            AplicacaoAdministracaoModel modelo = Mapper.Map<AplicacaoAdministracaoModel>(_aplicacaoBP.BuscarPorId(id), opt => opt.Items["Url"] = Url);

            if (modelo == null)
            {
                return HttpNotFound();
            }

            ViewBag.Page = "Aplicação";
            ViewBag.Title = "Edição de aplicação";
            return View(modelo);
        }

        // POST: Administracao/Aplicacao/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AplicacaoEditarFrm(AplicacaoAdministracaoModel modelo)
        {
            try
            {
                if (string.IsNullOrEmpty(modelo.Nome))
                    ModelState.AddModelError(modelo.Nome, "Nome da aplicação é um campo obrigatório.");

                if (ModelState.IsValid)
                {
                    if (_aplicacaoBP.BuscarPorIdNomeAplicacao(modelo.Id, modelo.Nome) != null)
                        ModelState.AddModelError(modelo.Nome, "Nome da aplicação já existente em nossa base.");

                    if (ModelState.IsValid)
                    {
                        _aplicacaoBP.AlterarAplicacao(Mapper.Map<APLICACAO>(modelo));
                        return RedirectToAction("AplicacaoLst");
                    }
                }

                ViewBag.Page = "Aplicação";
                ViewBag.Title = "Edição de aplicação";
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
                ViewBag.MensagemErro = "Erro ao tentar editar uma aplicação!";
            }
            return View(modelo);
        }

        #endregion

        #region Exclusão
        // GET: Administracao/Aplicacao/Delete/5
        public ActionResult Delete(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id;
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "idAplicacao").Value, out id);
            AplicacaoAdministracaoModel modelo = Mapper.Map<AplicacaoAdministracaoModel>(_aplicacaoBP.BuscarPorId(id), opt => opt.Items["Url"] = Url);

            if (modelo == null)
            {
                return HttpNotFound();
            }

            ViewBag.Page = "Aplicação";
            ViewBag.Title = "Exclusão de aplicação";
            return View(modelo);
        }

        // POST: Administracao/Aplicacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _aplicacaoBP.ExcluirAplicacao(id);
            return RedirectToAction("AplicacaoLst");
        }

        #endregion
    }
}