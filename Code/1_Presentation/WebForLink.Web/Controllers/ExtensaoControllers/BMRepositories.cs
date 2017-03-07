
using WebForLink.Service.Services.FichaCadastral;
using WebForLink.Service.Services.Process;

namespace WebForLink.Web.Controllers.Extensoes
{
    public class BPRepositories
    {
        
        #region Fornecedor Cadastrado
        public FornecedorContatoService fornecedorContatoService { get; private set; }
        public FornecedorBancoService fornecedorBancoService { get; private set; }
        public FornecedorServicoMaterialService fornecedorServicoMaterialService { get; private set; }
        public FornecedorEnderecoService fornecedorEnderecoService { get; private set; }
        public FornecedorCategoriaService FornecedorCategoriaService { get; private set; }
        public FornecedorArquivoService fornecedorArquivoService { get; private set; }
        #endregion

        #region Fornecedor Importado
        public IFornecedorBaseService fornecedorBaseBP { get; private set; }
        public FornecedorBaseImportacaoService pjpfBaseImportacaoBP { get; private set; }
        #endregion

        #region Contratante
        //public IContratanteService contratanteBP { get; private set; }
        public ContratanteConfiguracaoService contratanteConfiguracaoBP { get; private set; }
        public ConfiguracaoEmailContratanteService contratanteConfiguracaoEmailBP { get; private set; }
        public ContratanteFornecedorService contratantePjPfBP { get; private set; }
        #endregion
        
        public UsuarioService usuarioBP { get; private set; }
        public ImportacaoService importacaoBP { get; private set; }
        //public RoboLogService RoboLogBP { get; private set; }
        public RoboService roboBP { get; private set; }
        public SolicitacaoDesbloqueioService solicitacaoDesbloqueio { get; private set; }
        public PapelService papelBP { get; private set; }
        public BancoService bancoBP { get; private set; }
        public EnderecoService enderecoBP { get; private set; }
        public FluxoService fluxoBP { get; private set; }
        public VisaoService visaoBP { get; private set; }
        public DescricaoService descricaoBP { get; private set; }
        public TipoDocumentosService tipoDocumentosBP { get; private set; }
        public CadastroUnicoService cadastroUnicoBP { get; private set; }
        public InformacaoComplementarService informacaoComplementarBP { get; private set; }
        public OrganizacaoComprasService organizacaoComprasBP { get; private set; }
        public TipoGrupoService wfdTGrupoBP { get; private set; }
        public TipoBloqueioRoboService funcaoBloqueioBP { get; private set; }
        public FichaCadastralService bpFichaCadastral { get; private set; }
        public PreCadastroService preCadastroBP { get; private set; }
        public BPRepositories()
        {
            
            //RoboLogBP = new RoboLogService();
            
            //contratanteConfiguracaoBP = new ContratanteConfiguracaoService();
            //fornecedorArquivoService = new FornecedorArquivoService();
            //contratanteConfiguracaoEmailBP = new ConfiguracaoEmailContratanteService();
            //fornecedorServicoMaterialService = new FornecedorServicoMaterialService();
            //fornecedorEnderecoService = new FornecedorEnderecoBP();
            //usuarioBP = new UsuarioService();
            
        }
    }
}