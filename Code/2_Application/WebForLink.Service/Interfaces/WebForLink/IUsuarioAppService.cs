using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;

namespace WebForLink.Application.Interfaces.WebForLink
{
    public interface IFornecedorCategoriaWebForLinkAppService
                : IAppService<FORNECEDOR_CATEGORIA>, IReadOnlyAppService<FORNECEDOR_CATEGORIA>
    {
        RetornoPesquisa<FORNECEDOR_CATEGORIA> PesquisarCategorias(string descricao, string codigo, int contratanteId, int tamanhoPagina, int paginaAtual);
        List<FORNECEDOR_CATEGORIA> BuscarCategorias(int contratanteId);
        List<FORNECEDOR_CATEGORIA> ListarTodosPorIdContratanteAtivo(int contratanteId);
        List<FORNECEDOR_CATEGORIA> BuscarPorCategoriaPai(int idPai, int contratanteId);
        FORNECEDOR_CATEGORIA Buscar(Expression<Func<FORNECEDOR_CATEGORIA, bool>> p);
        FORNECEDOR_CATEGORIA BuscarPorId(int id, int contratanteId);
        void InserirCategoria(FORNECEDOR_CATEGORIA categoriaInserir);
        void AlterarCategoria(FORNECEDOR_CATEGORIA categoriaInserir);
        void ExcluirCategoriaDireto(FORNECEDOR_CATEGORIA categoriaInserir);
        FORNECEDOR_CATEGORIA BuscarPorId(int categoriaId);
        FORNECEDOR_CATEGORIA BuscarEmSolicitacaoFornecedor(int contratanteId, int categoria);
    }
    public interface ICriacaoContratanteWebForLinkAppService
                    : IAppService<Fornecedor>, IReadOnlyAppService<Fornecedor>
    {
    }

    public interface IFichaCadastralWebForLinkAppService
                    : IAppService<Fornecedor>, IReadOnlyAppService<Fornecedor>
    {
        Fornecedor BuscarFichaCadastralMeuContratante(int contratanteId);
    }
    public interface IQuestionarioRespostaService
        : IAppService<QUESTIONARIO_RESPOSTA>//, IReadOnlyAppService<QUESTIONARIO_RESPOSTA>
    {
    }

    public interface IFornecedorSolicitacaoWebForLinkService
        : IReadOnlyAppService<FORNECEDOR_SOLICITACAO> //, IAppService<FORNECEDOR_SOLICITACAO>
    {
    }

    public interface ISolicitacaoWebForLinkAppService
        : IAppService<SOLICITACAO>, IReadOnlyAppService<SOLICITACAO>
    {
        List<int> BuscarSolicitacaoAguardandoCarga();
        SOLICITACAO BuscarPorIdControleSolicitacoes(int idSolicitacao);
        SOLICITACAO BuscarPorIdComDocumentos(int solicitacaoCriacaoId);
        SOLICITACAO BuscarPorIdDocumentosSolicitados(int solicitacaoId);
        SOLICITACAO BuscarPorId(int solicitacaoId);
        SOLICITACAO BuscarAprovacaoPorId(int idSolicitacao);
        SOLICITACAO BuscarPorSolicitacaoId(int solicitacaoId);
        SOLICITACAO BuscarPorIdIncluindoFluxo(int solicitacaoId);
        SOLICITACAO InserirSolicitacaoDocumentos(SOLICITACAO solicitacao, List<SolicitacaoDeDocumentos> documentosList);
        SOLICITACAO CadastrarSolicitacaoNovoFornecedor(CadastrarSolicitacaoDTO modeloCadastro);
        SOLICITACAO InserirSolicitacao(SOLICITACAO solicitacao);
        SOLICITACAO BuscarPorIdComSolicitacaoCadastroFornecedor(int solicitacaoId);
        SOLICITACAO BuscarPorIdComFornecedoresDireto(int solicitacaoId);
        SOLICITACAO CadastrarSolicitacaoPreCadastro(int iD, CadastrarSolicitacaoDTO modeloCadastro);
        SOLICITACAO Buscar(Func<SOLICITACAO, bool> func);
        List<SOLICITACAO> ListarSolicitacaoCarga(int contratanteId);
        void Alterar(SOLICITACAO solicitacao);
        void AlterarAprovacao(int solicitacaoId, int contratanteId, int fluxoId, string motivoReprovacao, int usuarioId);
        void CriarSolicitacaoBloqueio(SOLICITACAO solicitacao, SOLICITACAO_BLOQUEIO bloqueio);
        List<int> BuscarSolicitacaoAguardandoRetornoCarga();
        RetornoPesquisa<SOLICITACAO> BuscarPesquisa(Expression<Func<SOLICITACAO, bool>> expression, int tamanhoPagina, int pagina, 
        Func<SOLICITACAO, IComparable> p);
        int BuscarTipoFluxoId(int idSolicitacao);
        void AlterarSolicitacaoMensagem(SOLICITACAO solicitacao);
        List<int> BuscarPorContratanteFornecedorTipoFluxoEStatus(int contratanteId, int? fornecedorId, int tipoFluxoId, int aguardando);
        void AlterarSolicitacaoParaFinalizado(int solicitacaoId, int statusId);
    }

