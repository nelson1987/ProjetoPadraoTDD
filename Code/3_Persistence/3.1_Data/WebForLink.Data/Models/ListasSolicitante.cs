using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class ListasSolicitante : ISelfValidation
    {
        public ListasSolicitante()
        {
            this.ListaDocumentoes = new List<ListaDocumento>();
        }

        public int Id { get; set; }
        public int IdSolicitante { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<ListaDocumento> ListaDocumentoes { get; set; }
        public virtual Solicitante Solicitante { get; set; }
    }
}
