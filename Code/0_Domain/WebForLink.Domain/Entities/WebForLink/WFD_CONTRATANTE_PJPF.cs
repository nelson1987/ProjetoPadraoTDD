using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class WFD_CONTRATANTE_PJPF
    {
        public WFD_CONTRATANTE_PJPF()
        {
            BancoDoFornecedor = new List<BancoDoFornecedor>();
            WFD_PJPF_CONTATOS = new List<FORNECEDOR_CONTATOS>();
            WFD_PJPF_DOCUMENTOS = new List<DocumentosDoFornecedor>();
            WFD_PJPF_ENDERECO = new List<FORNECEDOR_ENDERECO>();
            WFD_PJPF_INFORM_COMPL = new List<FORNECEDOR_INFORM_COMPL>();
        }

        public int ID { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public int PJPF_ID { get; set; }
        public int TP_PJPF { get; set; }
        public string PJPF_COD_ERP { get; set; }
        public int? CATEGORIA_ID { get; set; }
        public int? CRIA_USUARIO_ID { get; set; }
        public int? PJPF_STATUS_ID { get; set; }
        public int? PJPF_STATUS_TP_SOL { get; set; }
        public int? PJPF_STATUS_ID_SOL { get; set; }
        public DateTime? CRIA_DT { get; set; }
        public DateTime? PJPF_STATUS_DT { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual Fornecedor WFD_PJPF { get; set; }
        public virtual FORNECEDOR_CATEGORIA WFD_PJPF_CATEGORIA { get; set; }
        public virtual FORNECEDOR_STATUS WFD_PJPF_STATUS { get; set; }
        public virtual TIPO_FORNECEDOR WFD_T_TP_PJPF { get; set; }
        public virtual ICollection<BancoDoFornecedor> BancoDoFornecedor { get; set; }
        public virtual ICollection<FORNECEDOR_CONTATOS> WFD_PJPF_CONTATOS { get; set; }
        public virtual ICollection<DocumentosDoFornecedor> WFD_PJPF_DOCUMENTOS { get; set; }
        public virtual ICollection<FORNECEDOR_ENDERECO> WFD_PJPF_ENDERECO { get; set; }
        public virtual ICollection<FORNECEDOR_INFORM_COMPL> WFD_PJPF_INFORM_COMPL { get; set; }
    }
}