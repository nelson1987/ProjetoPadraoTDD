using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class Responsavel : ISelfValidation
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int IdSolicitado { get; set; }
        public virtual Solicitado Solicitado { get; set; }
    }
}
