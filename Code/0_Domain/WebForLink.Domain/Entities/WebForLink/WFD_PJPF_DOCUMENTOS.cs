using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class DocumentosDoFornecedor
    {
        public DocumentosDoFornecedor()
        {
            MEU_DOCUMENTOS_COMPARTILHADOS = new List<DocumentosCompartilhados>();
            WFD_PJPF_DOCUMENTOS_VERSAO = new List<VersionamentoDeDocumentoDoFornecedor>();
            WFD_SOL_DOCUMENTOS = new List<SolicitacaoDeDocumentos>();
        }

        public int ID { get; set; }
        public int PJPF_ID { get; set; }
        public int? ARQUIVO_ID { get; set; }
        public string NOME_ARQUIVO { get; set; }
        public int DESCRICAO_DOCUMENTO_ID { get; set; }
        public DateTime? DATA_UPLOAD { get; set; }
        public DateTime? DATA_VENCIMENTO { get; set; }
        public int? SOLICITACAO_ID { get; set; }
        public string EXTENSAO_ARQUIVO { get; set; }
        public int? CONTRATANTE_PJPF_ID { get; set; }
        public int? LISTA_DOCUMENTO_ID { get; set; }
        public bool? EXIGE_VALIDADE { get; set; }
        public int? PERIODICIDADE_ID { get; set; }
        public bool? OBRIGATORIO { get; set; }
        public DateTime? DATA_EMISSAO { get; set; }
        public bool ATIVO { get; set; }
        public bool SEM_VALIDADE { get; set; }
        public virtual ARQUIVOS WFD_ARQUIVOS { get; set; }
        public virtual WFD_CONTRATANTE_PJPF WFD_CONTRATANTE_PJPF { get; set; }
        public virtual DescricaoDeDocumentos DescricaoDeDocumentos { get; set; }
        public virtual Fornecedor WFD_PJPF { get; set; }
        public virtual ListaDeDocumentosDeFornecedor WFD_PJPF_LISTA_DOCUMENTOS { get; set; }
        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual TIPO_PERIODICIDADE WFD_T_PERIODICIDADE { get; set; }
        public virtual ICollection<DocumentosCompartilhados> MEU_DOCUMENTOS_COMPARTILHADOS { get; set; }
        public virtual ICollection<VersionamentoDeDocumentoDoFornecedor> WFD_PJPF_DOCUMENTOS_VERSAO { get; set; }
        public virtual ICollection<SolicitacaoDeDocumentos> WFD_SOL_DOCUMENTOS { get; set; }
    }
}