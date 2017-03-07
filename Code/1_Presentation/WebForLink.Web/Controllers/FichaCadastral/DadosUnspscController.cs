using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class DadosUnspscController : ControllerPadrao
    {
        #region Chamadas Para BP
        private readonly IBancoWebForLinkAppService bancoService;
        private readonly IFornecedorServicoMaterialWebForLinkAppService pjpfUnspscService;
        private readonly IServicosMateriaisWebForLinkAppService _unspscBP;
        #endregion

        public DadosUnspscController(IBancoWebForLinkAppService banco, IFornecedorServicoMaterialWebForLinkAppService pjpfUnspsc, IServicosMateriaisWebForLinkAppService unspscBP)
        {
            bancoService = banco;
            pjpfUnspscService = pjpfUnspsc;
            _unspscBP = unspscBP;
        }

        #region Modificação Unspsc

        [HttpPost]
        public ActionResult EditarUnspsc(int fornecedorID)
        {
            var unspscs = pjpfUnspscService.BuscarPorFornecedorId(fornecedorID);

            PersistirDadosEmMemoria();

            return PartialView("_FichaCadastral_ServicosMaterias_Editavel", Mapper.Map<List<FORNECEDOR_UNSPSC>, List<FornecedorUnspscVM>>(unspscs.Where(x => x.DT_EXCLUSAO == null).ToList()));
        }

        [HttpPost]
        public ActionResult SalvarUnspsc(FichaCadastralWebForLinkVM model, string ServicosSelecionados, string MateriaisSelecionados)
        {
            //UNSPSC
            model.FornecedoresUnspsc = new List<FornecedorUnspscVM>();
            FornecedorUnspscVM unspscVM = new FornecedorUnspscVM();

            var unspscs = _unspscBP.BuscarListaPorID(ServicosSelecionados.Split(new Char[] { '|' }), MateriaisSelecionados.Split(new Char[] { '|' }));

            model.FornecedoresUnspsc = unspscVM.PreencheModelUnspsc(model.PJPFID, model.SolicitacaoID, unspscs);

            var unspsscs = Mapper.Map<List<FornecedorUnspscVM>, List<FORNECEDOR_UNSPSC>>(model.FornecedoresUnspsc.ToList());

            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            pjpfUnspscService.GravaUnspscNoPjPf(unspsscs, model.PJPFID, model.ContratanteID, usuarioId);
            PersistirDadosEmMemoria();

            return PartialView("_FichaCadastral_ServicosMaterias", model.FornecedoresUnspsc);
        }

        [HttpPost]
        public ActionResult CancelarUnspsc(int fornecedorID)
        {
            var unspscs = pjpfUnspscService.BuscarPorFornecedorId(fornecedorID);

            return PartialView("_FichaCadastral_ServicosMaterias", Mapper.Map<List<FORNECEDOR_UNSPSC>, List<FornecedorUnspscVM>>(unspscs));
        }

        #endregion

        private void PersistirDadosEmMemoria()
        {
            //ViewBag.Bancos
            if (TempData["Bancos"] == null)
                TempData["Bancos"] = new SelectList(bancoService.ListarTodosPorNome(), "ID", "BANCO_NM");

            ViewBag.Bancos = TempData["Bancos"] as SelectList;
            TempData.Keep("Bancos");
        }

    }
}