using Ninject.Modules;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services;
using WebForLink.Domain.Services.ChMasterData;
using WebForLink.Domain.Services.Common;
using WebForLink.Domain.Services.Process;

namespace WebForLink.CrossCutting.InversionControl.Modules
{
    public class ServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(IService<>)).To(typeof(Service<>));

            Bind<IArquivoService>().To<ArquivoService>();
            Bind<IBancoService>().To<BancoService>();
            Bind<IContatoService>().To<ContatoService>();
            Bind<IEnderecoService>().To<EnderecoService>();
            Bind<IFichaCadastralService>().To<FichaCadastralService>();
            Bind<ISolicitacaoService>().To<SolicitacaoService>();
            Bind<ICarrinhoService>().To<CarrinhoService>();
            Bind<IDocumentoAnexadoService>().To<DocumentoAnexadoService>();
            Bind<IDocumentosSolicitacaoService>().To<DocumentosSolicitacaoService>();
            Bind<IListasDocumentoService>().To<ListasDocumentoService>();
            Bind<IListasSolicitanteService>().To<ListasSolicitanteService>();
            Bind<IResponsavelService>().To<ResponsavelService>();
            Bind<ISolicitadoService>().To<SolicitadoService>();
            Bind<ISolicitanteService>().To<SolicitanteService>();
            Bind<IGrupoWebForLinkService>().To<GrupoWebForLinkService>();
            //WebForLink
            Bind<IContratanteFornecedorWebForLinkService>().To<ContratanteFornecedorWebForLinkService>();
            Bind<IServicosMateriaisWebForLinkService>().To<ServicosMateriaisWebForLinkService>();
            Bind<IArquivoWebForLinkService>().To<ArquivosWebForLinkService>();
            Bind<IMeusDocumentosWebForLinkService>().To<MeusDocumentosWebForLinkService>();
            Bind<IMeusCompartilhamentosWebForLinkService>().To<MeusCompartilhamentosWebForLinkService>();
            Bind<IDocumentoWebForLinkService>().To<DocumentoWebForLinkService>();
            Bind<IFornecedorVersaoDocumentosWebForLinkService>().To<FornecedorVersaoDocumentosWebForLinkService>();
            Bind<IFornecedorDocumentoWebForLinkService>().To<FornecedorDocumentoWebForLinkService>();
            Bind<IFornecedorBancoWebForLinkService>().To<FornecedorBancoWebForLinkService>();
            Bind<IFornecedorCategoriaWebForLinkService>().To<FornecedorCategoriaWebForLinkService>();
            Bind<IContratanteConfiguracaoWebForLinkService>().To<ContratanteConfiguracaoWebForLinkService>();
            Bind<ITipoCadastroWebForLinkService>().To<TipoCadastroWebForLinkService>();
            Bind<IPapelWebForLinkService>().To<PapelWebForLinkService>();
            Bind<ISolicitacaoWebForLinkService>().To<SolicitacaoWebForLinkService>();
            Bind<ICadastroUnicoWebForLinkService>().To<CadastroUnicoWebForLinkService>();
            Bind<IBancoWebForLinkService>().To<BancoWebForLinkService>();
            Bind<ITipoBloqueioRoboWebForLinkService>().To<TipoBloqueioRoboWebForLinkService>();
            Bind<IConfiguracaoEmailContratanteWebForLinkService>().To<ConfiguracaoEmailContratanteWebForLinkService>();
            Bind<IUsuarioWebForLinkService>().To<UsuarioWebForLinkService>();
            Bind<ISolicitacaoTramiteWebForLinkService>().To<SolicitacaoTramiteWebForLinkService>();
            Bind<ISolicitacaoProrrogacaoPrazoWebForLinkService>().To<SolicitacaoProrrogacaoPrazoWebForLinkService>();
            Bind<ISolicitacaoCadastroFornecedorWebForLinkService>().To<SolicitacaoCadastroFornecedorWebForLinkService>();
            Bind<ISolicitacaoDocumentoWebForLinkService>().To<SolicitacaoDocumentoWebForLinkService>();
            Bind<IFornecedorWebForLinkService>().To<FornecedorWebForLinkService>();
            Bind<IFornecedorRoboWebForLinkService>().To<FornecedorRoboWebForLinkService>();
            Bind<IEnderecoWebForLinkService>().To<EnderecoWebForLinkService>();
            Bind<IFornecedorArquivoWebForLinkService>().To<FornecedorArquivoWebForLinkService>();
            Bind<ISolicitacaoModificacaoBancoWebForLinkService>().To<SolicitacaoModificacaoBancoWebForLinkService>();
            Bind<ISolicitacaoDocumentosFornecedorWebForLinkService>().To<SolicitacaoDocumentosFornecedorWebForLinkService>();
            Bind<ISolicitacaoModificacaoContatoWebForLinkService>().To<SolicitacaoContatoWebForLinkService>();
            Bind<ISolicitacaoModificacaoEnderecoWebForLinkService>().To<SolicitacaoEnderecoWebForLinkService>();
            Bind<IPreCadastroWebForLinkService>().To<PreCadastroWebForLinkService>();
            Bind<IContratanteWebForLinkService>().To<ContratanteWebForLinkService>();
            Bind<IFuncaoWebForLinkService>().To<FuncaoWebForLinkService>();
            Bind<IFornecedorBaseWebForLinkService>().To<FornecedorBaseWebForLinkService>();
            Bind<ITipoGrupoWebForLinkService>().To<TipoGrupoWebForLinkService>();
            Bind<IDescricaoWebForLinkService>().To<DescricaoWebForLinkService>();
            Bind<IContratanteOrganizacaoComprasWebForLinkService>().To<ContratanteOrganizacaoComprasWebForLinkService>();
            Bind<ITipoDocumentosWebForLinkService>().To<TipoDocumentosWebForLinkService>();
            Bind<IContratanteArquivoWebForLinkService>().To<ContratanteArquivoWebForLinkService>();
            Bind<IPerfilWebForLinkService>().To<PerfilWebForLinkService>();
            Bind<IFornecedorContatoWebForLinkService>().To<FornecedorContatoWebForLinkService>();
            Bind<IUsuarioSenhaHistoricoWebForLinkService>().To<UsuarioSenhaHistoricoWebForLinkService>();
            Bind<IConfiguracaoWebForLinkService>().To<ConfiguracaoWebForLinkService>();
            Bind<IFornecedorBaseImportacaoWebForLinkService>().To<FornecedorBaseImportacaoWebForLinkService>();
            Bind<IImportacaoWebForLinkService>().To<ImportacaoWebForLinkService>();
            Bind<IAplicacaoWebForLinkService>().To<AplicacaoWebForLinkService>();
            Bind<IAcessoLogWebForLinkService>().To<AcessoLogWebForLinkService>();
            Bind<IProcessoLoginWebForLinkService>().To<ProcessoLoginWebForLinkService>();
            Bind<IDestinatarioWebForLinkService>().To<DestinatarioWebForLinkService>();
            Bind<IFornecedorListaDocumentosWebForLinkService>().To<FornecedorListaDocumentosWebForLinkService>();
            Bind<IFornecedorEnderecoWebForLinkService>().To<FornecedorEnderecoWebForLinkService>();
            Bind<ISolicitacaoMensagemWebForLinkService>().To<SolicitacaoMensagemWebForLinkService>();
            Bind<IOrganizacaoComprasWebForLinkService>().To<OrganizacaoComprasWebForLinkService>();
            Bind<IVisaoWebForLinkService>().To<VisaoWebForLinkService>();

            Bind<IAprovacaoWebForLinkService>().To<AprovacaoWebForLinkService>();
            Bind<IFluxoSequenciaWebForLinkService>().To<FluxoSequenciaWebForLinkService>();
            Bind<ISolicitacaoUnspscService>().To<SolicitacaoUnspscWebForLinkService>();
            Bind<IFornecedorServicoMaterialWebForLinkService>().To<FornecedorServicoMaterialWebForLinkService>();
            Bind<IDescricaoDocumentosWebForLinkService>().To<DescricaoDocumentosWebForLinkService>();
            Bind<IDescricaoDocumentosChWebForLinkService>().To<DescricaoDocumentosChWebForLinkService>();
            Bind<IFornecedorCategoriaChWebForLinkService>().To<FornecedorCategoriaChWebForLinkService>();
            Bind<IUsuarioSenhasHistWebForLinkService>().To<UsuarioSenhasHistWebForLinkService>();
            Bind<ITipoDocumentoWebForLinkService>().To<TipoDocumentoWebForLinkService>();
            Bind<ITipoDocumentosChWebForLinkService>().To<TipoDocumentosChWebForLinkService>();
            Bind<IContratantePjpfWebForLinkService>().To<ContratantePjpfWebForLinkService>();
            Bind<IRoboWebForLinkService>().To<RoboWebForLinkService>();
            Bind<IFluxoWebForLinkService>().To<FluxoWebForLinkService>();
            Bind<IFornecedorBaseConviteWebForLinkService>().To<FornecedorBaseConviteWebForLinkService>();
            Bind<ISolicitacaoBloqueioWebForLinkService>().To<SolicitacaoBloqueioWebForLinkService>();
            Bind<ISolicitacaoServicoMaterialWebForLinkService>().To<SolicitacaoServicoMaterialWebForLinkService>();
            Bind<IFornecedorUnspscWebForLinkService>().To<FornecedorUnspscWebForLinkService>();
            Bind<ITipoEnderecoWebForLinkService>().To<TipoEnderecoWebForLinkService>();
            Bind<ITipoUnspscWebForLinkService>().To<TipoUnspscWebForLinkService>();
            Bind<IFornecedorBaseEnderecoWebForLinkService>().To<FornecedorBaseEnderecoWebForLinkService>();
            Bind<ITipoFuncaoBloqueioWebForLinkService>().To<TipoFuncaoBloqueioWebForLinkService>();
            Bind<IContratanteConfiguracaoEmailWebForLinkService>().To<ContratanteConfiguracaoEmailWebForLinkService>();
            Bind<IContratanteOrganizacaoCompraWebForLinkService>().To<ContratanteOrganizacaoCompraWebForLinkService>();
            Bind<IEstadoWebForLinkService>().To<UfWebForLinkService>();
            Bind<IQuestionarioWebForLinkService>().To<QuestionarioWebForLinkService>();
            Bind<IInformacaoComplementarWebForLinkService>().To<InformacaoComplementarWebForLinkService>();
            Bind<IFornecedorInformacaoComplementarComplService>().To<FornecedorInformacaoComplementarComplService>();

            Bind<IArquivoWebForLinkAppService>().To<ArquivoWebForLinkAppService>();
            Bind<ICompartilhamentoWebForLinkService>().To<CompartilhamentoWebForLinkService>();
            Bind<IFornecedorDocumentosVersaoWebForLinkService>().To<FornecedorDocumentosVersaoWebForLinkService>();
            Bind<IContratanteOrganizacaoCompraService>().To<ContratanteOrganizacaoCompraService>();
            Bind<ITipoVisaoWebForLinkService>().To<TipoVisaoWebForLinkService>();
            Bind<ITipoDescricaoWebForLinkService>().To<TipoDescricaoWebForLinkService>();
            Bind<IFornecedorInformacaoComplementarComplWebForLinkService>().To<FornecedorInformacaoComplementarComplWebForLinkService>();
            Bind<ISolicitacaoProrrogacaoWebForLinkService>().To<SolicitacaoProrrogacaoWebForLinkService>();
            Bind<IDocumentosCompartilhadosWebForLinkService>().To<DocumentosCompartilhadosWebForLinkService>();
            Bind<IFornecedorBaseContatosWebForLinkService>().To<FornecedorBaseContatosWebForLinkService>();
            Bind<IFornecedorBaseUnspscWebForLinkService>().To<FornecedorBaseUnspscWebForLinkService>();

        }
    }
}