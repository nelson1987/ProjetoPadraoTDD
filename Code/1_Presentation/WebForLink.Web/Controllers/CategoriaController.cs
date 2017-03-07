using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.Novo;

namespace WebForLink.Web.Controllers
{
    [WebForLinkFilter]
    public class CategoriaController : ControllerPadrao
    {
        private readonly IFornecedorCategoriaWebForLinkAppService _fornecedorCategoriaService;
        private readonly IContratanteConfiguracaoWebForLinkAppService _contratanteConfiguracaoService;
        public CategoriaController(IFornecedorCategoriaWebForLinkAppService FornecedorCategoriaService,
            IContratanteConfiguracaoWebForLinkAppService contratanteConfiguracaoService)
        {
            _fornecedorCategoriaService = FornecedorCategoriaService;
            _contratanteConfiguracaoService = contratanteConfiguracaoService;
        }

        //Modelo de Mapper
        [Authorize]
        public ActionResult CategoriaLst(PesquisaCategoriaVM model)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int paginaAtual = model.Filtro.Pagina ?? 1;
            var listaCategorias = _fornecedorCategoriaService.PesquisarCategorias(model.Filtro.Descricao, model.Filtro.Codigo, contratanteId, TamanhoPagina, paginaAtual);

            //model.Grid = Mapper.Map<List<ListaPesquisaCategoriaVM>>(listaCategorias.RegistrosPagina, opt => opt.Items["Url"] = Url);
            model.Grid = ListaPesquisaCategoriaVM.ModelToViewModel(listaCategorias.RegistrosPagina, Url);

            model.Filtro.TotalNiveis = _contratanteConfiguracaoService.BuscarPorID(contratanteId).NIVEIS_CATEGORIA;
            model.Filtro.Paginacao = new PaginacaoModel(listaCategorias.TotalRegistros, paginaAtual, 10);

            //--Viewbag Paginação partial
            ViewBag.Pagina = paginaAtual;
            ViewBag.TotalPaginas = listaCategorias.TotalPaginas;
            ViewBag.TotalRegistros = listaCategorias.TotalRegistros;
            return View(model);
        }

        [Authorize]
        public ActionResult CategoriaFrm(string chaveurl)
        {
            try
            {
                int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
                int id = 0;
                string acao = "";

                if (!string.IsNullOrEmpty(chaveurl))
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                    Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
                    acao = param.First(p => p.Name == "Acao").Value;
                }

                CategoriaVM categoriaVM = new CategoriaVM();
                //Inclusão Categoria
                if (id == 0)
                    categoriaVM.Ativo = true;
                //Inclusão de SubCategoria
                else if (acao == "NovaSubcategoria")
                {
                    FORNECEDOR_CATEGORIA categoriaSelecionada = _fornecedorCategoriaService.BuscarPorId(id, contratanteId);
                    categoriaVM.PaiId = categoriaSelecionada.ID;
                    categoriaVM.DescricaoCategoriaPai = String.Concat(categoriaSelecionada.CODIGO, !String.IsNullOrEmpty(categoriaSelecionada.CODIGO) ? " - " : "", categoriaSelecionada.DESCRICAO);
                }
                //Alteração ou Exclusão de Categoria ou Subcategoria
                else
                    categoriaVM = Mapper.Map<CategoriaVM>(_fornecedorCategoriaService.BuscarPorId(id, contratanteId), opt => opt.Items["Url"] = Url);

                ViewBag.Acao = acao;
                return View(categoriaVM);
            }
            catch (ServiceWebForLinkException ex)
            {
                ModelState.AddModelError("", ex.Message);
                Log.Error(ex);
                return View(new CategoriaVM() { Ativo = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro durante sua requisição.");
                Log.Error(ex);
                return View(new CategoriaVM() { Ativo = true });
            }
        }

        //[SubtitleData]
        [Authorize]
        [HttpPost]
        public ActionResult CategoriaFrm(CategoriaVM model, string acao)
        {
            try
            {
                int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
                ViewBag.Acao = acao;

                if (acao == "Excluir")
                {
                    ModelState.Remove("Codigo");
                    ModelState.Remove("Descricao");
                }
                if (ModelState.IsValid)
                {
                    ModelState.Clear();

                    FORNECEDOR_CATEGORIA categoriaInserir = Mapper.Map<FORNECEDOR_CATEGORIA>(model);

                    //Inserção de Categoria
                    if (model.ID == 0)
                    {
                        categoriaInserir.CONTRATANTE_ID = contratanteId;
                        _fornecedorCategoriaService.InserirCategoria(categoriaInserir);
                        TempData["MensagemSucesso"] = "Inclusão realizada com sucesso!";
                    }
                    //Alteração de Categoria
                    else if (acao == "Alterar")
                    {
                        _fornecedorCategoriaService.AlterarCategoria(categoriaInserir);
                        TempData["MensagemSucesso"] = "Alteração realizada com sucesso!";
                    }
                    //Exclusão de Categoria
                    if (acao == "Excluir")
                    {
                        if (ModelState.IsValid)
                            _fornecedorCategoriaService.ExcluirCategoriaDireto(categoriaInserir);
                        TempData["MensagemSucesso"] = "Exclusão realizada com sucesso!";
                    }
                    return RedirectToAction("CategoriaLst", "Categoria");
                }
                return View(model);
            }
            catch (ServiceWebForLinkException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro durante sua requisição.");
                Log.Error(ex);
                return View(model);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult CategoriaFilhoLst(int idPai, int? nivel, int? totalNiveis, int tipoExibicao)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            List<FORNECEDOR_CATEGORIA> subCategorias = _fornecedorCategoriaService.BuscarPorCategoriaPai(idPai, contratanteId);

            List<CategoriaVM> modelo = Mapper.Map<List<CategoriaVM>>(subCategorias, opt => opt.Items["Url"] = Url);

            ViewBag.Nivel = nivel;
            ViewBag.ProximoNivel = nivel + 1;
            ViewBag.TotalNiveis = totalNiveis;
            ViewBag.TipoExibicao = tipoExibicao;

            return View(modelo);
        }
    }
}