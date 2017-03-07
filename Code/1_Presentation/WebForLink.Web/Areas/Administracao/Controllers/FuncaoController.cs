using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Areas.Administracao.Models;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Controllers
{
    public class FuncaoController : ControllerPadrao
    {
        #region Chamadas Para BP
        private readonly IFuncaoWebForLinkAppService funcaoBP;
        private readonly IAplicacaoWebForLinkAppService aplicacaoBP;
        private readonly IPerfilWebForLinkAppService perfilBP;
        public FuncaoController(IFuncaoWebForLinkAppService funcao, IAplicacaoWebForLinkAppService aplicacao, IPerfilWebForLinkAppService perfil)
        {
            funcaoBP = funcao;
            aplicacaoBP = aplicacao;
            perfilBP = perfil;
        }
        #endregion

        // GET: WAC_FUNCAO
        public ActionResult FuncaoLst(FuncaoAdministracaoModel modelo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int pagina = modelo.Pagina ?? 1;

            PesquisaFuncaoFiltrosDTO filtros = new PesquisaFuncaoFiltrosDTO()
            {
                ContratanteUsuario = contratanteId,
                Aplicacao = modelo.AplicacaoId,
                Codigo = modelo.Codigo,
                Descricao = modelo.Descricao,
                Nome = modelo.Nome,
                PaiFuncao = modelo.FuncaoPaiId,
                Tela = modelo.Tela

            };
            var pesquisa = funcaoBP.PesquisarFuncao(filtros, pagina, 10, contratanteId);
            IList<FuncaoAdministracaoModel> listUsuarioAdmin = Mapper.Map<IList<FuncaoAdministracaoModel>>(pesquisa.RegistrosPagina, opt => opt.Items["Url"] = Url);

            ViewBag.MensagemSucesso = modelo.MensagemSucesso ?? "";
            ViewBag.Pagina = pagina;
            ViewBag.TotalPaginas = pesquisa.TotalPaginas;
            ViewBag.TotalRegistros = pesquisa.TotalRegistros;
            ViewBag.Page = "Função";
            ViewBag.Title = "Lista de funções";
            ViewBag.APLICACAO_ID = new SelectList(aplicacaoBP.ListarTodos(), "ID", "APLICACAO_NM", modelo.AplicacaoId);
            ViewBag.FUNCAO_PAI = new SelectList(funcaoBP.ListarTodos(contratanteId), "ID", "FUNCAO_NM", modelo.FuncaoPaiId);
            return View(listUsuarioAdmin);
        }

        // GET: WAC_FUNCAO/Details/5
        public ActionResult FuncaoDetalharFrm(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id;
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
            FuncaoAdministracaoModel modelo = Mapper.Map<FuncaoAdministracaoModel>(funcaoBP.BuscarPorID(id), opt => opt.Items["Url"] = Url);

            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        // GET: WAC_FUNCAO/Create
        public ActionResult FuncaoCriarFrm()
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            ViewBag.Page = "Função";
            ViewBag.Title = "Criar função";
            ViewBag.PERFIL_ID = new SelectList(perfilBP.ListarTodos(), "ID", "PERFIL_NM");
            ViewBag.APLICACAO_ID = new SelectList(aplicacaoBP.ListarTodos(), "ID", "APLICACAO_NM");
            ViewBag.FUNCAO_PAI = new SelectList(funcaoBP.ListarTodos(contratanteId), "ID", "FUNCAO_NM");
            return View();
        }

        // POST: WAC_FUNCAO/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FuncaoCriarFrm(FuncaoAdministracaoModel modelo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            ModelState.Remove("ID");
            if (string.IsNullOrEmpty(modelo.Codigo))
                ModelState.AddModelError(modelo.Codigo, "Código da função é um campo obrigatório.");

            if (ModelState.IsValid)
            {
                if (funcaoBP.BuscarPorCodigo(modelo.Codigo) != null)
                    ModelState.AddModelError(modelo.Codigo, "Código da função já existente em nossa base.");

                if (ModelState.IsValid)
                {
                    funcaoBP.InserirFuncao(Mapper.Map<FUNCAO>(modelo));
                    return RedirectToAction("FuncaoLst");
                }
            }
            ViewBag.Page = "Função";
            ViewBag.Title = "Criar função";
            ViewBag.PERFIL_ID = new SelectList(perfilBP.ListarTodos(), "ID", "PERFIL_NM", modelo.PerfilId);
            ViewBag.APLICACAO_ID = new SelectList(aplicacaoBP.ListarTodos(), "ID", "APLICACAO_NM", modelo.AplicacaoId);
            ViewBag.FUNCAO_PAI = new SelectList(funcaoBP.ListarTodos(contratanteId), "ID", "FUNCAO_NM", modelo.FuncaoPaiId);
            return View(modelo);
        }

        // GET: WAC_FUNCAO/Edit/5
        public ActionResult FuncaoEditarFrm(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id;
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "id").Value, out id);

            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            FuncaoAdministracaoModel modelo = Mapper.Map<FuncaoAdministracaoModel>(funcaoBP.BuscarPorID(id), opt => opt.Items["Url"] = Url);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            ViewBag.Page = "Função";
            ViewBag.Title = "Editar função";

            ViewBag.PERFIL_ID = new SelectList(perfilBP.ListarTodos(), "ID", "PERFIL_NM", modelo.PerfilId);
            ViewBag.APLICACAO_ID = new SelectList(aplicacaoBP.ListarTodos(), "ID", "APLICACAO_NM", modelo.AplicacaoId);
            ViewBag.FUNCAO_PAI = new SelectList(funcaoBP.ListarTodos(contratanteId), "ID", "FUNCAO_NM", modelo.FuncaoPaiId);
            return View(modelo);
        }

        // POST: WAC_FUNCAO/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FuncaoEditarFrm(FuncaoAdministracaoModel modelo)
        {
            ViewBag.Page = "Função";
            ViewBag.Title = "Editar função";
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            if (ModelState.IsValid)
            {
                funcaoBP.AlterarFuncao(Mapper.Map<FUNCAO>(modelo));
                return RedirectToAction("FuncaoLst");
            }

            ViewBag.PERFIL_ID = new SelectList(perfilBP.ListarTodos(), "ID", "PERFIL_NM", modelo.PerfilId);
            ViewBag.APLICACAO_ID = new SelectList(aplicacaoBP.ListarTodos(), "ID", "APLICACAO_NM", modelo.AplicacaoId);
            ViewBag.FUNCAO_PAI = new SelectList(funcaoBP.ListarTodos(contratanteId), "ID", "FUNCAO_NM", modelo.FuncaoPaiId);
            return View(modelo);
        }

        // GET: WAC_FUNCAO/Delete/5
        public ActionResult Delete(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id;
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "id").Value, out id);

            FuncaoAdministracaoModel modelo = Mapper.Map<FuncaoAdministracaoModel>(funcaoBP.BuscarPorID(id), opt => opt.Items["Url"] = Url);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        // POST: WAC_FUNCAO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Db.WAC_FUNCAO.Remove(Db.WAC_FUNCAO.Find(id));
            Db.SaveChanges();
            return RedirectToAction("FuncaoLst");
        }

    }
}