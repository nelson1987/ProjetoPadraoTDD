using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Areas.Administracao.Models;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Controllers
{
    public class ContratanteController : ControllerPadrao
    {
        #region Chamadas Para BP
        private readonly IContratanteConfiguracaoWebForLinkAppService _contratanteConfiguracaoBP;
        private readonly ITipoCadastroWebForLinkAppService _tipoCadastroBP;
        private readonly IContratanteWebForLinkAppService _contratanteBP;

        public ContratanteController(IContratanteConfiguracaoWebForLinkAppService contratanteConfiguracao, 
            IContratanteWebForLinkAppService contratante, 
            ITipoCadastroWebForLinkAppService tipoCadastro)
        {
            _contratanteConfiguracaoBP = contratanteConfiguracao;
            _tipoCadastroBP = tipoCadastro;
            _contratanteBP = contratante;
        }

        #endregion

        #region Index
        // GET: Contratante
        public ActionResult ContratanteLst(ContratanteAdministracaoModel modelo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int pagina = modelo.Pagina ?? 1;
            PesquisaContratanteFiltrosDTO filtros = new PesquisaContratanteFiltrosDTO()
            {
                ContratanteUsuario = contratanteId,
                CNPJ = modelo.CNPJ,
                RazaoSocial = modelo.RazaoSocial,
                NomeFantasia = modelo.NomeFantasia,
                Estilo = modelo.Estilo,
                ContratanteCodErp = modelo.ContranteCodERP,
                TipoCadastroId = modelo.TipoCadastroId
            };
            var pesquisa = _contratanteBP.PesquisarContratantes(filtros, pagina, 10);

            List<ContratanteAdministracaoModel> contratanteList =
                Mapper.Map<List<ContratanteAdministracaoModel>>(pesquisa.RegistrosPagina, opt => opt.Items["Url"] = Url);


            ViewBag.Page = "Contratante";
            ViewBag.Title = "Lista de Contratantes";
            ViewBag.MensagemSucesso = modelo.MensagemSucesso ?? "";
            ViewBag.Pagina = pagina;
            ViewBag.TotalPaginas = pesquisa.TotalPaginas;
            ViewBag.TotalRegistros = pesquisa.TotalRegistros;
            ViewBag.TIPO_CADASTRO_ID = new SelectList(_tipoCadastroBP.ListarTodos(), "ID", "NOME", modelo.TipoCadastroId);
            return View(contratanteList);
        }
        #endregion

        #region Detalhar
        // GET: Contratante/Details/5
        public ActionResult ContratanteDetalharFrm(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id;
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
            ContratanteAdministracaoModel modelo = Mapper.Map<ContratanteAdministracaoModel>(_contratanteBP.BuscarPorId(id), opt => opt.Items["Url"] = Url);

            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }
        #endregion

        #region Criação
        // GET: Contratante/Create
        public ActionResult ContratanteCriarFrm()
        {
            ViewBag.ID = new SelectList(_contratanteConfiguracaoBP.ListarTodos(), "CONTRATANTE_ID", "CONTRATANTE_ID");
            ViewBag.TIPO_CADASTRO_ID = new SelectList(_tipoCadastroBP.ListarTodos(), "ID", "NOME");
            return View();
        }
        // POST: Contratante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContratanteCriarFrm(ContratanteAdministracaoModel modelo)
        {
            if (ModelState.IsValid)
            {
                _contratanteBP.InserirContratante(Mapper.Map<Contratante>(modelo));
                return RedirectToAction("ContratanteLst");
            }

            ViewBag.ID = new SelectList(_contratanteConfiguracaoBP.ListarTodos(), "CONTRATANTE_ID", "CONTRATANTE_ID", modelo.Id);
            ViewBag.TIPO_CADASTRO_ID = new SelectList(_tipoCadastroBP.ListarTodos(), "ID", "NOME", modelo.TipoCadastroId);
            return View(modelo);
        }
        #endregion

        #region Edição
        // GET: Contratante/Edit/5
        public ActionResult ContratanteEditarFrm(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id;
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
            ContratanteAdministracaoModel modelo = Mapper.Map<ContratanteAdministracaoModel>(_contratanteBP.BuscarPorId(id), opt => opt.Items["Url"] = Url);
            if (modelo == null)
            {
                return HttpNotFound();
            }

            ViewBag.ID = new SelectList(_contratanteConfiguracaoBP.ListarTodos(), "CONTRATANTE_ID", "CONTRATANTE_ID", modelo.Id);
            ViewBag.TIPO_CADASTRO_ID = new SelectList(_tipoCadastroBP.ListarTodos(), "ID", "NOME", modelo.TipoCadastroId);
            return View(modelo);
        }

        // POST: Contratante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContratanteEditarFrm(ContratanteAdministracaoModel modelo)
        {
            if (ModelState.IsValid)
            {
                _contratanteBP.AlterarContratante(Mapper.Map<Contratante>(modelo));
                return RedirectToAction("ContratanteLst");
            }

            ViewBag.ID = new SelectList(_contratanteConfiguracaoBP.ListarTodos(), "CONTRATANTE_ID", "CONTRATANTE_ID", modelo.Id);
            ViewBag.TIPO_CADASTRO_ID = new SelectList(_tipoCadastroBP.ListarTodos(), "ID", "NOME", modelo.TipoCadastroId);
            return View(modelo);
        }
        #endregion

        #region Exclusão
        // GET: Contratante/Delete/5
        public ActionResult Delete(string chaveurl)
        {
            if (string.IsNullOrEmpty(chaveurl))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int id;
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
            ContratanteAdministracaoModel modelo = Mapper.Map<ContratanteAdministracaoModel>(_contratanteBP.BuscarPorId(id), opt => opt.Items["Url"] = Url);
            if (modelo == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        // POST: Contratante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Db.Contratante.Remove(Db.Contratante.Find(id));
            Db.SaveChanges();
            return RedirectToAction("ContratanteLst");
        }

        #endregion
    }
}