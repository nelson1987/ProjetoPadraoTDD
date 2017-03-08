using System;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Contato : ISelfValidation
    {
        private Contato()
        {
        }

        public Contato(string nome, string email, string telefone, string celular) : this()
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Celular = celular;
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
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public string Celular { get; private set; }
        public Empresa Empresa { get; private set; }
    }
}