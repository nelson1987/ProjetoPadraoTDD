using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;

namespace WebForLink.Domain.Aggregates
{
    public abstract class AgregateCriacaoSolicitacao
    {
        public AgregateCriacaoSolicitacao(SOLICITACAO solicitacao)
        {
            Solicitacao = solicitacao;
            Solicitacao.DT_PRAZO = DateTime.Now.AddDays(ContratanteConfiguracao.PRAZO_ENTREGA_FICHA);
            Solicitacao.FLUXO_ID = Fluxo.ID;
        }

        public AgregateCriacaoSolicitacao(SOLICITACAO solicitacao, ROBO roboSolicitacao)
        {
            Solicitacao = solicitacao;
            RoboAtual = roboSolicitacao;
            //Solicitacao.AdicionarRobo(RoboAtual);
        }

        public SOLICITACAO Solicitacao { get; private set; }
        public Fluxo Fluxo { get; private set; }
        public ROBO RoboAtual { get; private set; }
        public CONTRATANTE_CONFIGURACAO ContratanteConfiguracao { get; private set; }
    }

    public class AmpliacaoFornecedor : AgregateCriacaoSolicitacao
    {
        public AmpliacaoFornecedor(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    public class AtualizacaoDocumento : AgregateCriacaoSolicitacao
    {
        public AtualizacaoDocumento(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    #region Bloqueio E Desbloqueio

    public class BloqueioFornecedor : AgregateCriacaoSolicitacao
    {
        public BloqueioFornecedor(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    public class DesbloqueioFornecedor : AgregateCriacaoSolicitacao
    {
        public DesbloqueioFornecedor(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    #endregion

    #region Cadastro Fornecedor

    public class CadastroFornecedorPessoaFisica : AgregateCriacaoSolicitacao
    {
        public CadastroFornecedorPessoaFisica(int contratanteId, int usuarioId, int categoria, string cpnj,
            string telefone, string nomeContato, string email)
            : base(new SOLICITACAO
            {
                CONTRATANTE_ID = contratanteId,
                SOLICITACAO_DT_CRIA = DateTime.Now,
                USUARIO_ID = usuarioId,
                SOLICITACAO_STATUS_ID = (int) EnumStatusTramite.EmAprovacao
            })
        {
        }

        public CadastroFornecedorPessoaFisica(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }

        public CadastroFornecedorPessoaFisica(SOLICITACAO solicitacao, ROBO roboSolicitacao)
            : base(solicitacao, roboSolicitacao)
        {
            //Solicitacao.AdicionarRobo(roboSolicitacao);
        }

        public void CriarSolicitacao()
        {
        }
    }

    public class CadastroFornecedorPessoaFisicaDireto : AgregateCriacaoSolicitacao
    {
        public CadastroFornecedorPessoaFisicaDireto(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    public class CadastroFornecedorPessoaJuridica : AgregateCriacaoSolicitacao
    {
        public CadastroFornecedorPessoaJuridica(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    public class CadastroFornecedorPessoaJuridicaDireto : AgregateCriacaoSolicitacao
    {
        public CadastroFornecedorPessoaJuridicaDireto(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    public class CadastroFornecedorEstrangeiro : AgregateCriacaoSolicitacao
    {
        public CadastroFornecedorEstrangeiro(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    #endregion

    #region Modificacao

    public class ModificacaoBancaria : AgregateCriacaoSolicitacao
    {
        public ModificacaoBancaria(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    public class ModificacaoContatos : AgregateCriacaoSolicitacao
    {
        public ModificacaoContatos(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    public class ModificacaoFiscais : AgregateCriacaoSolicitacao
    {
        public ModificacaoFiscais(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    public class ModificacaoEndereco : AgregateCriacaoSolicitacao
    {
        public ModificacaoEndereco(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    public class ModificacaoInformacaoComplementar : AgregateCriacaoSolicitacao
    {
        public ModificacaoInformacaoComplementar(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    public class ModificacaoServicoEMateriais : AgregateCriacaoSolicitacao
    {
        public ModificacaoServicoEMateriais(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    public class ModificacaoGerais : AgregateCriacaoSolicitacao
    {
        public ModificacaoGerais(SOLICITACAO solicitacao)
            : base(solicitacao)
        {
        }
    }

    #endregion
}