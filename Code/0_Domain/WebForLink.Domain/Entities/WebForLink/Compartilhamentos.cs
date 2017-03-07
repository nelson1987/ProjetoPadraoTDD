using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class Compartilhamentos
    {
        public Compartilhamentos()
        {
            DocumentosCompartilhados = new List<DocumentosCompartilhados>();
            WFD_PJPF_BANCO = new List<BancoDoFornecedor>();
            WFD_PJPF_CONTATOS = new List<FORNECEDOR_CONTATOS>();
            WFD_DESTINATARIO = new List<DESTINATARIO>();
        }

        public int ID { get; set; }
        public int? CONTRATANTE_ID { get; set; }
        public DateTime? ENVIADO_EM { get; set; }
        public string ASSUNTO { get; set; }
        public string MENSAGEM { get; set; }
        public bool SEM_PRAZO { get; set; }
        public DateTime? VALIDADE { get; set; }
        public string CHAVE { get; set; }
        public bool? RESTRITA { get; set; }
        public int? USUARIO_ID { get; set; }
        public bool FICHA_CADASTRAL { get; set; }
        public virtual Usuario WFD_USUARIO { get; set; }
        public virtual ICollection<DocumentosCompartilhados> DocumentosCompartilhados { get; set; }
        public virtual Contratante Contratante { get; set; }
        public virtual ICollection<BancoDoFornecedor> WFD_PJPF_BANCO { get; set; }
        public virtual ICollection<FORNECEDOR_CONTATOS> WFD_PJPF_CONTATOS { get; set; }
        public virtual ICollection<DESTINATARIO> WFD_DESTINATARIO { get; set; }
    }
}