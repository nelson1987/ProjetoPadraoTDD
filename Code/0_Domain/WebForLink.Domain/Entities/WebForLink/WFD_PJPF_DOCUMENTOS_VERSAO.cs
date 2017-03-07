using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class VersionamentoDeDocumentoDoFornecedor
    {
        public int ID { get; set; }
        public int DOCUMENTO_ID { get; set; }
        public int ARQUIVO_ID { get; set; }
        public DateTime DATA_UPLOAD { get; set; }
        public DateTime? DATA_VENCIMENTO { get; set; }
        public string NOME_ARQUIVO { get; set; }
        public int USUARIO_ID { get; set; }
        public virtual DocumentosDoFornecedor DocumentosDoFornecedor { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}