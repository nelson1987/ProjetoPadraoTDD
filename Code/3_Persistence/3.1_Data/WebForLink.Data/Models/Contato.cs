using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class Contato : ISelfValidation
    {
        public int Id { get; set; }
        public int IdFichaCadastral { get; set; }
        public string NOME { get; set; }
        public string EMAIL { get; set; }
        public string TELEFONE { get; set; }
        public string CELULAR { get; set; }
        public virtual FichaCadastral FichaCadastral { get; set; }
    }
}
