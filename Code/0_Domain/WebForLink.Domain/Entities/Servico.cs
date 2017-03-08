using System;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Servico : ISelfValidation
    {
        public int Id { get; set; }
        public bool EhValido { get; }
        public ValidationResult ValidationResult
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}