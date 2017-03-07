using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Service.Common;

namespace WebForLink.Domain.Interfaces.Service
{
    public interface IUsuarioWebForLinkService : IService<Usuario>
    {
        void ContabilizarErroLogin(Usuario usuario);
        void IncluirUsuario(Contratante contratante, CONTRATANTE_CONFIGURACAO config, Usuario usuario);
        void IncluirUsuarioPadraoSenha(Usuario login, USUARIO_SENHAS senha, int[] papeis, int[] perfis);
        void IncluirNovoUsuarioPadraoPreCadastro(Usuario usuarioInclusao, USUARIO_SENHAS historicoSenhaInclusao);
        void IncluirUsuarioIncluirNovaSenhaUsuario(Usuario usuario, USUARIO_SENHAS historicoSenha);
        void IncluirUsuarioPadraoSenha(Usuario login, USUARIO_SENHAS senha, int idPapel, int idPerfil);

        void IncluirNovoUsuarioPadrao(Usuario usuarioInclusao, USUARIO_SENHAS historicoSenhaInclusao, int[] papeis,
            int[] perfis);

        void AlterarUsuario(Usuario entidade);
        void AlterarMinhaConta(Usuario entidade, int[] papeis, int[] perfis, int contratanteSelecionado);
        void ExecutarPrimeiroAcesso(Usuario entidade);
        void ExcluirUsuario(int id);
        bool Bloqueio90Dias(Usuario entidade);
        bool VerificaLoginExistente(string login);
        bool ValidarPorEmail(string email);
        bool ValidarPorCnpj(string cnpj);
        Usuario ZerarTentativasLogin(Usuario usuario);
        Usuario BuscarPorId(int id);
        Usuario BuscarFichaCadastral(int id);
        Usuario BuscarPorLoginParaAcesso(string login);
        Usuario BuscarPorLoginParaAcesso(string login, string senha);
        Usuario BuscarPorLogin(string login);
        Usuario BuscarPorCpf(string cpf);
        Usuario BuscarPorEmail(string email);
        Usuario BuscarPorDocumento(string documento);
        List<Usuario> ListarPorIdContratante(int idContratante);
        RetornoPesquisa<Usuario> PesquisarUsuarios(GerenciarContasFiltrosDTO filtros, int pagina, int qtdLinhas);

        RetornoPesquisa<Usuario> PesquisarUsuarios(Expression<Func<Usuario, bool>> predicate, int pagina,
            int tamanhoPagina);
    }

    public interface ISolicitacaoDesbloqueioWebForLinkService : IService<SOLICITACAO_DESBLOQUEIO>
    {
    }

    public interface ILogRoboWebForLinkService : IService<ROBO_LOG>
    {
    }

    public interface IFornecedorInformacaoComplementarComplService : IService<FORNECEDOR_INFORM_COMPL>
    {
    }

    public interface ICompartilhamentoWebForLinkService : IService<Compartilhamentos>
    {
        RetornoPesquisa<Compartilhamentos> BuscarPesquisaInvertido(Expression<Func<Compartilhamentos, bool>> filtros, int tamanhoPagina, int pagina,
            Func<Compartilhamentos, IComparable> ordenacao);
    }

    public interface IQuestionarioPerguntaWebForLinkService : IService<QUESTIONARIO_PERGUNTA>
    {
    }

    public interface IFornecedorStatusWebForLinkService : IService<FORNECEDOR_STATUS>
    {
    }

    public interface IFornecedorDocumentosVersaoWebForLinkService : IService<VersionamentoDeDocumentoDoFornecedor>
    {
    }

    public interface IContratanteOrganizacaoCompraWebForLinkService : IService<CONTRATANTE_ORGANIZACAO_COMPRAS>
    {
    }

    public interface IContratanteOrganizacaoCompraService : IService<CONTRATANTE_ORGANIZACAO_COMPRAS>
    {
    }

