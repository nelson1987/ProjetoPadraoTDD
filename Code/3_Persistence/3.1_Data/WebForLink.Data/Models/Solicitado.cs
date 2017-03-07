using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class Solicitado : ISelfValidation
    {
        public Solicitado()
        {
            this.Responsavels = new List<Responsavel>();
            this.Solicitacaos = new List<Solicitacao>();
        }

        public int Id { get; set; }
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public virtual ICollection<Responsavel> Responsavels { get; set; }
        public virtual ICollection<Solicitacao> Solicitacaos { get; set; }
    }
}
