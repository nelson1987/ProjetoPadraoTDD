using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public partial class Solicitante : ISelfValidation
    {
        public Solicitante()
        {
            this.ListasSolicitantes = new List<ListasSolicitante>();
            this.Solicitacaos = new List<Solicitacao>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Codigocliente { get; set; }
        public virtual ICollection<ListasSolicitante> ListasSolicitantes { get; set; }
        public virtual ICollection<Solicitacao> Solicitacaos { get; set; }
    }
}