    public interface ITipoDescricaoWebForLinkService : IService<TIPO_DESCRICAO>
    {
        List<TIPO_DESCRICAO> ListarPorGrupoId(int grupoId);
    }

    public interface ISolicitacaoMensagemSWebForLinkervice : IService<FORNECEDOR_CATEGORIA_CH>
    {
    }

    public interface IFornecedorBaseConviteWebForLinkService : IService<FORNECEDORBASE_CONVITE>
    {
    }

    public interface ISolicitacaoModificacaoEnderecoWebForLinkService : IService<SOLICITACAO_MODIFICACAO_ENDERECO>
    {
        SOLICITACAO_MODIFICACAO_ENDERECO BuscarPorId(int id);
        List<SOLICITACAO_MODIFICACAO_ENDERECO> ListarPorSolicitacaoId(int solicitacaoID);
        void InserirSolicitacao(SOLICITACAO_MODIFICACAO_ENDERECO solicitacaoEndereco, SOLICITACAO solicitacao);
        void InserirSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes, SOLICITACAO solicitacao);
        void InserirOuAtualizarSolicitacao(SOLICITACAO_MODIFICACAO_ENDERECO solicitacao);
        void InserirOuAtualizarSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes);
        void ExcluirSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes);
        void InserirOuAtualizar(SOLICITACAO_MODIFICACAO_ENDERECO solicitacao);
    }

    public interface ISolicitacaoBloqueioWebForLinkService : IService<SOLICITACAO_BLOQUEIO>
    {
    }

    public interface ISolicitacaoServicoMaterialWebForLinkService : IService<SOLICITACAO_UNSPSC>
    {
        IEnumerable<SOLICITACAO_UNSPSC> BuscarPorSolicitacaoId(int idSolicitacao);
    }

    public interface IFluxoWebForLinkService : IService<Fluxo>
    {
        Fluxo BuscarPorTipoEContratante(int tipoFluxoId, int contratanteId);
        List<Fluxo> ListarPorContratanteId(int contratanteId);
    }

    public interface IFornecedorCategoriaChWebForLinkService : IService<FORNECEDOR_CATEGORIA_CH>
    {
    }

    public interface ITipoFuncaoBloqueioWebForLinkService : IService<TIPO_FUNCAO_BLOQUEIO>
    {
    }

    public interface ITipoDocumentoWebForLinkService : IService<TipoDeDocumento>
    {
    }

    public interface IDescricaoDocumentosWebForLinkService : IService<DescricaoDeDocumentos>
    {
    }

    public interface IQuestionarioAbaWebForLinkService : IService<QUESTIONARIO_ABA>
    {
    }

    public interface ICategoriaFornecedorChWebForLinkService : IService<FORNECEDOR_CATEGORIA_CH>
    {
    }

    public interface IFornecedorBaseContatosWebForLinkService : IService<FORNECEDORBASE_CONTATOS>
    {
    }

    public interface ICadastroUnicoWebForLinkService : IService<QUESTIONARIO>
    {
        List<QuestionarioDinamico> BuscarQuestionarioDinamico(QuestionarioDinamicoFiltrosDTO filtros);
        List<QUESTIONARIO> BuscarPorIdContratante(int idContratante);
        List<QUESTIONARIO> BuscarPorIdContratanteParaIncluirSolicitacao(int idContratante, int idPapel);
        List<QUESTIONARIO> BuscarPorIdContratante(int idContratante, int idPapel);
        List<QUESTIONARIO> BuscarPorIdContratante(int idContratante, int idPapel, int idSolicitacao);
        List<QUESTIONARIO> BuscarPorIdContratanteAlteracao(int idContratante, int idPapel, int idContratanteFornecedor);
        Fornecedor CarregarDadosPjpf(int idFornecedor);
        Fornecedor CarregarDadosPjpf(int idFornecedor, int idPapel, int? idSolicitacao);
    }

    public interface IAbaPerguntaWebForLinkService : IService<QUESTIONARIO_PERGUNTA>
    {
    }

    public interface IBancoWebForLinkService : IService<TiposDeBanco>
    {
        List<TiposDeBanco> ListarTodosPorNome();
    }

    public interface IConfiguracaoEmailContratanteWebForLinkService : IService<CONTRATANTE_CONFIGURACAO_EMAIL>
    {
        CONTRATANTE_CONFIGURACAO_EMAIL BuscarPorContratanteETipo(int contratanteId, int emailTipoId);
    }

    public interface IArquivoWebForLinkService : IService<ARQUIVOS>
    {
        ARQUIVOS BuscarPorId(int arquivoId);
        void ExcluirMeusDocumentos(int arquivoId, int documentoId, int contratanteId);
    }

    public interface IContratanteOrganizacaoComprasWebForLinkService : IService<CONTRATANTE_ORGANIZACAO_COMPRAS>
    {
        CONTRATANTE_ORGANIZACAO_COMPRAS BuscarPorId(int id);
        CONTRATANTE_ORGANIZACAO_COMPRAS BuscarPorContratanteId(int contratanteId);
        List<CONTRATANTE_ORGANIZACAO_COMPRAS> ListarTodosPorIdContratante(int idContratante);
    }

    public interface IDescricaoDocumentosChWebForLinkService : IService<WFD_DESCRICAO_DOCUMENTOS_CH>
    {
    }

    public interface ITipoVisaoWebForLinkService : IService<TIPO_VISAO>
    {
    }

    public interface IUsuarioSenhasHistWebForLinkService : IService<USUARIO_SENHAS>
    {
        USUARIO_SENHAS BuscarPorIdComUsuario(int id);
        USUARIO_SENHAS BuscarHistoricoPorLogin(string login);
        USUARIO_SENHAS BuscarHistoricoPorIdUsuario(int id);
        List<USUARIO_SENHAS> ListarPorIdContratante(int idContratante);
        List<USUARIO_SENHAS> Listar6UltimasPorUsuarioId(int idUsuario);
    }

    public interface IFornecedorListaDocumentosService : IService<ListaDeDocumentosDeFornecedor>
    {
    }

    public interface ISolicitacaoProrrogacaoWebForLinkService : IService<SOLICITACAO_PRORROGACAO>
    {
        SOLICITACAO_PRORROGACAO BuscarPorIdIncluindoSolicitacao(int id);
    }

    public interface IRoboWebForLinkService : IService<ROBO>
    {
    }

    public interface IContratantePjpfWebForLinkService : IService<WFD_CONTRATANTE_PJPF>
    {
        WFD_CONTRATANTE_PJPF BuscaFichaCadastralPagante(int contratanteId);
    }

    public interface IFornecedorUnspscWebForLinkService : IService<FORNECEDOR_UNSPSC>
    {
    }

    public interface IFornecedorInformacaoComplementarComplWebForLinkService : IService<FORNECEDOR_INFORM_COMPL>
    {
    }

    public interface ITipoDocumentosWebForLinkService : IService<TipoDeDocumento>
    {
    }

    public interface IQuestionarioWebForLinkService : IService<QUESTIONARIO>
    {
        QUESTIONARIO BuscarPorIdEIdContratante(int id, int idContratante);
    }

    public interface IInformacaoComplementarWebForLinkService : IService<WFD_INFORM_COMPL>
    {
        WFD_INFORM_COMPL BuscarPorId(int id);
        WFD_INFORM_COMPL BuscarPorPerguntaId(int idPergunta);
        WFD_INFORM_COMPL BuscarPorPerguntaIdSolicitacaoId(int idPergunta, int idSolicitacao);
        WFD_INFORM_COMPL BuscarPorPerguntaIdSolicitacaoIdResposta(int idPergunta, int idSolicitacao, string resposta);
        List<WFD_INFORM_COMPL> UpdateAll(List<WFD_INFORM_COMPL> entidade);
        List<WFD_INFORM_COMPL> InserirTodos(List<WFD_INFORM_COMPL> entidade);
        FORNECEDOR_INFORM_COMPL BuscarPorPerguntaIdPjpfId(int idPergunta, int idPjpf);
        WFD_INFORM_COMPL ValidaExistente(WFD_INFORM_COMPL entidade);
        bool ValidaDuplicado(WFD_INFORM_COMPL entidade);
        List<WFD_INFORM_COMPL> InsertAll(List<WFD_INFORM_COMPL> entidade);
        FORNECEDOR_INFORM_COMPL BuscarPorPerguntaIdFornecedorId(int pergPai, int contratantePjpfId);
    }

    public interface IContratanteConfiguracaoEmailWebForLinkService : IService<CONTRATANTE_CONFIGURACAO_EMAIL>
    {
    }

    public interface ISolicitacaoStatusWebForLinkService : IService<SOLICITACAO_STATUS>
    {
    }

    public interface ISolicitacaoWebForLinkService : IService<SOLICITACAO>
    {
        SOLICITACAO BuscarSolicitacaoFinalizaCriacaoFornecedor(int solicitacaoId);
        int[] BuscarSolicitacaoAguardandoCarga();
        int[] BuscarSolicitacaoAguardandoRetornoCarga();
        int BuscarTipoFluxoId(int solicitacaoId);
        SOLICITACAO BuscarPorIdIncluindoFluxo(int id);
        List<int> ListarPorId(int contratanteId, int? fornecedorId, int tipoFluxoId, int statusId);
        List<SOLICITACAO> ListarSolicitacaoAprovadaPorId(int aguardando);

        void ConvidarFornecedorComSolicitacao(SOLICITACAO solicitacao,
            SolicitacaoCadastroFornecedor solicitacaoCadastroPjPf, SolicitacaoModificacaoDadosContato contato, object o,
            List<SolicitacaoDeDocumentos> docs, FORNECEDORBASE_CONVITE convite, SOLICITACAO_MENSAGEM mensagem);

        SOLICITACAO BuscarAprovacaoPorId(int id);
        SOLICITACAO BuscarPorSolicitacaoId(int id);
        SOLICITACAO BuscarPorIdComSolicitacaoCadastroPjpf(int id);
        SOLICITACAO BuscarPorIdControleSolicitacoes(int id);
        SOLICITACAO BuscarPorIdComFornecedoresDireto(int id);
        SOLICITACAO BuscarPorIdDocumentosSolicitados(int id);
        SOLICITACAO BuscarPorIdComDocumentos(int id);
        List<SOLICITACAO> ListarTodasSolicitacoesAprovadas(int idSolicitacao);
        List<SOLICITACAO> ListarSolicitacaoCarga(int idContratante);
    }

    public interface ITipoUnspscWebForLinkService : IService<TIPO_UNSPSC>
    {
    }

    public interface IFornecedorBaseEnderecoWebForLinkService : IService<FORNECEDORBASE_ENDERECO>
    {
    }

    public interface IFornecedorBaseUnspscWebForLinkService : IService<FORNECEDORBASE_UNSPSC>
    {
    }

    public interface IEstadoWebForLinkService : IService<TiposDeEstado>
    {
    }

    public interface IAprovacaoWebForLinkService
    {
        void FinalizarCriacaoFornecedor(int solicitacaoId);
        void FinalizarExpansao(int solicitacaoId);
        void FinalizarModificacaoDadosBancarios(int solicitacaoId);
        void FinalizarModificacaoDadosEnderecos(int solicitacaoId);
        void FinalizarModificacaoDadosContatos(int solicitacaoId);
        void FinalizarBloqueio(int solicitacaoId, int? grupoId);
        void FinalizarDesbloqueio(int solicitacaoId, int? grupoId);
        void FinalizarSolicitacao(int? grupoId, int tipoFluxoId, int solicitacaoId);
    }
}