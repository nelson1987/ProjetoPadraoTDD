using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class DocumentoAnexado : ISelfValidation
    {
        public DocumentoAnexado()
        {
            this.Arquivoes = new List<Arquivo>();
        }

        public int Id { get; set; }
        public int IdFichaCadastral { get; set; }
        public Nullable<int> IdDocumentoSolicitado { get; set; }
        public virtual ICollection<Arquivo> Arquivoes { get; set; }
        public virtual FichaCadastral FichaCadastral { get; set; }
    }
}
