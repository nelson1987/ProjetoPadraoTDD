using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Web.Interfaces;

namespace WebForLink.Web.Infrastructure
{
    public interface IAprovacao
    {
        void FinalizaSolicitacao(int tipoFluxoId, int solicitacaoId, int? grupoId);
    }
    public static class Aprovacao
    {
        private static IGeral _metodosGerais = new Geral();

        private static readonly IAprovacaoWebForLinkAppService _aprovacaoService;

        public static void FinalizaSolicitacao(int tipoFluxoId, int solicitacaoId, int? grupoId)
        {
            switch ((EnumTiposFluxo)tipoFluxoId)
            {
                case EnumTiposFluxo.CadastroFornecedorNacional:
                case EnumTiposFluxo.CadastroFornecedorEstrangeiro:
                case EnumTiposFluxo.CadastroFornecedorNacionalDireto:
                case EnumTiposFluxo.CadastroFornecedorPFDireto:
                case EnumTiposFluxo.CadastroFornecedorPF:
                    _aprovacaoService.FinalizarCriacaoFornecedor(solicitacaoId);
                    break;

                case EnumTiposFluxo.AmpliacaoFornecedor:
                    _aprovacaoService.FinalizarExpansao(solicitacaoId);
                    break;

                case EnumTiposFluxo.ModificacaoDadosBancarios:
                    _aprovacaoService.FinalizarModificacaoDadosBancarios(solicitacaoId);
                    break;

                case EnumTiposFluxo.ModificacaoEndereco:
                    _aprovacaoService.FinalizarModificacaoDadosEnderecos(solicitacaoId);
                    break;

                case EnumTiposFluxo.ModificacaoDadosContato:
                    _aprovacaoService.FinalizarModificacaoDadosContatos(solicitacaoId);
                    break;

                case EnumTiposFluxo.BloqueioFornecedor:
                    _aprovacaoService.FinalizarBloqueio(solicitacaoId, grupoId);
                    break;

                case EnumTiposFluxo.DesbloqueioFornecedor:
                    _aprovacaoService.FinalizarDesbloqueio(solicitacaoId, grupoId);
                    break;

                default:
                    break;
            }
        }
    }
}