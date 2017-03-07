using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class Solicitacao : ISelfValidation
    {
        public Solicitacao()
        {
            this.DocumentosSolicitacaos = new List<DocumentosSolicitacao>();
        }

        public int Id { get; set; }
        public int IdSolicitante { get; set; }
        public int IdSolicitado { get; set; }
        public Nullable<int> IdFichaCadastral { get; set; }
        public virtual ICollection<DocumentosSolicitacao> DocumentosSolicitacaos { get; set; }
        public virtual FichaCadastral FichaCadastral { get; set; }
        public virtual Solicitado Solicitado { get; set; }
        public virtual Solicitante Solicitante { get; set; }
    }
}
