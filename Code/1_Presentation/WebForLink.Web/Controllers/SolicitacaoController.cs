using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.ViewModels.OCP;

namespace WebForLink.Web.Controllers
{
    [AllowAnonymous]
    public class SolicitacaoController : ControllerPadrao
    {
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoService;

        public SolicitacaoController(ISolicitacaoWebForLinkAppService solicitacao)
        {
            try
            {
                _solicitacaoService = solicitacao;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        // GET: Solicitacao
        public ActionResult Index()
        {
            var solicitacao = _solicitacaoService.Get(1293);
            var fluxo = solicitacao.FLUXO_ID;
            var retorno = Mapper.Map<SolicitacaoFichaCadastralVM>(solicitacao);
            DividirMateriaisServicos(retorno.Unspsc);
            var modelo = new SolicitacaoFichaCadastralVM(retorno.Gerais, retorno.DadosRobo, retorno.Enderecos,
                retorno.Bancarios, retorno.Contatos,
                retorno.Documentos, retorno.Unspsc, retorno.InformacaoComplementares)
            {
                Id = retorno.Id,
                IdContratante = retorno.IdContratante,
                IdContratanteFornecedor = retorno.IdContratanteFornecedor,
                IdPjPf = retorno.IdPjPf,
                IdPjPfBase = retorno.IdPjPfBase,
                IdSolicitacao = retorno.IdSolicitacao,
                TipoFluxo = retorno.TipoFluxo,
                DadosSolicitacao = retorno.DadosSolicitacao
            };
            return View(modelo);
        }

        public void DividirMateriaisServicos(List<UnspscFichaCadastralVM> dados)
        {
            foreach (var item in dados)
            {
                if (item.Id != 0)
                {
                    if (item.Id > 700000)
                        item.Materiais.Add(new UnspscMaterialFichaCadastralVM
                        {
                            IdUnspsc = item.Id,
                            Nome = item.Nome
                        });
                    else
                        item.Servicos.Add(new UnspscServicoFichaCadastralVM
                        {
                            IdUnspsc = item.Id,
                            Nome = item.Nome
                        });
                }
            }
        }

        [HttpPost]
        public JsonResult ConsultarSolicitacoesEmAberto(int contratanteId, int? fornecedorId, int? tipoFluxoId)
        {
            var solicitacoes = _solicitacaoService.BuscarPorContratanteFornecedorTipoFluxoEStatus(contratanteId,
                fornecedorId, (int) tipoFluxoId, (int) EnumStatusTramite.Aguardando);
            var jsonResult = new List<int>();

            if (solicitacoes.Any())
                jsonResult = solicitacoes;

            return Json(jsonResult);
        }
    }
}