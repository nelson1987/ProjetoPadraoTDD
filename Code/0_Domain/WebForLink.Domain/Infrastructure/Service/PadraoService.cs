using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Domain.Infrastructure.Service
{
    public interface ISolicitacaoService
    {
        int BuscarTipoFluxoId(int solicitacaoId);
        void AlterarAprovacao(int SolicitacaoID, int ContratanteID, int FluxoID, string motivoReprovacao, int usuarioId);
        int[] BuscarSolicitacaoAguardandoCarga();
        int[] BuscarSolicitacaoAguardandoRetornoCarga();
        SOLICITACAO Alterar(SOLICITACAO solicitacao);
        SOLICITACAO AlterarSolicitacaoMensagem(SOLICITACAO solicitacao);
        SOLICITACAO BuscarPorIdIncluindoFluxo(int id);
        SOLICITACAO BuscarAprovacaoPorId(int id);
        SOLICITACAO BuscarPorSolicitacaoId(int id);
        SOLICITACAO BuscarPorIdComSolicitacaoCadastroPJPF(int id);
        SOLICITACAO BuscarPorIdControleSolicitacoes(int id);
        SOLICITACAO BuscarPorIdComFornecedoresDireto(int id);
        SOLICITACAO BuscarPorIdDocumentosSolicitados(int id);
        SOLICITACAO BuscarPorIdComDocumentos(int id);
        SOLICITACAO BuscarPorId(int id);
        SOLICITACAO Buscar(Expression<Func<SOLICITACAO, bool>> filtro);
        SOLICITACAO InserirSolicitacao(SOLICITACAO solicitacao);
        SOLICITACAO InserirSolicitacaoDocumentos(SOLICITACAO solicitacao, List<SolicitacaoDeDocumentos> documentos);

        SOLICITACAO IncluirNovaSolicitacaoFornecedor(SOLICITACAO solicitacao, SolicitacaoCadastroFornecedor cadastro,
            SolicitacaoModificacaoDadosContato contato, ROBO robo, List<SolicitacaoDeDocumentos> documentos);

        SOLICITACAO ConvidarFornecedorComSolicitacao(SOLICITACAO solicitacao, SolicitacaoCadastroFornecedor cadastro,
            SolicitacaoModificacaoDadosContato contato, ROBO robo, List<SolicitacaoDeDocumentos> documentos,
            FORNECEDORBASE_CONVITE convite, SOLICITACAO_MENSAGEM mensagem);

        SOLICITACAO CadastrarSolicitacaoPreCadastro(int idPjPfBase, CadastrarSolicitacaoDTO cadastro);
        SOLICITACAO CadastrarSolicitacaoNovoFornecedor(CadastrarSolicitacaoDTO cadastro);
        SOLICITACAO BuscarSolicitacaoFinalizaCriacaoFornecedor(int solicitacaoId);
        List<SOLICITACAO> ListarSolicitacaoCarga(int idContratante);
        List<SOLICITACAO> ListarTodasSolicitacoesAprovadas(int idSolicitacao);
        List<SOLICITACAO> ListarSolicitacaoAprovadaPorId();
        List<int> ListarPorId(int contratanteId, int? fornecedorId, int tipoFluxoId, int statusId);

        List<int> BuscarPorContratanteFornecedorTipoFluxoEStatus(int contratanteId, int? fornecedorId, int tipoFluxoId,
            int statusId);

        RetornoPesquisa<SOLICITACAO> BuscarPesquisa(Expression<Func<SOLICITACAO, bool>> filtro, int tamanhoPagina,
            int pagina, Func<SOLICITACAO, IComparable> ordenacao);

        RetornoPesquisa<SOLICITACAO> BuscarPesquisaAcompanhamento(Expression<Func<SOLICITACAO, bool>> filtro,
            int tamanhoPagina, int pagina, Func<SOLICITACAO, IComparable> ordenacao);

        void CriarSolicitacaoBloqueio(SOLICITACAO solicitacao, SOLICITACAO_BLOQUEIO bloqueio);
        void Dispose();
    }
}