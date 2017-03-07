using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class Endereco : ISelfValidation
    {
        public int Id { get; set; }
        public int IdFichaCadastral { get; set; }
        public int Tipo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public string Pais { get; set; }
        public virtual FichaCadastral FichaCadastral { get; set; }
    }
}
