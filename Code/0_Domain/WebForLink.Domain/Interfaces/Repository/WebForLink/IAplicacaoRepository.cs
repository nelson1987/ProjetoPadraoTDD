using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Interfaces.Repository.Common;

namespace WebForLink.Domain.Interfaces.Repository
{
    public interface IQuestionarioAbaPerguntaPapelWebForLinkRepository : IRepository<QUESTIONARIO_PAPEL>
    {
    }

    public interface ICategoriaFornecedorCHWebForLinkRepository : IRepository<FORNECEDOR_CATEGORIA_CH>
    {
    }

    public interface ITipoFluxoWebForLinkRepository : IRepository<TipoDeFluxo>
    {
    }

    public interface IAbaPerguntaWebForLinkRepository : IRepository<QUESTIONARIO_PERGUNTA>
    {
    }

    public interface IFornecedorCategoriaChWebForLinkRepository : IRepository<FORNECEDOR_CATEGORIA_CH>
    {
    }

    public interface IDescricaoDocumentosWebForLinkRepository : IRepository<DescricaoDeDocumentos>
    {
        List<DescricaoDeDocumentos> ListarPorContratanteId(int contratanteId);
    }

    public interface ISolicitacaoStatusWebForLinkRepository : IRepository<SOLICITACAO_STATUS>
    {
    }

    public interface ISolicitacaoTramiteWebForLinkRepository : IRepository<SOLICITACAO_TRAMITE>
    {
        SOLICITACAO_TRAMITE BuscarTramitePorSolicitacaoIdPapelId(int solicitacaoId, int papelId);
        SOLICITACAO_TRAMITE IncluirTramiteStatusUm(SOLICITACAO_TRAMITE tramiteInclusao);
        SOLICITACAO_TRAMITE IncluirTramiteStatusDoisComTramiteAtual(SOLICITACAO_TRAMITE tramiteInclusao);
        SOLICITACAO_TRAMITE IncluirTramiteStatusDoisSemTramiteAtual(SOLICITACAO_TRAMITE tramiteInclusao);
        SOLICITACAO_TRAMITE ReprovarTramite(SOLICITACAO_TRAMITE tramiteInclusao);
        List<SOLICITACAO_TRAMITE> BuscarTramiteAtual(int solicitacao);
        List<SOLICITACAO_TRAMITE> BuscarTramiteAtualcomPapel(int solicitacao);
        SOLICITACAO_TRAMITE incluirTramite(SOLICITACAO_TRAMITE tramite);
        bool SolicitacaoAprovadaPorUmAprovador(int solicitacao);
        bool SolicitacaoFornecedorFinalizou(int solicitacao);
    }

    public interface IFornecedorBaseConviteWebForLinkRepository : IRepository<FORNECEDORBASE_CONVITE>
    {
    }

    public interface ISolicitacaoCadasatroFornecedorWebForLinkRepository : IRepository<SolicitacaoCadastroFornecedor>
    {
    }

    public interface ISolicitacaoMensagemWebForLinkRepository : IRepository<SOLICITACAO_MENSAGEM>
    {
    }

    public interface ISolicitacaoEnderecoWebForLinkRepository : IRepository<SOLICITACAO_MODIFICACAO_ENDERECO>
    {
        List<SOLICITACAO_MODIFICACAO_ENDERECO> ListarPorSolicitacaoId(int solicitacaoID);
        void InserirOuAtualizar(SOLICITACAO_MODIFICACAO_ENDERECO solicitacao);
    }

    public interface ISolicitacaoModificacaoContatoWebForLinkRepository :
        IRepository<SolicitacaoModificacaoDadosContato>
    {
        List<SolicitacaoModificacaoDadosContato> ListarPorSolicitacaoId(int solicitacaoID);
        void InserirOuAtualizar(SolicitacaoModificacaoDadosContato solicitacao);
        void Excluir(SolicitacaoModificacaoDadosContato solicitacao);
        void ManterContatoCadastroFornecedor(List<SolicitacaoModificacaoDadosContato> contatos, int SolicitacaoId);
    }

