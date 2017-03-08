using System;
using System.Collections.Generic;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Documento : ISelfValidation
    {
        private Documento()
        {
            Arquivos = new List<Arquivo>();
        }

        public Documento(string nome)
            : this()
        {
            Nome = nome;
        }
        
        public string Nome { get; private set; }
        public List<Arquivo> Arquivos { get; private set; }
        public Empresa Empresa { get; private set; }

        public void AdicionarArquivo(params Arquivo[] file)
        {
            Arquivos.AddRange(file);
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
    }
}