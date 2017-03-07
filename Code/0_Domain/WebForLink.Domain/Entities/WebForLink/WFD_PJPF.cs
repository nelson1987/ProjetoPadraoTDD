using System;
using System.Collections.Generic;
using System.Linq;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class Fornecedor
    {
        public Fornecedor()
        {
            WFD_CONTRATANTE_PJPF = new List<WFD_CONTRATANTE_PJPF>();
            WFD_PJPF_CONTRATANTE_ORG_COMPRAS = new List<WFD_PJPF_CONTRATANTE_ORG_COMPRAS>();
            DocumentosDoFornecedor = new List<DocumentosDoFornecedor>();
            ROBO_LOG = new List<ROBO_LOG>();
            FornecedorServicoMaterialList = new List<FORNECEDOR_UNSPSC>();
            SolicitacaoModificacaoEnderecoList = new List<SOLICITACAO_MODIFICACAO_ENDERECO>();
            SolicitacaoList = new List<SOLICITACAO>();
        }

        public int ID { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public bool ATIVO { get; set; }
        public string RAZAO_SOCIAL { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string TELEFONE { get; set; }
        public string NOME { get; set; }
        public string NOME_FANTASIA { get; set; }
        //public string CNAE { get; set; }
        public string INSCR_ESTADUAL { get; set; }
        public string INSCR_MUNICIPAL { get; set; }
        public string ENDERECO { get; set; }
        public string NUMERO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string CEP { get; set; }
        public string BAIRRO { get; set; }
        public string CIDADE { get; set; }
        public string UF { get; set; }
        public string PAIS { get; set; }
        public string EMAIL { get; set; }
        public string RF_SIT_CADASTRAL_CNPJ { get; set; }
        public string IBGE_COD { get; set; }
        public string SINT_IE_COD { get; set; }
        public string SINT_IE_SITU_CADASTRAL { get; set; }
        public string SIMPLES_NACIONAL_SITUACAO { get; set; }
        public string SUFRAMA_SIT_CADASTRAL { get; set; }
        public string SUFRAMA_INSCRICAO { get; set; }
        public int? TIPO_PJPF_ID { get; set; }
        public int? SITUACAO_ID { get; set; }
        public int? ROBO_ID { get; set; }
        public DateTime? RF_SIT_CADASTRAL_CNPJ_DT { get; set; }
        public DateTime? RF_CONSULTA_DTHR { get; set; }
        public DateTime? SINT_DTHR_CONSULTA { get; set; }
        public DateTime? SINT_IE_SITU_CADASTRAL_DT { get; set; }
        public DateTime? SUFRAMA_SIT_CADASTRAL_VALIDADE { get; set; }
        public DateTime? DT_NASCIMENTO { get; set; }
        public DateTime? DT_ATUALIZACAO_UNSPSC { get; set; }
        public virtual TiposDeEstado T_UF { get; set; }
        public virtual Contratante Contratante { get; set; }
        public virtual ROBO ROBO { get; set; }
        public virtual ICollection<WFD_CONTRATANTE_PJPF> WFD_CONTRATANTE_PJPF { get; set; }
        public virtual ICollection<WFD_PJPF_CONTRATANTE_ORG_COMPRAS> WFD_PJPF_CONTRATANTE_ORG_COMPRAS { get; set; }
        public virtual ICollection<DocumentosDoFornecedor> DocumentosDoFornecedor { get; set; }
        public virtual ICollection<ROBO_LOG> ROBO_LOG { get; set; }
        public virtual ICollection<FORNECEDOR_UNSPSC> FornecedorServicoMaterialList { get; set; }
        public virtual ICollection<SOLICITACAO_MODIFICACAO_ENDERECO> SolicitacaoModificacaoEnderecoList { get; set; }
        public virtual ICollection<SOLICITACAO> SolicitacaoList { get; set; }

        public bool FornecedorConvencional(Fornecedor fornecedor)
        {
            return fornecedor.ATIVO && fornecedor.TIPO_PJPF_ID == 2;
        }

        public bool FornecedorConvencional(Fornecedor fornecedor, int idContratante)
        {
            return fornecedor.ATIVO && fornecedor.TIPO_PJPF_ID == 2 && fornecedor.CONTRATANTE_ID == idContratante;
        }

        public bool FornecedorIndividual(Fornecedor fornecedor)
        {
            return fornecedor.ATIVO && fornecedor.TIPO_PJPF_ID == 3;
        }

        public bool FornecedorIndividual()
        {
            return ATIVO && TIPO_PJPF_ID == 3;
        }

        public bool AtreladaUmContratante()
        {
            return WFD_CONTRATANTE_PJPF.Any();
        }
    }
}