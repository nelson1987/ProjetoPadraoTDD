using System;
using System.Collections.Generic;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Perfil : ISelfValidation
    {
        protected Perfil()
        {
        }

        public Perfil(string nome) : this()
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
        public List<Usuario> Usuarios { get; private set; }
        public Contratante Contratante { get; private set; }
    }
}