using System;

namespace WebForLink.Domain.Entities
{
    public class Arquivo
    {
        private Arquivo()
        {
        }

        public Arquivo(string nome)
            : this()
        {
            Nome = nome;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Tipo { get; private set; }
        public DateTime Upload { get; private set; }
        public int Tamanho { get; private set; }
        public string Caminho { get; private set; }
        public Documento Documento { get; private set; }
    }
}