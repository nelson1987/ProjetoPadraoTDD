using System;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Teste : ISelfValidation
    {
        public Teste(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public bool EhValido
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }

        public ValidationResult ValidationResult
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
