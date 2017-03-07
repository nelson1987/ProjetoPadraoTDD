using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class FichaCadastral : ISelfValidation
    {
        public FichaCadastral()
        {
            this.Bancoes = new List<Banco>();
            this.Contatoes = new List<Contato>();
            this.DocumentoAnexadoes = new List<DocumentoAnexado>();
            this.Enderecoes = new List<Endereco>();
            this.Solicitacaos = new List<Solicitacao>();
        }

        public int Id { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Banco> Bancoes { get; set; }
        public virtual ICollection<Contato> Contatoes { get; set; }
        public virtual ICollection<DocumentoAnexado> DocumentoAnexadoes { get; set; }
        public virtual ICollection<Endereco> Enderecoes { get; set; }
        public virtual ICollection<Solicitacao> Solicitacaos { get; set; }
    }
}