    public interface ISolicitacaoCadastroFornecedorWebForLinkRepository : IRepository<SolicitacaoCadastroFornecedor>
    {
        SolicitacaoCadastroFornecedor BuscarPorRazaoSocial(string razaoSocial);
        SolicitacaoCadastroFornecedor BuscarPorId(string cnpjteste, int statusId);
        int BuscarIdSolicitacaoPorCnpj(string cnpj);
        string BuscarRazaoSocialPorCnpj(string cnpj);
        string BuscarRazaoSocialPorcpf(string cpf);
        SolicitacaoCadastroFornecedor BuscarPorSolicitacaoID(int solicitacaoID);
        SolicitacaoCadastroFornecedor BuscarPorSolicitacaoIDComDocumentos(int solicitacaoID);
        SolicitacaoCadastroFornecedor BuscarCadFornPorSolicitacao(int solicitacaoID);
    }

    public interface ITipoUnspscWebForLinkRepository : IRepository<TIPO_UNSPSC>
    {
    }

    public interface IQuestionarioPerguntaWebForLinkRepository : IRepository<QUESTIONARIO_PERGUNTA>
    {
        List<QUESTIONARIO_PERGUNTA> BuscarPorPerguntasFilho(int idPai);
    }

    public interface IQuestionarioRespostaWebForLinkRepository : IRepository<QUESTIONARIO_RESPOSTA>
    {
    }

    public interface IFornecedorBaseEnderecoWebForLinkRepository : IRepository<FORNECEDORBASE_ENDERECO>
    {
    }

    public interface IFornecedorBaseUnspscWebForLinkRepository : IRepository<FORNECEDORBASE_UNSPSC>
    {
    }

    public interface IPerfilWebForLinkRepository : IRepository<Perfil>
    {
        List<Perfil> ListarPorContratanteId(int contratanteId);
    }

    public interface IPapelWebForLinkRepository : IRepository<Papel>
    {
        List<Papel> ListarPorContratanteId(int contratanteId);
        Papel BuscarPorContratanteIdETipoPapelId(int contratanteId, int tipoPapelId);
    }

    public interface ISolicitacao_prorrogacaoWebForLinkRepository : IRepository<SOLICITACAO_PRORROGACAO>
    {
        SOLICITACAO_PRORROGACAO BuscarPorIdIncluindoSolicitacao(int id);
    }

    public interface ISolicitacaoDesbloqueioWebForLinkRepository : IRepository<SOLICITACAO_DESBLOQUEIO>
    {
    }

    public interface IFornecedorStatusWebForLinkRepository : IRepository<FORNECEDOR_STATUS>
    {
    }

    public interface ILogRoboWebForLinkRepository : IRepository<ROBO_LOG>
    {
    }

    public interface IFornecedorCategoriaWebForLinkRepository : IRepository<FORNECEDOR_CATEGORIA>
    {
        List<FORNECEDOR_CATEGORIA> ListarPorContratanteId(int id);
        FORNECEDOR_CATEGORIA BuscarComPjPfListaDocumento(int id);
    }

    public interface IGrupoWebForLinkRepository : IRepository<GRUPO>
    {
        int QuantidadeEmpresa(int contratanteId);
    }

    public interface IFuncaoWebForLinkRepository : IRepository<FUNCAO>
    {
    }

    public interface IFornecedorDocumentosVersaoWebForLinkRepository : IRepository<VersionamentoDeDocumentoDoFornecedor>
    {
    }

    public interface IFornecedorListaDocumentosWebForLinkRepository : IRepository<ListaDeDocumentosDeFornecedor>
    {
    }

    public interface IFornecedorSolicitacaoWebForLinkRepository : IRepository<FORNECEDOR_SOLICITACAO>
    {
    }

    public interface IDescricaoDocumentosChWebForLinkRepository : IRepository<WFD_DESCRICAO_DOCUMENTOS_CH>
    {
    }


