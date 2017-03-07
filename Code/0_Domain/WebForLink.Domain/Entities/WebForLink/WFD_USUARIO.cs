using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class Usuario
    {
        public Usuario()
        {
            MEU_COMPARTILHAMENTOS = new List<Compartilhamentos>();
            WAC_ACESSO_LOG = new List<WAC_ACESSO_LOG>();
            WFD_PJPF_BASE_CONVITE = new List<FORNECEDORBASE_CONVITE>();
            WFD_PJPF_BASE_IMPORTACAO = new List<FORNECEDORBASE_IMPORTACAO>();
            WFD_PJPF_DOCUMENTOS_VERSAO = new List<VersionamentoDeDocumentoDoFornecedor>();
            WFD_SOLICITACAO = new List<SOLICITACAO>();
            WFD_SOLICITACAO_PRORROGACAO = new List<SOLICITACAO_PRORROGACAO>();
            WFD_SOLICITACAO_TRAMITE = new List<SOLICITACAO_TRAMITE>();
            WFD_USUARIO_SENHAS_HIST = new List<USUARIO_SENHAS>();
            WAC_PERFIL = new List<Perfil>();
            WFD_CONTRATANTE1 = new List<Contratante>();
            WFL_PAPEL = new List<Papel>();
        }

        public int ID { get; set; }
        public int? CONTRATANTE_ID { get; set; }
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public string EMAIL_ALTERNATIVO { get; set; }
        public string SENHA { get; set; }
        public DateTime? DAT_NASCIMENTO { get; set; }
        public bool? PRINCIPAL { get; set; }
        public string TROCAR_SENHA { get; set; }
        public string CPF_CNPJ { get; set; }
        public bool ATIVO { get; set; }
        public string CARGO { get; set; }
        public string FIXO { get; set; }
        public string CELULAR { get; set; }
        public string LOGIN { get; set; }
        public string LOGIN_SSO { get; set; }
        public string DOMINIO { get; set; }
        public bool PRIMEIRO_ACESSO { get; set; }
        public DateTime? DT_ATIVACAO { get; set; }
        public DateTime? DT_CRIACAO { get; set; }
        public int? CONTA_TENTATIVA { get; set; }
        public int EXPIRA_EM_DIAS { get; set; }
        public virtual Contratante Contratante { get; set; }
        public virtual ICollection<Compartilhamentos> MEU_COMPARTILHAMENTOS { get; set; }
        public virtual ICollection<WAC_ACESSO_LOG> WAC_ACESSO_LOG { get; set; }
        public virtual ICollection<FORNECEDORBASE_CONVITE> WFD_PJPF_BASE_CONVITE { get; set; }
        public virtual ICollection<FORNECEDORBASE_IMPORTACAO> WFD_PJPF_BASE_IMPORTACAO { get; set; }
        public virtual ICollection<VersionamentoDeDocumentoDoFornecedor> WFD_PJPF_DOCUMENTOS_VERSAO { get; set; }
        public virtual ICollection<SOLICITACAO> WFD_SOLICITACAO { get; set; }
        public virtual ICollection<SOLICITACAO_PRORROGACAO> WFD_SOLICITACAO_PRORROGACAO { get; set; }
        public virtual ICollection<SOLICITACAO_TRAMITE> WFD_SOLICITACAO_TRAMITE { get; set; }
        public virtual ICollection<USUARIO_SENHAS> WFD_USUARIO_SENHAS_HIST { get; set; }
        public virtual ICollection<Perfil> WAC_PERFIL { get; set; }
        public virtual ICollection<Contratante> WFD_CONTRATANTE1 { get; set; }
        public virtual ICollection<Papel> WFL_PAPEL { get; set; }
    }
}