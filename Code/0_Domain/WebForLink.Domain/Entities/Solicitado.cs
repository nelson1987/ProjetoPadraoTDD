using System.Collections.Generic;
using WebForLink.Domain.Entities.Validations;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Solicitado : ISelfValidation
    {
        private string cnpj;

        public Solicitado()
        {
            Responsaveis = new List<Responsavel>();
            Solicitacoes = new List<Solicitacao>();
        }

        public Solicitado(string cnpj, Responsavel responsavel)
            : this()
        {
            //Id = 0;
            if (responsavel != null)
                Responsaveis.Add(responsavel);

            this.cnpj = cnpj;
        }

        public int Id { get; set; }
        public string RazaoSocial { get; set; }

        public string Cnpj
        {
            get { return cnpj.Replace("/", "").Replace(".", "").Replace("-", ""); }
            set { cnpj = value; }
        }

        public string Cidade { get; set; }
        public string Estado { get; set; }
        public virtual ICollection<Responsavel> Responsaveis { get; set; }
        public virtual ICollection<Solicitacao> Solicitacoes { get; set; }

        public bool EhValido
        {
            get
            {
                var validacaoExterna = new SolicitadoValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }

        public void AdicionarResponsavel(Responsavel responsavel)
        {
            if (responsavel != null)
                Responsaveis.Add(responsavel);
        }
    }
}