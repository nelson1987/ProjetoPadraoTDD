using System.Web.Mvc;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Interfaces
{
    public interface IFornecedoresController
    {
        //private List<Fornecedor> ListarFornecedorPorFornecedoresId(List<int> fornecedoresId);
        ActionResult ConfirmarFornecedoresSolicitacaoDocumento(FornecedoresSolicitacaoDocumentosVM model, string hdnFornecedores, string[] grupoDoc, string[] doc, string[] hdnObrigatorio, string[] hdnTipoAtualizacao);
        ActionResult FornecedoresFrm(string chaveurl, string cnpj, string cpf);
        ActionResult FornecedoresFrm(FornecedoresVM model, int? CategoriaSelecionada, string CategoriaSelecionadaNome, int? SolicitacaoID, int Empresa, string Acao);
        ActionResult FornecedoresLst(PesquisaFornecedoresVM model);
        ActionResult FornecedoresSolicitacaoDocumento(string chaveurl);
        JsonResult ConsultarSolicitacoesEmAberto(int contratanteId, int? fornecedorId, int? tipoFluxoId);
        FileResult FornecedorArquivo(string chaveurl);
        ActionResult FornecedoresExibirFrm(string chaveurl);
        ActionResult FornecedorFichaVL(string chaveurl);
        ActionResult FornecedorModificacaoFrm(string chaveurl);
        
        JsonResult ListarOutrosDadosDescricao(int outrosDadosGrupo);
        JsonResult ListarOutrosDadosGrupo(int outrosDadosVisao);

        ContentResult UploadArquivoFornecedor(string cnpj_cpf, string arqTmp);
        JsonResult ValidarInternaCNPJ(string cnpj, int contratante, int tipoFornecedor, int categoria);
        //IList<SolicitacaoDeDocumentos> CriarSolicitacoesDocumentos(int solicitacaoCriacaoID, ICollection<ListaDeDocumentosDeFornecedor> documentos);
        //IList<SolicitacaoDeDocumentos> CriarSolicitacoesDocumentos(int solicitacaoCriacaoID, ICollection<ListaDeDocumentosDeFornecedor> documentos, WFD_SOL_MENSAGEM solicitacaoMensagem);
        //bool FinalizarFichaCadastral(FichaCadastralVM model);
        //List<SelectListItem> MontarDescricaoDocumento(int? tipoDocumento, string tipo);
        //List<SelectListItem> MontarDescricaoDocumentoSemContratante(int? tipoDocumento, string tipo);
        //List<SelectListItem> MontarTipoDocumento(string tipo);
        //SolicitacaoFornecedorVM PopularSolicitacaoCadastroPJPF(int contratanteId, SolicitacaoFornecedorVM modelo);
        //void PreencherFichaCadastral(WFD_SOLICITACAO solicitacao, ref FichaCadastralVM ficha, int TpPapel);

        //private static void CriarEntidadePartialDadosCadastro(int fornecedorID, Fornecedor fornecedor, Contratante contratante, FichaCadastralVM ficha);
        //private static void EnviarEmailContratantes(FornecedoresSolicitacaoDocumentosVM model, int contratanteId, Fornecedor item);
        //private DateTime CalculaDataVencimento(int periodoId);
        //private void CompletarSolicitacaoCadastroPJPF(ref SolicitacaoCadastroFornecedor cadPJPF, FichaCadastralVM ficha);
        //private WFD_SOLICITACAO CriarSolicitacao(FichaCadastralVM model, int tipoFluxoId);
        //private WFD_SOLICITACAO CriarSolicitacaoDocumentos(FichaCadastralVM model, int tipoFluxoId);
        //private void EnviarSolicitacaoFornecedor(int contratanteId, FornecedoresVM model, int? solicitacaoId);
        //private void IncluirSolicitacaoDocumentoEnviarEmailContratantes(FornecedoresSolicitacaoDocumentosVM model, string[] doc, string[] hdnObrigatorio, string[] hdnTipoAtualizacao, int contratanteId, int usuarioId, DateTime dtPrazo, int fluxoId, int papelAtual, Fornecedor item);
        //private List<SelectListItem> ListarContratantesCombo(List<WFD_CONTRATANTE_PJPF> contratantes);
        //private void ListarFornecedoresComFichaCadastral(int? CategoriaSelecionada, string Fornecedor, string CNPJ, int grupoId, int pagina, List<FornecedoresVM> fornecedoresVM, PesquisaFornecedoresVM modelo, out int totalRegistro, out int totalPaginas);
        //private void ListarFornecedoresSemFichaCadastral(int? CategoriaSelecionada, string Fornecedor, string CNPJ, string CPF, int grupoId, int pagina, List<FornecedoresVM> fornecedoresVM, out int totalRegistro, out int totalPaginas);
        //private void ManterDadosBancarios(List<DadosBancariosVM> dadosBancarios, int solicitacaoCriacaoID, int contratanteID);
        //private void ManterDadosContatos(List<DadosContatoVM> dadosContatos, int solicitacaoCriacaoID);
        //private void ManterDadosEnderecos(List<DadosEnderecosVM> dadosEnderecos, int solicitacaoCriacaoID);
        //private void ManterDocumentos(List<SolicitacaoDocumentosVM> solicitacoesDocumentosVM, int solicitacaoCriacaoID, int contratanteId);
        //private void ManterUnspsc(List<FornecedorUnspscVM> unspsc, int solicitacaoCriacaoID);
        //private void PersistirDadosEmMemoria();
        //private void PersistirDadosEnderecoEmMemoria();
        //private void PopularSolicitacaoEmAprovacao(int contratanteId, int fornecedorId, int? usuarioId, int fluxoId, WFD_SOLICITACAO solicitacao);
        //private void ValidarArquivoEnviaEmailAnexo(FornecedoresSolicitacaoDocumentosVM model, int contratanteId, Fornecedor item, int arquivoId);
        //private void ValidarFormularioCriacaoSolicitacao(FornecedoresVM model, string Acao, int contratante);
    }
}
