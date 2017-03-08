using System;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Arquivo : ISelfValidation
    {
        private Arquivo()
        {
        }

        public Arquivo(string nome)
            : this()
        {
            Nome = nome;
        }

        public int Id { get; set; }
        public bool EhValido { get; }
        public ValidationResult ValidationResult
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        
        public string Nome { get; private set; }
        public string Tipo { get; private set; }
        public DateTime Upload { get; private set; }
        public int Tamanho { get; private set; }
        public string Caminho { get; private set; }
        public Documento Documento { get; private set; }
    }
}