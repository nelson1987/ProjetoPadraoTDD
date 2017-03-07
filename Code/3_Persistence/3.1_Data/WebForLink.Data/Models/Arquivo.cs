using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class Arquivo : ISelfValidation
    {
        public int Id { get; set; }
        public int IdDocumentoAnexado { get; set; }
        public string NomeOriginal { get; set; }
        public bool JaBaixado { get; set; }
        public virtual DocumentoAnexado DocumentoAnexado { get; set; }
    }
}
