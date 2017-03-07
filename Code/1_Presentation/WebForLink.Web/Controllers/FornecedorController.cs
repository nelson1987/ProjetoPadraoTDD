using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Web.ViewModels.OCP;

namespace WebForLink.Web.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly IFornecedorWebForLinkAppService _solicitacaoBp;

        public FornecedorController(IFornecedorWebForLinkAppService solicitacaoBp)
        {
            _solicitacaoBp = solicitacaoBp;
        }

        // GET: Fornecedor
        public ActionResult Index()
        {
            Fornecedor solicitacao = _solicitacaoBp.Buscar(x => x.ID == 39);
            WFD_CONTRATANTE_PJPF contratanteFornecedor = solicitacao.WFD_CONTRATANTE_PJPF.FirstOrDefault(x => x.CONTRATANTE_ID == 10);
            FornecedorFichaCadastralVM retorno = AutoMapper.Mapper.Map<FornecedorFichaCadastralVM>(contratanteFornecedor);
            DividirMateriaisServicos(retorno.Unspsc);
            var modelo = new FornecedorFichaCadastralVM(retorno.Gerais, retorno.DadosRobo, retorno.Enderecos,
                retorno.Bancarios, retorno.Contatos, retorno.Documentos, retorno.Unspsc,
                retorno.InformacaoComplementares)
            {
                Id = retorno.Id,
                IdContratante = retorno.IdContratante,
                IdContratanteFornecedor = retorno.IdContratanteFornecedor,
                IdPjPf = retorno.IdPjPf,
                IdPjPfBase = retorno.IdPjPfBase,
                IdSolicitacao = retorno.IdSolicitacao
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