    public interface ISolicitacaoDadosFiscaisWebForLinkAppService
        : IAppService<CONTRATANTE_CONFIGURACAO_EMAIL>//, IReadOnlyAppService<CONTRATANTE_CONFIGURACAO_EMAIL>
    {
    }

    public interface IConfiguracaoEmailContratanteWebForLinkAppService
        : IAppService<CONTRATANTE_CONFIGURACAO_EMAIL>//, IReadOnlyAppService<CONTRATANTE_CONFIGURACAO_EMAIL>
    {
        CONTRATANTE_CONFIGURACAO_EMAIL BuscarPorContratanteETipo(int contratanteId, int emailTipoId);
    }

    public interface IAprovacaoWebForLinkAppService
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

    public interface ICadastroUnicoWebForLinkAppService
        : IAppService<QUESTIONARIO>//, IReadOnlyAppService<QUESTIONARIO>
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

    public interface ISolicitacaoStatusWebForLinkAppService
        : IAppService<SOLICITACAO_STATUS>//, IReadOnlyAppService<SOLICITACAO_STATUS>
    {
    }
    public interface ISolicitacaoMaterialEServicoWebForLinkAppService
        : IAppService<SOLICITACAO_UNSPSC>//, IReadOnlyAppService<SOLICITACAO_UNSPSC>
    {
        SOLICITACAO_UNSPSC BuscarPorId(int id);
        void ManterUnspscSolicitacao(List<SOLICITACAO_UNSPSC> unscpsc, int idSolicitacao);
        void InserirSolicitacoes(List<SOLICITACAO_UNSPSC> unscpsc);
    }

    public interface ITipoDocumentosWebForLinkAppService
        : IAppService<TipoDeDocumento>//, IReadOnlyAppService<TipoDeDocumento>
    {
        List<TipoDeDocumento> ListarPorIdContratante(int contratanteId);
    }
    public interface ISolicitacaoDesbloqueioWebForLinkAppService
        : IAppService<SOLICITACAO_DESBLOQUEIO>, IReadOnlyAppService<SOLICITACAO_DESBLOQUEIO>
    {
    }

    public interface ISolicitacaoDocumentoWebForLinkAppService
        : IAppService<SolicitacaoDeDocumentos>, IReadOnlyAppService<SolicitacaoDeDocumentos>
    {
        List<SolicitacaoDeDocumentos> ListarPorIdSolicitacao(int id);
        SolicitacaoDeDocumentos BuscarPorIdSolicitacaoIdDescricaoDocumento(int solicitacaoId, int descricaoDocumentoId);
    }

