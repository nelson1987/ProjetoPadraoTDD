using System;
using System.Collections.Generic;
using System.Linq;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class SOLICITACAO
    {
        public SOLICITACAO()
        {
            WFD_INFORM_COMPL = new List<WFD_INFORM_COMPL>();
            WFD_PJPF_BASE_CONVITE = new List<FORNECEDORBASE_CONVITE>();
            DocumentosDoFornecedor = new List<DocumentosDoFornecedor>();
            ROBO = new List<ROBO>();
            WFD_PJPF_ROBO_LOG = new List<ROBO_LOG>();
            WFD_PJPF_SOLICITACAO_DOCUMENTOS = new List<FORNECEDOR_SOLICITACAO_DOCUMENTOS>();
            SOLICITACAO_BLOQUEIO = new List<SOLICITACAO_BLOQUEIO>();
            SolicitacaoCadastroFornecedor = new List<SolicitacaoCadastroFornecedor>();
            WFD_SOL_DESBLOQ = new List<SOLICITACAO_DESBLOQUEIO>();
            SolicitacaoDeDocumentos = new List<SolicitacaoDeDocumentos>();
            WFD_SOL_MENSAGEM = new List<SOLICITACAO_MENSAGEM>();
            SolicitacaoModificacaoDadosBancario = new List<SolicitacaoModificacaoDadosBancario>();
            SolicitacaoModificacaoDadosContato = new List<SolicitacaoModificacaoDadosContato>();
            WFD_SOL_MOD_DGERAIS_SEQ = new List<SOLICITACAO_MODIFICACAO_DADOSGERAIS>();
            WFD_SOL_MOD_ENDERECO = new List<SOLICITACAO_MODIFICACAO_ENDERECO>();
            WFD_SOL_UNSPSC = new List<SOLICITACAO_UNSPSC>();
            WFD_SOLICITACAO_TRAMITE = new List<SOLICITACAO_TRAMITE>();
            WFD_SOLICITACAO_PRORROGACAO = new List<SOLICITACAO_PRORROGACAO>();
        }

        public int ID { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public int FLUXO_ID { get; set; }
        public DateTime SOLICITACAO_DT_CRIA { get; set; }
        public int? USUARIO_ID { get; set; }
        public int? SOLICITACAO_STATUS_ID { get; set; }
        public string MOTIVO { get; set; }
        public int? PJPF_ID { get; set; }
        public string TP_PJPF { get; set; }
        public DateTime? DT_PRAZO { get; set; }
        public DateTime? DT_PRORROGACAO_PRAZO { get; set; }
        public string MOTIVO_PRORROGACAO { get; set; }
        public int? PJPF_BASE_ID { get; set; }
        public bool ROBO_EXECUTADO { get; set; }
        public bool ROBO_TENTATIVAS_EXCEDIDAS { get; set; }
        public virtual Contratante Contratante { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
        public virtual FORNECEDORBASE FORNECEDORBASE { get; set; }
        public virtual SOLICITACAO_STATUS WFD_SOLICITACAO_STATUS { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual Fluxo Fluxo { get; set; }
        public virtual ICollection<WFD_INFORM_COMPL> WFD_INFORM_COMPL { get; set; }
        public virtual ICollection<FORNECEDORBASE_CONVITE> WFD_PJPF_BASE_CONVITE { get; set; }
        public virtual ICollection<DocumentosDoFornecedor> DocumentosDoFornecedor { get; set; }
        public virtual ICollection<ROBO> ROBO { get; set; }
        public virtual ICollection<ROBO_LOG> WFD_PJPF_ROBO_LOG { get; set; }
        public virtual ICollection<FORNECEDOR_SOLICITACAO_DOCUMENTOS> WFD_PJPF_SOLICITACAO_DOCUMENTOS { get; set; }
        public virtual ICollection<SOLICITACAO_BLOQUEIO> SOLICITACAO_BLOQUEIO { get; set; }
        public virtual ICollection<SolicitacaoCadastroFornecedor> SolicitacaoCadastroFornecedor { get; set; }
        public virtual ICollection<SOLICITACAO_DESBLOQUEIO> WFD_SOL_DESBLOQ { get; set; }
        public virtual ICollection<SolicitacaoDeDocumentos> SolicitacaoDeDocumentos { get; set; }
        public virtual ICollection<SOLICITACAO_MENSAGEM> WFD_SOL_MENSAGEM { get; set; }
        public virtual ICollection<SolicitacaoModificacaoDadosBancario> SolicitacaoModificacaoDadosBancario { get; set; }
        public virtual ICollection<SolicitacaoModificacaoDadosContato> SolicitacaoModificacaoDadosContato { get; set; }
        public virtual ICollection<SOLICITACAO_MODIFICACAO_DADOSGERAIS> WFD_SOL_MOD_DGERAIS_SEQ { get; set; }
        public virtual ICollection<SOLICITACAO_MODIFICACAO_ENDERECO> WFD_SOL_MOD_ENDERECO { get; set; }
        public virtual ICollection<SOLICITACAO_UNSPSC> WFD_SOL_UNSPSC { get; set; }
        public virtual ICollection<SOLICITACAO_TRAMITE> WFD_SOLICITACAO_TRAMITE { get; set; }
        public virtual ICollection<SOLICITACAO_PRORROGACAO> WFD_SOLICITACAO_PRORROGACAO { get; set; }

        public ROBO RoboAtual()
        {
            return ROBO.OrderByDescending(x => x.ROBO_DT_EXEC).FirstOrDefault();
        }

        public bool CadastroFornecedor(SOLICITACAO item)
        {
            return item.Fluxo.CadastroFornecedor(item.Fluxo);
        }

        public void AdicionarRobo(ROBO solrobo)
        {
            ROBO.Add(solrobo);
        }

        public void AdicionarSolicitacaoCadastroFornecedor(SolicitacaoCadastroFornecedor solForn)
        {
            SolicitacaoCadastroFornecedor.Add(solForn);
        }

        public void AdicionarSolicitacaoModificacaoContato(SolicitacaoModificacaoDadosContato solContato)
        {
            SolicitacaoModificacaoDadosContato.Add(solContato);
        }

        public void IncluirRoboCriacaoFornecedor()
        {
            var robo = ROBO.FirstOrDefault();

            if (robo != null)
            {
                if (robo.RF_CONSULTA_DTHR != null && robo.SINT_CONSULTA_DTHR != null && robo.SN_CONSULTA_DTHR != null)
                {
                    ROBO_EXECUTADO = true;
                }
            }
        }
    }
}