using System;
using System.Collections.Generic;
using System.Linq;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class Contratante
    {
        public Contratante()
        {
            MEU_COMPARTILHAMENTOS = new List<Compartilhamentos>();
            MEU_DOCUMENTOS_COMPARTILHADOS = new List<DocumentosCompartilhados>();
            QIC_QUESTIONARIO = new List<QUESTIONARIO>();
            WAC_PERFIL = new List<Perfil>();
            WFD_CONTRATANTE_ORG_COMPRAS = new List<CONTRATANTE_ORGANIZACAO_COMPRAS>();
            WFD_CONTRATANTE_CONFIG_EMAIL = new List<CONTRATANTE_CONFIGURACAO_EMAIL>();
            WFD_CONTRATANTE_LOG = new List<CONTRATANTE_LOG>();
            WFD_CONTRATANTE_PJPF = new List<WFD_CONTRATANTE_PJPF>();
            WFD_DESCRICAO_DOCUMENTOS = new List<DescricaoDeDocumentos>();
            WFD_DESTINATARIO = new List<DESTINATARIO>();
            WFD_PJPF_BASE_IMPORTACAO = new List<FORNECEDORBASE_IMPORTACAO>();
            WFD_PJPF_BASE = new List<FORNECEDORBASE>();
            WFD_PJPF_CATEGORIA = new List<FORNECEDOR_CATEGORIA>();
            WFD_PJPF_ROBO_LOG = new List<ROBO_LOG>();
            WFD_PJPF_SOLICITACAO = new List<FORNECEDOR_SOLICITACAO>();
            WFD_PJPF = new List<Fornecedor>();
            WFD_SOLICITACAO = new List<SOLICITACAO>();
            WFD_TIPO_DOCUMENTOS = new List<TipoDeDocumento>();
            Usuario = new List<Usuario>();
            WFL_FLUXO_SEQUENCIA = new List<FLUXO_SEQUENCIA>();
            WFL_FLUXO = new List<Fluxo>();
            WFL_PAPEL = new List<Papel>();
            WAC_FUNCAO = new List<FUNCAO>();
            WFD_GRUPO = new List<GRUPO>();
            WFD_USUARIO1 = new List<Usuario>();
        }

        public int ID { get; set; }
        public int? TIPO_CADASTRO_ID { get; set; }
        public string CNPJ { get; set; }
        public string RAZAO_SOCIAL { get; set; }
        public string NOME_FANTASIA { get; set; }
        public DateTime DATA_CADASTRO { get; set; }
        public byte[] LOGO_FOTO { get; set; }
        public string EXTENSAO_IMAGEM { get; set; }
        public string ESTILO { get; set; }
        //public string CONTRANTE_COD_ERP { get; set; }
        public bool? ATIVO { get; set; }
        public DateTime? ATIVO_DT { get; set; }
        public int? USUARIO_ID { get; set; }
        public string COD_WEBFORMAT { get; set; }
        public int? TIPO_CONTRATANTE_ID { get; set; }
        public DateTime? DATA_NASCIMENTO { get; set; }
        public virtual ICollection<Compartilhamentos> MEU_COMPARTILHAMENTOS { get; set; }
        public virtual ICollection<DocumentosCompartilhados> MEU_DOCUMENTOS_COMPARTILHADOS { get; set; }
        public virtual ICollection<QUESTIONARIO> QIC_QUESTIONARIO { get; set; }
        public virtual ICollection<Perfil> WAC_PERFIL { get; set; }
        public virtual ICollection<CONTRATANTE_ORGANIZACAO_COMPRAS> WFD_CONTRATANTE_ORG_COMPRAS { get; set; }
        public virtual ICollection<CONTRATANTE_CONFIGURACAO_EMAIL> WFD_CONTRATANTE_CONFIG_EMAIL { get; set; }
        public virtual CONTRATANTE_CONFIGURACAO WFD_CONTRATANTE_CONFIG { get; set; }
        public virtual ICollection<CONTRATANTE_LOG> WFD_CONTRATANTE_LOG { get; set; }
        public virtual ICollection<WFD_CONTRATANTE_PJPF> WFD_CONTRATANTE_PJPF { get; set; }
        public virtual TIPO_CADASTRO_FORNECEDOR WFD_TIPO_CADASTRO { get; set; }
        public virtual TIPO_CONTRATANTE WFD_TIPO_CONTRATANTE { get; set; }
        public virtual ICollection<DescricaoDeDocumentos> WFD_DESCRICAO_DOCUMENTOS { get; set; }
        public virtual ICollection<DESTINATARIO> WFD_DESTINATARIO { get; set; }
        public virtual ICollection<FORNECEDORBASE_IMPORTACAO> WFD_PJPF_BASE_IMPORTACAO { get; set; }
        public virtual ICollection<FORNECEDORBASE> WFD_PJPF_BASE { get; set; }
        public virtual ICollection<FORNECEDOR_CATEGORIA> WFD_PJPF_CATEGORIA { get; set; }
        public virtual ICollection<ROBO_LOG> WFD_PJPF_ROBO_LOG { get; set; }
        public virtual ICollection<FORNECEDOR_SOLICITACAO> WFD_PJPF_SOLICITACAO { get; set; }
        public virtual ICollection<Fornecedor> WFD_PJPF { get; set; }
        public virtual ICollection<SOLICITACAO> WFD_SOLICITACAO { get; set; }
        public virtual ICollection<TipoDeDocumento> WFD_TIPO_DOCUMENTOS { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
        public virtual ICollection<FLUXO_SEQUENCIA> WFL_FLUXO_SEQUENCIA { get; set; }
        public virtual ICollection<Fluxo> WFL_FLUXO { get; set; }
        public virtual ICollection<Papel> WFL_PAPEL { get; set; }
        public virtual ICollection<FUNCAO> WAC_FUNCAO { get; set; }
        public virtual ICollection<GRUPO> WFD_GRUPO { get; set; }
        public virtual ICollection<Usuario> WFD_USUARIO1 { get; set; }

        public virtual ICollection<SOLICITACAO> SolicitacoesComRoboExecutado
        {
            get { return WFD_SOLICITACAO.Where(x => x.ROBO_EXECUTADO).ToList(); }
        }
    }
}