    public interface ISolicitacaoModificacaoEnderecoWebForLinkAppService
        : IAppService<SOLICITACAO_MODIFICACAO_ENDERECO>, IReadOnlyAppService<SOLICITACAO_MODIFICACAO_ENDERECO>
    {
        SOLICITACAO_MODIFICACAO_ENDERECO BuscarPorId(int id);
        List<SOLICITACAO_MODIFICACAO_ENDERECO> ListarPorSolicitacaoId(int solicitacaoId);
        void InserirSolicitacao(SOLICITACAO_MODIFICACAO_ENDERECO solicitacaoEndereco, SOLICITACAO solicitacao);
        void InserirSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes, SOLICITACAO solicitacao);
        void InserirOuAtualizarSolicitacao(SOLICITACAO_MODIFICACAO_ENDERECO solicitacao);
        void InserirOuAtualizarSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes);
        void ExcluirSolicitacoes(List<SOLICITACAO_MODIFICACAO_ENDERECO> solicitacoes);
    }

    public interface ISolicitacaoMensagemWebForLinkAppService
        : IAppService<SOLICITACAO_MENSAGEM>, IReadOnlyAppService<SOLICITACAO_MENSAGEM>
    {
        void InserirMensagem(SOLICITACAO_MENSAGEM mensagem, List<SolicitacaoDeDocumentos> docs);
    }

    public interface ISolicitacaoModificacaoContatoWebForLinkAppService
        : IAppService<SolicitacaoModificacaoDadosContato>, IReadOnlyAppService<SolicitacaoModificacaoDadosContato>
    {
        SolicitacaoModificacaoDadosContato BuscarPorId(int id);
        List<SolicitacaoModificacaoDadosContato> ListarPorSolicitacaoId(int solicitacaoId);
        void InserirSolicitacao(SolicitacaoModificacaoDadosContato solicitacao);
        void InserirSolicitacoes(List<SolicitacaoModificacaoDadosContato> solicitacoes);
        void InserirOuAtualizarSolicitacao(SolicitacaoModificacaoDadosContato solicitacao);
        void InserirOuAtualizarSolicitacoes(List<SolicitacaoModificacaoDadosContato> solicitacoes);
        void ExcluirSolicitacoes(List<SolicitacaoModificacaoDadosContato> solicitacoes);
        SOLICITACAO IncluirSolicitacao(int contratanteId, int fornecedorId, int usuarioId, int tipoFluxoId);
        void ManterContatoCadastroFornecedor(List<SolicitacaoModificacaoDadosContato> contatos, int solicitacaoId);
    }


    public interface IQuestionarioAbaPerguntaPapelWebForLinkAppService
        : IAppService<QUESTIONARIO_PAPEL>, IReadOnlyAppService<QUESTIONARIO_PAPEL>
    {
    }

    public interface IQuestionarioAbaPerguntaRespostaWebForLinkAppService
        : IAppService<QUESTIONARIO_RESPOSTA>, IReadOnlyAppService<QUESTIONARIO_RESPOSTA>
    {
    }

    public interface IQuestionarioAbaPerguntaWebForLinkAppService
        : IAppService<QUESTIONARIO_PERGUNTA>, IReadOnlyAppService<QUESTIONARIO_PERGUNTA>
    {
    }

    public interface IQuestionarioAbaWebForLinkAppService
        : IAppService<QUESTIONARIO_ABA>, IReadOnlyAppService<QUESTIONARIO_ABA>
    {
    }

    public interface IQuestionarioWebForLinkAppService
        : IAppService<QUESTIONARIO>, IReadOnlyAppService<QUESTIONARIO>
    {
    }

    public interface IRoboLogWebForLinkAppService
        : IAppService<ROBO_LOG>, IReadOnlyAppService<ROBO_LOG>
    {
    }

    public interface IRoboWebForLinkAppService
        : IAppService<ROBO>, IReadOnlyAppService<ROBO>
    {
    }

    public interface IRoboSintegraWebForLinkAppService
        : IAppService<SolicitacaoCadastroFornecedor>, IReadOnlyAppService<SolicitacaoCadastroFornecedor>
    {
    }

    public interface ISolicitacaoBloqueioWebForLinkAppService
        : IAppService<SOLICITACAO_BLOQUEIO>, IReadOnlyAppService<SOLICITACAO_BLOQUEIO>
    {
    }

    public interface ISolicitacaoCadastroFornecedorWebForLinkAppService
        : IAppService<SolicitacaoCadastroFornecedor>, IReadOnlyAppService<SolicitacaoCadastroFornecedor>
    {
        SolicitacaoCadastroFornecedor BuscarPorId(int id);
        void Alterar(SolicitacaoCadastroFornecedor solForn);
        SolicitacaoCadastroFornecedor BuscarPorRazaoSocial(string razaoSocial);
        SolicitacaoCadastroFornecedor BuscarPorStatusId(string cnpjteste, int statusId);
        SolicitacaoCadastroFornecedor ValidarSolicitacaoCriacao(int statusSolicitacao, string documento, int contratante);
        int BuscarIdSolicitacaoPorCnpj(string cnpj);
        SolicitacaoCadastroFornecedor BuscarPorSolicitacaoId(int solicitacaoId);
        SolicitacaoCadastroFornecedor BuscarPorSolicitacaoIdComDocumentos(int solicitacaoId);
        void AtualizarSolicitacao(SolicitacaoCadastroFornecedor solicitacao);
        string BuscarRazaoSocialPorCnpj(string cnpj);
        string BuscarRazaoOuNomePorSolicitacao(int solicitacaoId);
        string BuscarRazaoSocialPorCpf(string cpf);
        SolicitacaoCadastroFornecedor Buscar(Expression<Func<SolicitacaoCadastroFornecedor, bool>> filtro);
    }

    public interface ISolicitacaoModificacaoBancoWebForLinkAppService
        : IAppService<SolicitacaoModificacaoDadosBancario>, IReadOnlyAppService<SolicitacaoModificacaoDadosBancario>
    {
        SolicitacaoModificacaoDadosBancario BuscarPorId(int id);
        List<SolicitacaoModificacaoDadosBancario> ListarPorSolicitacaoId(int solicitacaoId);
        void InserirSolicitacao(SolicitacaoModificacaoDadosBancario solicitacao);
        void InserirSolicitacoes(List<SolicitacaoModificacaoDadosBancario> solicitacoes);
        void InserirOuAtualizarSolicitacao(SolicitacaoModificacaoDadosBancario solicitacao);
        void InserirOuAtualizarSolicitacoes(List<SolicitacaoModificacaoDadosBancario> solicitacoes);
        void ExcluirSolicitacoes(List<SolicitacaoModificacaoDadosBancario> solicitacoes);
        void InserirSolicitacoes(List<SolicitacaoModificacaoDadosBancario> bancos, SOLICITACAO solicitacao);
        void ManterBancoCadastroFornecedor(List<SolicitacaoModificacaoDadosBancario> bancos, int solicitacaoId);
    }


    public interface IPreCadastroWebForLinkAppService
        : IAppService<FORNECEDORBASE>, IReadOnlyAppService<FORNECEDORBASE>
    {
        int ContratanteId { get; set; }
        string DocumentoFornecedor { get; set; }
        FORNECEDORBASE FornecedorBase { get; set; }
        List<FORNECEDORBASE_ENDERECO> FornecedorBaseEndereco { get; set; }
        List<FORNECEDORBASE_CONTATOS> FornecedorBaseContato { get; set; }
        List<FORNECEDORBASE_UNSPSC> FornecedoresBaseUnspsc { get; set; }
        int PjpfBaseId { get; set; }
        void IncluirPreCadastro(CasosPreCadastroEnum preCadastroEnum, int acao);
        RetornoPesquisa<FORNECEDORBASE> ListarTodos(PreCadastroFiltrosDTO filtros, int pagina, int tamanhoPagina);
    }

    public interface IPerfilWebForLinkAppService
        : IAppService<Perfil>, IReadOnlyAppService<Perfil>
    {
        List<Perfil> ListarTodos();
        List<Perfil> ListarTodos(int[] perfilId);
        List<Perfil> ListarTodosPorContratante(int id);
        RetornoPesquisa<Perfil> PesquisarPerfil(PesquisaPerfilFiltrosDTO filtros, int pagina, int tamanhoPagina);
        Perfil BuscarPorId(int id);
        Perfil AlterarPerfil(Perfil entidade, int[] idfuncoes);
        Perfil InserirPerfil(Perfil entidade);
        Perfil InserirPerfilFuncoes(Perfil entidade);
        Perfil ExcluirPerfil(int id);
    }

    public interface IFornecedorStatusWebForLinkAppService
        : IAppService<FORNECEDOR_STATUS>, IReadOnlyAppService<FORNECEDOR_STATUS>
    {
    }

    public interface IAbaPerguntaWebForLinkAppService
        : IAppService<QUESTIONARIO_PERGUNTA>, IReadOnlyAppService<QUESTIONARIO_PERGUNTA>
    {
    }

    public interface IAcessoLogWebForLinkAppService
        : IAppService<WAC_ACESSO_LOG>, IReadOnlyAppService<WAC_ACESSO_LOG>
    {
        void GravarLogAcesso(int iD, string v1, string v2);
    }

    public interface IAplicacaoWebForLinkAppService
        : IAppService<APLICACAO>, IReadOnlyAppService<APLICACAO>
    {
        void AlterarAplicacao(APLICACAO aplicacao);
        APLICACAO BuscarPorId(int id);
        object BuscarPorIdNomeAplicacao(int id, string nome);
        object BuscarPorNome(string nome);
        void ExcluirAplicacao(int id);
        void InserirAplicacao(APLICACAO aplicacao);
        IEnumerable ListarTodos();
        RetornoPesquisa<APLICACAO> PesquisarAplicacao(PesquisaAplicacaoFiltrosDTO filtros, int pagina, int v);
    }

    public interface ICategoriaFornecedorChWebForLinkAppService
        : IAppService<FORNECEDOR_CATEGORIA_CH>, IReadOnlyAppService<FORNECEDOR_CATEGORIA_CH>
    {
    }

    public interface IDescricaoDocumentosChWebForLinkAppService
        : IAppService<WFD_DESCRICAO_DOCUMENTOS_CH>, IReadOnlyAppService<WFD_DESCRICAO_DOCUMENTOS_CH>
    {
    }

    public interface IDescricaoDocumentosWebForLinkAppService
        : IAppService<DescricaoDeDocumentos>, IReadOnlyAppService<DescricaoDeDocumentos>
    {
    }

    public interface IFornecedorServicoMaterialWebForLinkAppService : IAppService<FORNECEDOR_UNSPSC>,
        IReadOnlyAppService<FORNECEDOR_UNSPSC>
    {
        FORNECEDOR_UNSPSC BuscarPorId(int id);
        void GravaUnspscNoPjPf(List<FORNECEDOR_UNSPSC> unspscs, int fornecedorId, int contratanteId, int usuarioId);
        List<FORNECEDOR_UNSPSC> BuscarPorFornecedorId(int fornecedorId);
    }

    public interface IFornecedorArquivoWebForLinkAppService : IAppService<ARQUIVOS>, IReadOnlyAppService<ARQUIVOS>
    {
        ARQUIVOS BuscarPorId(int arquivoId);
        int GravarArquivoSolicitacao(int contratanteId, string arquivo, string tipoArquivo);

        int SubstituirArquivoSolicitacaoDocumento(int contratanteId, int idDocumento, int idArquivo,
            string arquivoSubido, string tipoArquivoSubido, string nomeArquivoCadastrado);

        int SubstituirArquivoSolicitacaoBancario(int contratanteId, int idBanco, int idArquivo, string arquivoSubido,
            string tipoArquivoSubido, string nomeArquivoCadastrado);

        int SubstituirArquivoMeusBancario(int contratanteId, int idBanco, int idArquivo, string arquivoSubido,
            string tipoArquivoSubido, string nomeArquivoCadastrado);

        string PegaNomeArquivoSubido(string arquivoSubido);
    }

    public interface IVisaoWebForLinkAppService : IAppService<TIPO_VISAO>, IReadOnlyAppService<TIPO_VISAO>
    {
        List<TIPO_VISAO> ListarTodos();
    }

    public interface IUsuarioAppService : IAppService<Usuario>, IReadOnlyAppService<Usuario>
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
}