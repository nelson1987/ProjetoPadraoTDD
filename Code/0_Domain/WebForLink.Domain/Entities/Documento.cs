using System;

namespace WebForLink.Domain.Entities
{
    public class Documento
    {
        public Documento()
        {

        }
        public int Id { get; private set; }
        public DateTime Criacao { get; private set; }
        public string Nome { get; private set; }
    }
}
