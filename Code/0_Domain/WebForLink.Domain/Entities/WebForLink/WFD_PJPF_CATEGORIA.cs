using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDOR_CATEGORIA
    {
        public FORNECEDOR_CATEGORIA()
        {
            QIC_QUESTIONARIO_CATEGORIA = new List<QUESTIONARIO_CATEGORIA>();
            WFD_CONTRATANTE_PJPF = new List<WFD_CONTRATANTE_PJPF>();
            WFD_PJPF_BASE = new List<FORNECEDORBASE>();
            WFD_PJPF_CATEGORIA1 = new List<FORNECEDOR_CATEGORIA>();
            WFD_SOL_CAD_PJPF = new List<SolicitacaoCadastroFornecedor>();
            ListaDeDocumentosDeFornecedor = new List<ListaDeDocumentosDeFornecedor>();
        }

        public int ID { get; set; }
        public int? CATEGORIA_PAI_ID { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public string CODIGO { get; set; }
        public string DESCRICAO { get; set; }
        public bool ATIVO { get; set; }
        public int? PJPF_CATEGORIA_CH_ID { get; set; }
        public bool ISENTO_DOCUMENTOS { get; set; }
        public bool ISENTO_DADOSBANCARIOS { get; set; }
        public bool ISENTO_CONTATOS { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual FORNECEDOR_CATEGORIA WFD_PJPF_CATEGORIA2 { get; set; }
        public virtual FORNECEDOR_CATEGORIA_CH WFD_PJPF_CATEGORIA_CH { get; set; }
        public virtual ICollection<QUESTIONARIO_CATEGORIA> QIC_QUESTIONARIO_CATEGORIA { get; set; }
        public virtual ICollection<WFD_CONTRATANTE_PJPF> WFD_CONTRATANTE_PJPF { get; set; }
        public virtual ICollection<FORNECEDORBASE> WFD_PJPF_BASE { get; set; }
        public virtual ICollection<FORNECEDOR_CATEGORIA> WFD_PJPF_CATEGORIA1 { get; set; }
        public virtual ICollection<SolicitacaoCadastroFornecedor> WFD_SOL_CAD_PJPF { get; set; }
        public virtual ICollection<ListaDeDocumentosDeFornecedor> ListaDeDocumentosDeFornecedor { get; set; }

        public bool CategoriaVisivelPesquisa(FORNECEDOR_CATEGORIA fornecedor)
        {
            return fornecedor.ATIVO;
        }
    }
}