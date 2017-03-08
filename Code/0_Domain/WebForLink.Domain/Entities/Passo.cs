using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public class Passo
    {
        private Passo()
        {
            Papeis = new List<Papel>();
        }

        public Passo(string descricao, params Papel[] papel)
            : this()
        {
            Descricao = descricao;
            Papeis.AddRange(papel);
        }

        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public bool Aprovado { get; private set; }
        public List<Papel> Papeis { get; private set; }

        public void Aprovar()
        {
            Aprovado = true;
        }

        public void Reprovar()
        {
            Aprovado = false;
        }
    }
}