    public interface IFornecedorBaseContatosWebForLinkRepository : IRepository<FORNECEDORBASE_CONTATOS>
    {
    }


    public interface ISolicitacaoBloqueioWebForLinkRepository : IRepository<SOLICITACAO_BLOQUEIO>
    {
    }

    public interface IFornecedorBaseImportacaoWebForLinkRepository : IRepository<FORNECEDORBASE_IMPORTACAO>
    {
    }

    public interface IFornecedorDocumentoWebForLinkRepository : IRepository<DocumentosDoFornecedor>
    {
    }


    public interface IFluxoSequenciaWebForLinkRepository : IRepository<FLUXO_SEQUENCIA>
    {
        List<FLUXO_SEQUENCIA> ListarPorContratanteId(int contratanteId);
        List<FLUXO_SEQUENCIA> ListarPorContratanteIdEFluxoTipoId(int contratanteId, int fluxoId);
    }

    public interface IFluxoWebForLinkRepository : IRepository<Fluxo>
    {
        Fluxo BuscarPorTipoEContratante(int tipoFluxoId, int contratanteId);
        List<Fluxo> BuscarPorContratanteId(int contratanteId);
    }

    public interface IDestinatarioWebForLinkRepository : IRepository<DESTINATARIO>
    {
    }

    public interface ISolicitacaoDocumentoWebForLinkRepository : IRepository<SolicitacaoDeDocumentos>
    {
        List<SolicitacaoDeDocumentos> ListarPorIdSolicitacao(int id);
    }

    public interface ISolicitacaoBancoWebForLinkRepository : IRepository<SolicitacaoModificacaoDadosBancario>
    {
    }

    public interface IQuestionarioWebForLinkRepository : IRepository<QUESTIONARIO>
    {
        List<QUESTIONARIO> ListarTodosPorIdContratante(int idContratante);
        QUESTIONARIO BuscarPorIdEIdContratante(int id, int idContratante);
    }

    public interface IInformacaoComplementarWebForLinkRepository : IRepository<WFD_INFORM_COMPL>
    {
        WFD_INFORM_COMPL BuscarPorPerguntaIdSolicitacaoId(int perguntaPai, int solicitacaoId);
        FORNECEDOR_INFORM_COMPL BuscarPorPerguntaIdFornecedorId(int perguntaPai, int fornecedorId);
    }

    public interface IContratanteConfiguracaoEmailWebForLinkRepository : IRepository<CONTRATANTE_CONFIGURACAO_EMAIL>
    {
        CONTRATANTE_CONFIGURACAO_EMAIL BuscarPorContratanteETipo(int contratanteId, int emailTipoId);
        List<CONTRATANTE_CONFIGURACAO_EMAIL> ListarTodosPorIdContratante(int contratanteId);
    }

    public interface IContratanteConfiguracaoWebForLinkRepository : IRepository<CONTRATANTE_CONFIGURACAO>
    {
    }

    public interface IRoboWebForLinkRepository : IRepository<ROBO>
    {
    }

    public interface IFornecedorWebForLinkRepository : IRepository<Fornecedor>
    {
        Fornecedor CarregarDadosPjpf(int idFornecedor);
    }

    public interface IFornecedorBancoWebForLinkRepository : IRepository<BancoDoFornecedor>
    {
    }

    public interface IFornecedorEnderecoWebForLinkRepository : IRepository<FORNECEDOR_ENDERECO>
    {
    }

    public interface IFornecedorContatoWebForLinkRepository : IRepository<FORNECEDOR_CONTATOS>
    {
    }

    public interface IFornecedorUnspscWebForLinkRepository : IRepository<FORNECEDOR_UNSPSC>
    {
    }

    public interface IFornecedorInformacaoComplementarComplWebForLinkRepository : IRepository<FORNECEDOR_INFORM_COMPL>
    {
    }

