using System.Collections.Generic;
using WebForLink.Domain.Entities;

namespace WebForLink.Domain
{
    public class Etapa
    {
        protected Etapa()
        {
            Passos = new List<Passo>();
        }

        public Etapa(string nome)
            : this()
        {
            Nome = nome;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public bool Aprovado { get; private set; }
        public List<Passo> Passos { get; private set; }
        public List<Papel> Papeis { get; private set; }

        public void AdicionarPassos(Passo[] passos)
        {
            Passos.AddRange(passos);
        }

        internal void SetAprovado(bool aprovado)
        {
            Aprovado = aprovado;
        }
    }
}