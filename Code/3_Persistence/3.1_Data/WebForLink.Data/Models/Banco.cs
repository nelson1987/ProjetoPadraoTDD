using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class Banco : ISelfValidation
    {
        public int Id { get; set; }
        public int IdFichaCadastral { get; set; }
        public int Numero { get; set; }
        public string Agencia { get; set; }
        public string AgenciaDv { get; set; }
        public string Conta { get; set; }
        public string ContaDv { get; set; }
        public virtual FichaCadastral FichaCadastral { get; set; }
    }
}
