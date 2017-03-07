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
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Areas.Administracao.Models;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Controllers
{
    public class PerfilController : ControllerPadrao
    {
        #region Chamadas Para BP
        private readonly IFuncaoWebForLinkAppService _funcaoBP;
        private readonly IContratanteWebForLinkAppService _contratanteBP;
        private readonly IPerfilWebForLinkAppService _perfilBP;
        #endregion
        public PerfilController(IFuncaoWebForLinkAppService funcao, IContratanteWebForLinkAppService contratante, IPerfilWebForLinkAppService perfil)
        {
            _funcaoBP = funcao;
            _contratanteBP = contratante;
            _perfilBP = perfil;
        }

        public ActionResult PerfilLst(PerfilAdministracaoModel modelo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int pagina = modelo.Pagina ?? 1;
            PesquisaPerfilFiltrosDTO filtros = new PesquisaPerfilFiltrosDTO
            {
                ContratanteUsuario = contratanteId,
                Nome = modelo.Nome,
                Descricao = modelo.Descricao,
                ContratanteId = modelo.ContratanteId != 0 ? modelo.ContratanteId : contratanteId
            };
            var pesquisaPerfil = _perfilBP.PesquisarPerfil(filtros, pagina, 10);

            List<PerfilAdministracaoModel> perfilList = Mapper.Map<List<PerfilAdministracaoModel>>(pesquisaPerfil.RegistrosPagina, opt => opt.Items["Url"] = Url);

            int grupoId = (int)Geral.PegaAuthTicket("Grupo");

            ViewBag.MensagemSucesso = modelo.MensagemSucesso ?? "";
            ViewBag.Pagina = pagina;
            ViewBag.TotalPaginas = pesquisaPerfil.TotalPaginas;
            ViewBag.TotalRegistros = pesquisaPerfil.TotalRegistros;
            ViewBag.Page = "Perfil";
            ViewBag.Title = "Lista de Perfil";
            ViewBag.CONTRATANTE_ID = new SelectList(_contratanteBP.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL", modelo.ContratanteId);
            return View("PerfilLst", perfilList);
        }

        [Authorize]
        public ActionResult PerfilFrm(string chaveurl)
        {
            int id;
            string Acao;

            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                Int32.TryParse(param.First(p => p.Name == "idPerfil").Value, out id);
                Acao = param.First(p => p.Name == "Acao").Value;
            }
            else
            {
                id = 0;
                Acao = "Incluir";
            }


            ViewBag.Acao = Acao;

            PerfilAdministracaoModel modelo;
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            int ContratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            ViewBag.CONTRATANTE_ID = new SelectList(_contratanteBP.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL", ContratanteId);

            if (Acao == "Incluir")
            {
                modelo = new PerfilAdministracaoModel
                {
                    FuncaoList = _funcaoBP.ListarTodos(ContratanteId).Select(x => new FuncaoAdministracaoModel()
                    {
                        Id = x.ID,
                        Descricao = x.FUNCAO_DSC,
                        Nome = x.FUNCAO_NM,
                        AplicacaoId = 1,
                        FuncaoList = x.FUNCOES.Select(y => new FuncaoAdministracaoModel()
                        {
                            Id = y.ID,
                            Nome = y.FUNCAO_NM,
                            Tela = y.FUNCAO_TELA,
                            Codigo = y.CODIGO,
                            FuncaoList = y.FUNCOES.Select(z => new FuncaoAdministracaoModel()
                            {
                                Id = z.ID,
                                Nome = z.FUNCAO_NM,
                                Tela = z.FUNCAO_TELA,
                                Codigo = z.CODIGO
                            }).ToList()
                        }).ToList()
                    }).ToList()
                };
            }
            else
            {
                modelo = Mapper.Map<PerfilAdministracaoModel>(_perfilBP.BuscarPorId(id), opt => opt.Items["Url"] = Url);

                modelo.FuncaoList = _funcaoBP.ListarTodos(ContratanteId).OrderBy(x => x.FUNCAO_NM)
                    .Select(x => new FuncaoAdministracaoModel()
                    {
                        Id = x.ID,
                        Selecionado = modelo.FuncaoList.FirstOrDefault(y => y.Id == x.ID) != null,
                        Nome = x.FUNCAO_NM,
                        Tela = x.FUNCAO_TELA,
                        Codigo = x.CODIGO,
                        FuncaoList = x.FUNCOES.Select(y => new FuncaoAdministracaoModel()
                        {
                            Id = y.ID,
                            Selecionado = modelo.FuncaoList.FirstOrDefault(yy => yy.Id == y.ID) != null,
                            Nome = y.FUNCAO_NM,
                            Tela = y.FUNCAO_TELA,
                            Codigo = y.CODIGO,
                            FuncaoList = y.FUNCOES.Select(z => new FuncaoAdministracaoModel()
                            {
                                Id = z.ID,
                                Selecionado = modelo.FuncaoList.FirstOrDefault(zz => zz.Id == z.ID) != null,
                                Nome = z.FUNCAO_NM,
                                Tela = z.FUNCAO_TELA,
                                Codigo = z.CODIGO
                            }).ToList()
                        }).ToList()
                    }).ToList();
            }

            return View(modelo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PerfilFrm(PerfilAdministracaoModel modelo, string Acao)
        {
            ViewBag.Acao = Acao;
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            ViewBag.CONTRATANTE_ID = new SelectList(_contratanteBP.ListarTodos(grupoId), "ID", "RAZAO_SOCIAL", modelo.ContratanteId);

            try
            {
                if (ModelState.IsValid)
                {
                    var funcoes = modelo.FuncaoList.ToList();
                    modelo.FuncaoList.Clear();

                    foreach (var item in funcoes)
                    {
                        if (item.Selecionado) modelo.FuncaoList.Add(item);
                        foreach (var subitem in item.FuncaoList)
                        {
                            if (subitem.Selecionado) modelo.FuncaoList.Add(subitem);
                            foreach (var subsubitem in subitem.FuncaoList)
                            {
                                if (subsubitem.Selecionado) modelo.FuncaoList.Add(subsubitem);
                            }
                        }
                    }

                    modelo.FuncaoList.ForEach(x => x.FuncaoList = null);

                    if (modelo.Id == 0)
                    {
                        _perfilBP.InserirPerfilFuncoes(Mapper.Map<Perfil>(modelo));
                        return RedirectToAction("PerfilLst", "Perfil", new { MensagemSucesso = "Perfil incluído com Sucesso!" });
                    }
                    else if (Acao == "Alterar")
                    {
                        int[] idFuncoes = modelo.FuncaoList.Select(x => x.Id).ToArray();
                        _perfilBP.AlterarPerfil(Mapper.Map<Perfil>(modelo), idFuncoes);
                        return RedirectToAction("PerfilLst", "Perfil", new { MensagemSucesso = "Perfil alterado com Sucesso!" });
                    }
                    else if (Acao == "Excluir")
                    {
                        _perfilBP.ExcluirPerfil(modelo.Id);
                        return RedirectToAction("PerfilLst", "Perfil", new { MensagemSucesso = "Perfil excluído com Sucesso!" });
                    }

                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                switch (Acao)
                {
                    case "Excluir":
                        ModelState.AddModelError("", "Não é possível excluir este Perfil.");
                        break;
                    case "Alterar":
                        ModelState.AddModelError("", "Não foi possível alterar este Perfil.");
                        break;
                }
            }

            return View(modelo);
        }

    }
}