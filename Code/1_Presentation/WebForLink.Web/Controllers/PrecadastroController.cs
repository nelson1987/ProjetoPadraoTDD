using System.Collections.Generic;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Web.ViewModels.OCP;

namespace WebForLink.Web.Controllers
{
    public class PrecadastroController : Controller
    {
        private readonly IFornecedorBaseWebForLinkAppService _fornecedorBase;
        public PrecadastroController(IFornecedorBaseWebForLinkAppService fornBase)
        {
            _fornecedorBase = fornBase;
        }
        // GET: Precadastro
        public ActionResult Index()
        {
            PreCadastroFichaCadastralVM modelo;
            FORNECEDORBASE solicitacao = _fornecedorBase.Buscar(x => x.ID == 201);
            PreCadastroFichaCadastralVM retorno = AutoMapper.Mapper.Map<PreCadastroFichaCadastralVM>(solicitacao);
            DividirMateriaisServicos(retorno.Unspsc);
            modelo = new PreCadastroFichaCadastralVM(
                retorno.DadosRobo,
                retorno.Enderecos,
                retorno.Bancarios,
                retorno.Contatos,
                retorno.Unspsc,
                retorno.InformacaoComplementares)
            {
                Id = retorno.Id,
                IdContratante = retorno.IdContratante,
                IdContratanteFornecedor = retorno.IdContratanteFornecedor,
                IdPjPf = retorno.IdPjPf,
                IdPjPfBase = retorno.IdPjPfBase,
                IdSolicitacao = retorno.IdSolicitacao,
                TipoFluxo = retorno.TipoFluxo
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
    }
}