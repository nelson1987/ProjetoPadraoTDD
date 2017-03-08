using System;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Endereco : ISelfValidation
    {
        private Endereco()
        {
        }

        public Endereco(string rua, Empresa empresa) : this()
        {
            Rua = rua;
            Empresa = empresa;
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
        public string Rua { get; private set; }
        public Empresa Empresa { get; private set; }
    }
}