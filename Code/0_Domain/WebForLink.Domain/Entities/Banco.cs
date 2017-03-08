using System;
using System.Collections.Generic;
using WebForLink.Domain.Entities.Tipos;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Banco : ISelfValidation
    {
        private Banco()
        {
            SolicitacaoModificacaoBanco = new List<Tipos.SolicitacaoModificacaoBanco>();
        }

        public Banco(string codigo, string nome) : this()
        {
            Codigo = codigo;
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
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public Empresa Contratante { get; private set; }
        public List<SolicitacaoModificacaoBanco> SolicitacaoModificacaoBanco { get; private set; }
    }
}