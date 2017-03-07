using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class DocumentosSolicitacao : ISelfValidation
    {
        public int Id { get; set; }
        public int IdSolicitacao { get; set; }
        public string DescricaoDocumento { get; set; }
        public Nullable<int> IdTipoDocumentoCH { get; set; }
        public virtual Solicitacao Solicitacao { get; set; }
    }
}
