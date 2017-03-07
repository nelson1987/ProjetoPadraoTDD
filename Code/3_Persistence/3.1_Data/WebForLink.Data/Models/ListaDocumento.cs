using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class ListaDocumento : ISelfValidation
    {
        public int Id { get; set; }
        public int IdListaSolicitante { get; set; }
        public int IdTipoDocumentoCH { get; set; }
        public int IdDescricaoDocumentoCH { get; set; }
        public string Descricao { get; set; }
        public virtual ListasSolicitante ListasSolicitante { get; set; }
    }
}
