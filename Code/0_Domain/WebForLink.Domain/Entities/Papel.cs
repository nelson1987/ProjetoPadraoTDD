using System;
using System.Collections.Generic;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Papel : ISelfValidation
    {
        private Papel()
        {
        }

        public Papel(string nome)
            : this()
        {
            Nome = nome;
        }
        
        public string Nome { get; private set; }
        public Contratante Contratante { get; private set; }
        public List<Usuario> Usuarios { get; private set; }
        public List<Etapa> Etapas { get; private set; }
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