    public interface ISolicitacaoWebForLinkRepository : IRepository<SOLICITACAO>
    {
        int BuscarTipoFluxoId(int solicitacaoId);
        List<int> ListarPorId(int contratanteId, int? fornecedorId, int tipoFluxoId, int statusId);
        int[] BuscarSolicitacaoAguardandoCarga();
        int[] BuscarSolicitacaoAguardandoRetornoCarga();
        SOLICITACAO BuscarPorIdIncluindoFluxo(int id);
        List<SOLICITACAO> ListarSolicitacaoAprovadaPorId(int statusId);
        List<SOLICITACAO> ListarTodasSolicitacoesAprovadas(int idSolicitacao);
        List<SOLICITACAO> ListarSolicitacaoCarga(int idContratante);
        SOLICITACAO BuscarSolicitacaoFinalizaCriacaoFornecedor(int solicitacaoId);
        SOLICITACAO BuscarAprovacaoPorId(int id);
        SOLICITACAO BuscarPorSolicitacaoId(int id);
        SOLICITACAO BuscarPorDocumento(string pjpf);
        SOLICITACAO BuscarPorIdComSolicitacaoCadastroPJPF(int id);
        SOLICITACAO BuscarPorIdControleSolicitacoes(int id);
        SOLICITACAO BuscarPorIdComFornecedoresDireto(int id);
        SOLICITACAO BuscarPorIdDocumentosSolicitados(int id);
        SOLICITACAO BuscarPorIdComCadPjpf(int id);
        SOLICITACAO BuscarX(int id);
        SOLICITACAO BuscarPorIdComDocumentos(int id);
        List<SOLICITACAO> TrazerTodasSolicitacoesAprovadas(int idSolicitacao);
        List<SOLICITACAO> TrazerSolicitacaoAprovadaPorId();
        SOLICITACAO SolicitarProrrogacao(int idSolicitacao, DateTime dias, string motivo);
        SOLICITACAO BuscarSolicitacaoFichaCadastral(int id);
        SOLICITACAO BuscarSolicitacaoComBase(int Solicitacaoid);

        RetornoPesquisa<SOLICITACAO> BuscarPesquisaAcompanhamento(Expression<Func<SOLICITACAO, bool>> filtros,
            int tamanhoPagina, int pagina
            , Func<SOLICITACAO, IComparable> ordenacao);
    }

    public interface IContratanteWebForLinkRepository : IRepository<Contratante>
    {
        int[] ListarTodosIds();
        Contratante BuscarPorId(int id, bool incluir);
        Contratante BuscarPorIdDocumentoSolicitado(int id);
        List<Contratante> ListarTodosPorGrupo(int idGrupo);
        List<Contratante> ListarTodosPorUsuario(int idUsuario);
        List<Contratante> ListarTodosPorPapel(int papelId);
        List<Contratante> ListarTodos(int grupoId);
        int[] ListarTodasAprovadas();
    }

    public interface IAplicacaoWebForLinkRepository : IRepository<APLICACAO>
    {
    }

    public interface IArquivosWebForLinkRepository : IRepository<ARQUIVOS>
    {
    }

    public interface IBancoWebForLinkRepository : IRepository<TiposDeBanco>
    {
    }

    public interface ICategoriaWebForLinkRepository : IRepository<FORNECEDOR_CATEGORIA>
    {
    }

    public interface ICompartilhamentoWebForLinkRepository : IRepository<Compartilhamentos>
    {
        RetornoPesquisa<Compartilhamentos> PesquisarInvertido(Expression<Func<Compartilhamentos, bool>> filtro, int tamanhoPagina, int pagina
            , Func<Compartilhamentos, IComparable> ordenacao);
    }

    public interface ITipoCadastroWebForLinkRepository : IRepository<TIPO_CADASTRO_FORNECEDOR>
    {
    }

    public interface ITipoDescricaoWebForLinkRepository : IRepository<TIPO_DESCRICAO>
    {
        List<TIPO_DESCRICAO> ListarPorGrupoId(int grupoId);
    }

