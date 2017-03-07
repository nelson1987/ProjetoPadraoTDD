using Ninject.Modules;
using WebForLink.Data.Repository.Dapper;
using WebForLink.Data.Repository.EntityFramework;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Repository.Common;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.CrossCutting.InversionControl.Modules
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //Bind(typeof(IRepository<>)).To(typeof(EntityFrameworkRepository<>));

            Bind<ISolicitacaoRepository>().To<SolicitacaoRepository>();
            Bind<ISolicitacaoReadOnlyRepository>().To<SolicitacaoDapperRepository>();
            Bind<IReadOnlyRepository<Solicitacao>>().To<SolicitacaoDapperRepository>();

            Bind<IFichaCadastralRepository>().To<FichaCadastralRepository>();
            Bind<IFichaCadastralReadOnlyRepository>().To<FichaCadastralDapperRepository>();
            Bind<IReadOnlyRepository<FichaCadastral>>().To<FichaCadastralDapperRepository>();

            Bind<IContatoRepository>().To<ContatoRepository>();
            Bind<IContatoReadOnlyRepository>().To<ContatoDapperRepository>();
            Bind<IReadOnlyRepository<Contato>>().To<ContatoDapperRepository>();

            Bind<IBancoRepository>().To<BancoRepository>();
            Bind<IBancoReadOnlyRepository>().To<BancoDapperRepository>();
            Bind<IReadOnlyRepository<Banco>>().To<BancoDapperRepository>();

            Bind<IEnderecoRepository>().To<EnderecoRepository>();
            Bind<IEnderecoReadOnlyRepository>().To<EnderecoDapperRepository>();
            Bind<IReadOnlyRepository<Endereco>>().To<EnderecoDapperRepository>();

            Bind<IArquivoRepository>().To<ArquivoRepository>();
            Bind<IArquivoReadOnlyRepository>().To<ArquivoDapperRepository>();
            Bind<IReadOnlyRepository<Arquivo>>().To<ArquivoDapperRepository>();

            Bind<ICarrinhoRepository>().To<CarrinhoRepository>();
            Bind<ICarrinhoReadOnlyRepository>().To<CarrinhoDapperRepository>();
            Bind<IReadOnlyRepository<Carrinho>>().To<CarrinhoDapperRepository>();

            Bind<IDocumentoAnexadoRepository>().To<DocumentoAnexadoRepository>();
            Bind<IDocumentoAnexadoReadOnlyRepository>().To<DocumentoAnexadoDapperRepository>();
            Bind<IReadOnlyRepository<DocumentoAnexado>>().To<DocumentoAnexadoDapperRepository>();

            Bind<IDocumentosSolicitacaoRepository>().To<DocumentosSolicitacaoRepository>();
            Bind<IDocumentosSolicitacaoReadOnlyRepository>().To<DocumentosSolicitacaoDapperRepository>();
            Bind<IReadOnlyRepository<DocumentoSolicitacao>>().To<DocumentosSolicitacaoDapperRepository>();

            Bind<IListasDocumentoRepository>().To<ListasDocumentoRepository>();
            Bind<IListaDocumentoReadOnlyRepository>().To<ListaDocumentoDapperRepository>();
            Bind<IReadOnlyRepository<ListaDocumento>>().To<ListaDocumentoDapperRepository>();

            Bind<IListasSolicitanteRepository>().To<ListasSolicitanteRepository>();
            Bind<IListasSolicitanteReadOnlyRepository>().To<ListasSolicitanteDapperRepository>();
            Bind<IReadOnlyRepository<ListasSolicitante>>().To<ListasSolicitanteDapperRepository>();

            Bind<IResponsavelRepository>().To<ResponsavelRepository>();
            Bind<IResponsavelReadOnlyRepository>().To<ResponsavelDapperRepository>();
            Bind<IReadOnlyRepository<Responsavel>>().To<ResponsavelDapperRepository>();

            Bind<ISolicitadoRepository>().To<SolicitadoRepository>();
            Bind<ISolicitadoReadOnlyRepository>().To<SolicitadoDapperRepository>();
            Bind<IReadOnlyRepository<Solicitado>>().To<SolicitadoDapperRepository>();

            Bind<ISolicitanteRepository>().To<SolicitanteRepository>();
            Bind<ISolicitanteReadOnlyRepository>().To<SolicitanteDapperRepository>();
            Bind<IReadOnlyRepository<Solicitante>>().To<SolicitanteDapperRepository>();

            Bind<IDocumentosCompartilhadosWebForLinkRepository>().To<DocumentosCompartilhadosWebForLinkRepository>();
            Bind<ITipoCadastroWebForLinkRepository>().To<TipoCadastroRepository>();

            Bind<IBancoWebForLinkRepository>().To<BancoWebForLinkRepository>();
            Bind<IUsuarioWebForLinkRepository>().To<UsuarioWebForLinkRepository>();

            Bind<IContratanteWebForLinkRepository>().To<ContratanteWebForLinkRepository>();
            Bind<ITipoGrupoWebForLinkRepository>().To<TipoGrupoWebForLinkRepository>();
            Bind<IConfiguracaoWebForLinkRepository>().To<ConfiguracaoWebForLinkRepository>();
            Bind<IAplicacaoWebForLinkRepository>().To<AplicacaoWebForLinkRepository>();
            Bind<IAcessoLogWebForLinkRepository>().To<AcessoLogWebForLinkRepository>();
            Bind<IContratanteOrganizacaoCompraWebForLinkRepository>().To<ContratanteOrganizacaoCompraWebForLinkRepository>();
            Bind<IContratanteFornecedorWebForLinkRepository>().To<ContratanteFornecedorWebForLinkRepository>();
            Bind<ISolicitacaoServicoMaterialWebForLinkRepository>().To<SolicitacaoServicoMaterialWebForLinkRepository>();
            Bind<IFornecedorBancoWebForLinkRepository>().To<FornecedorBancoWebForLinkRepository>();
            Bind<IFornecedorBaseWebForLinkRepository>().To<FornecedorBaseWebForLinkRepository>();
            Bind<IFornecedorContatoRepository>().To<FornecedorContatosRepository>();
            Bind<IFornecedorEnderecoRepository>().To<FornecedorEnderecoRepository>();
            Bind<IFornecedorInformacaoComplementarComplRepository>().To<FornecedorInformComplRepository>();
            Bind<IFornecedorStatusWebForLinkRepository>().To<FornecedorStatusWebForLinkRepository>();
            Bind<IFornecedorUnspscRepository>().To<FornecedorServicoMaterialRepository>();
            Bind<ISolicitacaoDesbloqueioWebForLinkRepository>().To<SolicitacaoDesbloqueioIWebForLinkRepository>();
            Bind<ITipoFuncaoBloqueioWebForLinkRepository>().To<TipoFuncaoBloqueioRepository>();
            Bind<ITipoEnderecoWebForLinkRepository>().To<TipoEnderecoRepository>();
            Bind<ITipoDocumentosChWebForLinkRepository>().To<TipoDocumentosChRepository>();
            Bind<ITipoDocumentoWebForLinkRepository>().To<TipoDocumentosRepository>();
            Bind<IUsuarioSenhasHistWebForLinkRepository>().To<UsuarioSenhasHistRepository>();
            Bind<IEstadoWebForLinkRepository>().To<EstadoWebForLinkRepository>();
            Bind<IArquivosWebForLinkRepository>().To<ArquivosWebForLinkRepository>();
            Bind<ISolicitacaoBancoRepository>().To<SolicitacaoBancoRepository>();
            Bind<IDestinatarioWebForLinkRepository>().To<DestinatarioWebForLinkRepository>();
            Bind<ICompartilhamentoWebForLinkRepository>().To<CompartilhamentosWebForLinkRepository>();
            Bind<IFluxoSequenciaWebForLinkRepository>().To<FluxoSequenciaWebForLinkRepository>();
            Bind<IFornecedorBaseConviteWebForLinkRepository>().To<FornecedorBaseConviteWebForLinkRepository>();
            Bind<IFuncaoWebForLinkRepository>().To<FuncaoWebForLinkRepository>();
            Bind<IContratanteConfiguracaoWebForLinkRepository>().To<ContratanteConfiguracaoWebForLinkRepository>();
            Bind<IPapelWebForLinkRepository>().To<PapelWebForLinkRepository>();
            Bind<ISolicitacaoTramiteWebForLinkRepository>().To<SolicitacaoTramiteWebForLinkRepository>();
            Bind<ISolicitacaoCadastroFornecedorWebForLinkRepository>().To<SolicitacaoCadastroFornecedorWebForLinkRepository>();
            Bind<IFornecedorWebForLinkRepository>().To<FornecedorWebForLinkRepository>();
            Bind<ISolicitacaoModificacaoContatoWebForLinkRepository>().To<SolicitacaoModificacaoContatoWebForLinkRepository>();
            Bind<ISolicitacaoEnderecoWebForLinkRepository>().To<SolicitacaoEnderecoWebForLinkRepository>();
            Bind<IPerfilWebForLinkRepository>().To<PerfilWebForLinkRepository>();

            Bind<ISolicitacaoCadasatroFornecedorWebForLinkRepository>().To<SolicitacaoCadasatroFornecedorWebForLinkRepository>();
            Bind<ISolicitacaoMensagemWebForLinkRepository>().To<SolicitacaoMensagemWebForLinkRepository>();
            Bind<ISolicitacaoDocumentoWebForLinkRepository>().To<SolicitacaoDocumentoWebForLinkRepository>();
            Bind<IContratanteConfiguracaoEmailWebForLinkRepository>().To<ContratanteConfiguracaoEmailWebForLinkRepository>();
            Bind<IDescricaoDocumentosChWebForLinkRepository>().To<DescricaoDocumentosChWebForLinkRepository>();
            Bind<IFornecedorCategoriaChWebForLinkRepository>().To<FornecedorCategoriaChWebForLinkRepository>();
            Bind<IGrupoWebForLinkRepository>().To<GrupoWebForLinkRepository>();
            Bind<ISolicitacaoBloqueioWebForLinkRepository>().To<SolicitacaoBloqueioWebForLinkRepository>();
            Bind<IFornecedorBaseContatosWebForLinkRepository>().To<FornecedorBaseContatosWebForLinkRepository>();
            Bind<IFornecedorBaseEnderecoWebForLinkRepository>().To<FornecedorBaseEnderecoWebForLinkRepository>();
            Bind<IFornecedorBaseImportacaoWebForLinkRepository>().To<FornecedorBaseImportacaoWebForLinkRepository>();
            Bind<IFornecedorBaseUnspscWebForLinkRepository>().To<FornecedorBaseUnspscRepository>();
            Bind<IFornecedorCategoriaWebForLinkRepository>().To<FornecedorCategoriaRepository>();
            Bind<IFornecedorDocumentoWebForLinkRepository>().To<FornecedorDocumentosRepository>();
            Bind<IFornecedorDocumentosVersaoWebForLinkRepository>().To<FornecedorDocumentosVersaoRepository>();
            Bind<IFornecedorListaDocumentosWebForLinkRepository>().To<FornecedorListaDocumentosRepository>();
            Bind<IFornecedorSolicitacaoWebForLinkRepository>().To<FornecedorSolicitacaoRepository>();
            Bind<ISolicitacao_prorrogacaoWebForLinkRepository>().To<SolicitacaoProrrogacaoWebForLinkRepository>();
            Bind<ITipoDescricaoWebForLinkRepository>().To<TipoDescricaoWebForLinkRepository>();
            Bind<ITipoVisaoWebForLinkRepository>().To<TipoVisaoWebForLinkRepository>();
            Bind<IFluxoWebForLinkRepository>().To<FluxoWebForLinkRepository>();
            Bind<IDescricaoDocumentosWebForLinkRepository>().To<DescricaoDocumentosWebForLinkRepository>();
            Bind<IQuestionarioWebForLinkRepository>().To<QuestionarioWebForLinkRepository>();
            Bind<ILogRoboWebForLinkRepository>().To<LogRoboWebForLinkRepository>();
            Bind<IFornecedorContatoWebForLinkRepository>().To<FornecedorContatoWebForLinkRepository>();
            Bind<IFornecedorEnderecoWebForLinkRepository>().To<FornecedorEnderecoWebForLinkRepository>();


            Bind<IQuestionarioPerguntaWebForLinkRepository>().To<QuestionarioPerguntaRepository>();
            Bind<ISolicitacaoStatusWebForLinkRepository>().To<SolicitacaoStatusWebForLinkRepository>();
            Bind<ITipoFluxoWebForLinkRepository>().To<TipoFluxoWebForLinkRepository>();

            Bind<IQuestionarioRespostaWebForLinkRepository>().To<QuestionarioRespostaWebForLinkRepository>();

            Bind<ISolicitacaoBancoWebForLinkRepository>().To<SolicitacaoBancoWebForLinkRepository>();
            Bind<IInformacaoComplementarWebForLinkRepository>().To<InformacaoComplementarWebForLinkRepository>();
            Bind<IFornecedorInformacaoComplementarComplWebForLinkRepository>().To<FornecedorInformacaoComplementarComplWebForLinkRepository>();
            Bind<IRoboWebForLinkRepository>().To<RoboWebForLinkRepository>();
            Bind<ISolicitacaoWebForLinkRepository>().To<SolicitacaoWebForLinkRepository>();
            Bind<IFornecedorUnspscWebForLinkRepository>().To<FornecedorUnspscWebForLinkRepository>();
            Bind<ITipoUnspscWebForLinkRepository>().To<TipoUnspscWebForLinkRepository>();
            Bind<ICategoriaWebForLinkRepository>().To<CategoriaWebForLinkRepository>();
        }
    }
}