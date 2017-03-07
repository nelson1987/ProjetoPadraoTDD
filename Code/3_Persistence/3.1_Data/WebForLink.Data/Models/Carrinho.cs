using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class Carrinho : ISelfValidation
    {
        public int Id { get; set; }
        public string LoginUsuario { get; set; }
        public string CodigoClienteUsuario { get; set; }
        public System.DateTime DataConvite { get; set; }
        public int StatusConvite { get; set; }
        public string CnpjConvidado { get; set; }
        public string RadicalConvidado { get; set; }
    }
}
