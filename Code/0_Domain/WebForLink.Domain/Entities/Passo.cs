using System;
using System.Collections.Generic;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Passo : ISelfValidation
    {
        private Passo()
        {
            Papeis = new List<Papel>();
        }

        public Passo(string descricao, params Papel[] papel)
            : this()
        {
            Descricao = descricao;
            Papeis.AddRange(papel);
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
        public string Descricao { get; private set; }
        public bool Aprovado { get; private set; }
        public List<Papel> Papeis { get; private set; }

        public void Aprovar()
        {
            Aprovado = true;
        }

        public void Reprovar()
        {
            Aprovado = false;
        }
    }
}