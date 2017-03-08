using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public class Papel
    {
        private Papel()
        {
        }

        public Papel(string nome)
            : this()
        {
            Nome = nome;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public Contratante Contratante { get; private set; }
        public List<Usuario> Usuarios { get; private set; }
        public List<Etapa> Etapas { get; private set; }
    }
}