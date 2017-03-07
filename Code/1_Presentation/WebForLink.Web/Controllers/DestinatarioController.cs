using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers
{
    [WebForLinkFilter]
    public class DestinatarioController : ControllerPadrao
    {
        private readonly IDestinatarioWebForLinkAppService _bpDestinatario;
        public DestinatarioController(IDestinatarioWebForLinkAppService destinatario)
        {
            _bpDestinatario = destinatario;
        }

        [Authorize]
        public ActionResult DestinatarioLst(DestinatariosPesquisaVM modelo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            int pagina = modelo.Pagina ?? 1;

            Expression<Func<DESTINATARIO, bool>> filtro = Predicativos.FiltrarDestinatarioGrid(modelo, contratanteId);
            RetornoPesquisa<DESTINATARIO> listaPesquisa = new RetornoPesquisa<DESTINATARIO>();
            Func<DESTINATARIO, IComparable> ordenacao = (DESTINATARIO a) => a.NOME;

            listaPesquisa = _bpDestinatario.BuscarPesquisa(filtro, TamanhoPagina, pagina, ordenacao);
            modelo.DestinatarioGrid = DestinatariosVM.ModelToViewModel(listaPesquisa.RegistrosPagina, Url);
            ViewBag.TotalPaginas = listaPesquisa.TotalPaginas;
            ViewBag.TotalRegistros = listaPesquisa.TotalRegistros;
            ViewBag.Pagina = pagina;

            return View(modelo);
        }

        [Authorize]
        public ActionResult Incluir()
        {
            ViewBag.Acao = "Alterar";
            return View("DestinatarioFrm", new DestinatariosVM());
        }

        [Authorize]
        public ActionResult Editar(string chaveurl)
        {
            DestinatariosVM destinatario = RetornarModeloEdicao(chaveurl);
            ViewBag.Acao = "Alterar";
            return View("DestinatarioFrm", destinatario);
        }

        [Authorize]
        public ActionResult Excluir(string chaveurl)
        {
            DestinatariosVM destinatario = RetornarModeloEdicao(chaveurl);
            ViewBag.Acao = "Excluir";
            return View("DestinatarioFrm", destinatario);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DestinatarioFrm(DestinatariosVM model, string Acao)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            DESTINATARIO destinatario;

            if (ModelState.IsValid)
            {
                //INCLUSÃO DE DESTINATÁRIO
                if (model.ID == 0)
                {
                    if (!ValidarSeOEmailJaEstaSendoUtilizado(model.Email))
                    {
                        if (!ValidarSeHaCompartilhamentosAtivos(model.Email))
                        {
                            return CriarInclusaoDestinatario(model, contratanteId, out destinatario, _bpDestinatario);
                        }
                        ModelState.AddModelError("Email", "Erro ao tentar incluir o destinatário, este e-mail contém compartilhamentos ativos!");
                    }
                    ModelState.AddModelError("Email", "Erro ao tentar incluir o destinatário, este e-mail já encontra-se nos destinatários!");
                }
                else if (Acao == "Alterar")
                {
                    destinatario = _bpDestinatario.BuscarPorId(model.ID);
                    if (destinatario != null)
                    {
                        if (!ValidarSeHaCompartilhamentosAtivos(destinatario.EMAIL))
                        {
                            return CriarAlteracaoDestinatario(model, contratanteId, destinatario);
                        }
                        ModelState.AddModelError("Email", "Erro ao tentar incluir o destinatário, este e-mail contém compartilhamentos ativos!");
                    }
                    else
                        ModelState.AddModelError("", "Erro ao tentar alterar o destinatário, identificador não encontrado!");
                }
            }

            if (Acao == "Excluir")
            {
                destinatario = _bpDestinatario.BuscarPorId(model.ID);
                if (destinatario != null)
                {
                    if (!ValidarSeHaCompartilhamentosAtivos(destinatario.EMAIL))
                    {
                        _bpDestinatario.Excluir(destinatario);
                        TempData["MensagemSucesso"] = "Exclusão realizada com sucesso!";
                        return RedirectToAction("DestinatarioLst");
                    }
                    ModelState.AddModelError("Email", "Erro ao tentar incluir o destinatário, este e-mail contém compartilhamentos ativos!");
                }
                else
                    ModelState.AddModelError("", "Erro ao tentar alterar o destinatário, identificador não encontrado!");

            }
            return View(model);
        }

        private bool ValidarSeOEmailJaEstaSendoUtilizado(string email)
        {
            return _bpDestinatario.ValidarEmailDuplicado(email);
        }
        private bool ValidarSeHaCompartilhamentosAtivos(string email)
        {
            return _bpDestinatario.ValidarSeHaCompartilhamentosAtivos(email);
        }
        private ActionResult CriarAlteracaoDestinatario(DestinatariosVM model, int contratanteId, DESTINATARIO destinatario)
        {
            destinatario.CONTRATANTE_ID = contratanteId;
            destinatario.EMAIL_AVULSO = false;
            destinatario.NOME = model.Nome;
            destinatario.EMAIL = model.Email;
            destinatario.EMPRESA = model.Empresa;
            destinatario.OBS = model.Obs;
            destinatario.ATIVO = model.Ativo;
            destinatario.SOBRENOME = model.Sobrenome;
            destinatario.TELEFONE_FIXO = Mascara.RemoverMascaraTelefone(model.TelefoneFixo);
            destinatario.CELULAR = Mascara.RemoverMascaraTelefone(model.Celular);
            destinatario.TELEFONE_TRABALHO = Mascara.RemoverMascaraTelefone(model.TelefoneTrabalho);
            destinatario.FAX = Mascara.RemoverMascaraTelefone(model.Fax);
            //ViewBag.MensagemSucesso = "Alteração realizada com sucesso!";
            _bpDestinatario.Editar(destinatario);
            TempData["MensagemSucesso"] = "Alteração realizada com sucesso!";
            return RedirectToAction("DestinatarioLst");
        }

        private ActionResult CriarInclusaoDestinatario(DestinatariosVM model, int contratanteId, out DESTINATARIO destinatario, IDestinatarioWebForLinkAppService bpDestinatario)
        {
            destinatario = Mapper.Map<DESTINATARIO>(model);
            destinatario.CONTRATANTE_ID = contratanteId;
            destinatario.ATIVO = true;
            bpDestinatario.Incluir(destinatario);
            TempData["MensagemSucesso"] = "Inclusão realizada com sucesso!";
            return RedirectToAction("DestinatarioLst");
        }

        private DestinatariosVM RetornarModeloEdicao(string chaveurl)
        {
            int id = 0;

            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
            }
            DestinatariosVM destinatario = Mapper.Map<DestinatariosVM>(_bpDestinatario.BuscarPorId(id), opt => opt.Items["Url"] = Url);
            return destinatario;
        }

    }
}