    public interface ITipoDocumentosChWebForLinkRepository : IRepository<TIPO_DOCUMENTOS_CH>
    {
    }

    public interface ITipoDocumentoWebForLinkRepository : IRepository<TipoDeDocumento>
    {
    }

    public interface ITipoEnderecoWebForLinkRepository : IRepository<TIPO_ENDERECO>
    {
    }


    public interface IConfiguracaoWebForLinkRepository : IRepository<CONFIGURACAO>
    {
    }

    public interface ITipoFuncaoBloqueioWebForLinkRepository : IRepository<TIPO_FUNCAO_BLOQUEIO>
    {
    }

    public interface ITipoGrupoWebForLinkRepository : IRepository<TIPO_GRUPO>
    {
        List<TIPO_GRUPO> ListarGruposPorVisao(int visaoId);
    }

    public interface ITipoUnspscRepository : IRepository<TIPO_UNSPSC>
    {
    }

    public interface ITipoVisaoWebForLinkRepository : IRepository<TIPO_VISAO>
    {
        List<TIPO_VISAO> listarPorContratanteId(int idContratante);
    }

    public interface IEstadoWebForLinkRepository : IRepository<TiposDeEstado>
    {
        TiposDeEstado BuscarPorID(string sigla);
    }

    public interface IFornecedorBaseWebForLinkRepository : IRepository<FORNECEDORBASE>
    {
        void AlterarFornecedorbase(FORNECEDORBASE atual);
    }

    public interface ISolicitacaoServicoMaterialWebForLinkRepository : IRepository<SOLICITACAO_UNSPSC>
    {
        List<SOLICITACAO_UNSPSC> BuscarPorSolicitacaoId(int solicitacaoId);
    }

    public interface IContratanteFornecedorWebForLinkRepository : IRepository<WFD_CONTRATANTE_PJPF>
    {
        IEnumerable<WFD_CONTRATANTE_PJPF> BuscarCustomizado(Expression<Func<WFD_CONTRATANTE_PJPF, bool>> predicate);

        RetornoPesquisa<WFD_CONTRATANTE_PJPF> BuscarPesquisaCostumizada(
            Expression<Func<WFD_CONTRATANTE_PJPF, bool>> filtros, int tamanhoPagina, int pagina
            , Func<WFD_CONTRATANTE_PJPF, IComparable> ordenacao);
    }

    public interface IUsuarioWebForLinkRepository : IRepository<Usuario>
    {
        Usuario BuscarPorLoginParaAcesso(string login);
        Usuario BuscarPorLoginParaAcesso(string login, string senha);
        Usuario BuscarPorCpf(string cpf);
        Usuario BuscarPorEmail(string email);
        Usuario BuscarPorDocumento(string documento);
        Usuario ZerarTentativasLogin(Usuario usuario);
        List<Usuario> ListarPorIdContratante(int idContratante);
        bool VerificarLoginExistente(string login);
        bool ValidarPorEmail(string email);
        bool ValidarPorCnpj(string cnpj);
        void GravarLogAcesso(int usuarioId, string ip, string navegador);
        void ContabilizarErroLogin(Usuario usuario);
        void ExcluirUsuario(int id);
        bool Bloqueio90Dias(Usuario entidade);

        void IncluirNovoUsuarioPadrao(Usuario usuarioInclusao, USUARIO_SENHAS historicoSenhaInclusao, int[] papeis,
            int[] perfis);
    }

    public interface IUsuarioSenhasHistWebForLinkRepository : IRepository<USUARIO_SENHAS>
    {
        USUARIO_SENHAS BuscarPorIdComUsuario(int id);
        USUARIO_SENHAS BuscarHistoricoPorLogin(string login);
        USUARIO_SENHAS BuscarHistoricoPorIdUsuario(int id);
        List<USUARIO_SENHAS> ListarPorIdContratante(int idContratante);
        List<USUARIO_SENHAS> Listar6UltimasPorUsuarioId(int idUsuario);
    }
}