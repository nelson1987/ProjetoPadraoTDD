using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDORBASE
    {
        public FORNECEDORBASE()
        {
            WFD_PJPF_BASE_CONTATOS = new List<FORNECEDORBASE_CONTATOS>();
            WFD_PJPF_BASE_CONVITE = new List<FORNECEDORBASE_CONVITE>();
            WFD_PJPF_BASE_ENDERECO = new List<FORNECEDORBASE_ENDERECO>();
            WFD_PJPF_BASE_UNSPSC = new List<FORNECEDORBASE_UNSPSC>();
            WFD_PJPF_SOLICITACAO_DOCUMENTOS = new List<FORNECEDOR_SOLICITACAO_DOCUMENTOS>();
            WFD_SOLICITACAO = new List<SOLICITACAO>();
        }

        public int ID { get; set; }
        public int? PLANILHA_ID { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public int PJPF_TIPO { get; set; }
        public int? CATEGORIA_ID { get; set; }
        public int? ROBO_ID { get; set; }
        public string CPF { get; set; }
        public string NOME { get; set; }
        public DateTime? DT_NASCIMENTO { get; set; }
        public string CNPJ { get; set; }
        public string RAZAO_SOCIAL { get; set; }
        public string NOME_FANTASIA { get; set; }
        public string CNAE { get; set; }
        public string INSCR_ESTADUAL { get; set; }
        public string INSCR_MUNICIPAL { get; set; }
        public int? TP_LOGRADOURO { get; set; }
        public string ENDERECO { get; set; }
        public string NUMERO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string CEP { get; set; }
        public string BAIRRO { get; set; }
        public string CIDADE { get; set; }
        public string UF { get; set; }
        public int? PAIS { get; set; }
        public string NOME_CONTATO { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONE { get; set; }
        public string CELULAR { get; set; }
        public bool ATIVO { get; set; }
        public DateTime DT_IMPORTACAO { get; set; }
        public bool? EXECUTA_ROBO { get; set; }
        public DateTime? DT_SOLICITACAO_ROBO { get; set; }
        public bool ROBO_EXECUTADO { get; set; }
        public bool ROBO_TENTATIVAS_EXCEDIDAS { get; set; }
        public string COD_ERP { get; set; }
        public bool NOVO_PJPF { get; set; }
        public int? DECISAO_BLOQUEIO { get; set; }
        public bool PRECADASTRO { get; set; }
        public int? STATUS_PRECADASTRO { get; set; }
        public virtual TiposDeEstado T_UF { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual FORNECEDORBASE_IMPORTACAO WFD_PJPF_BASE_IMPORTACAO { get; set; }
        public virtual FORNECEDOR_CATEGORIA WFD_PJPF_CATEGORIA { get; set; }
        public virtual ROBO ROBO { get; set; }
        public virtual TIPO_STATUS_PRECADASTRO WFD_T_STATUS_PRECADASTRO { get; set; }
        public virtual ICollection<FORNECEDORBASE_CONTATOS> WFD_PJPF_BASE_CONTATOS { get; private set; }
        public virtual ICollection<FORNECEDORBASE_CONVITE> WFD_PJPF_BASE_CONVITE { get; private set; }
        public virtual ICollection<FORNECEDORBASE_ENDERECO> WFD_PJPF_BASE_ENDERECO { get; private set; }
        public virtual ICollection<FORNECEDORBASE_UNSPSC> WFD_PJPF_BASE_UNSPSC { get; private set; }

        public virtual ICollection<FORNECEDOR_SOLICITACAO_DOCUMENTOS> WFD_PJPF_SOLICITACAO_DOCUMENTOS { get;
            private set; }

        public virtual ICollection<SOLICITACAO> WFD_SOLICITACAO { get; private set; }
    }
}