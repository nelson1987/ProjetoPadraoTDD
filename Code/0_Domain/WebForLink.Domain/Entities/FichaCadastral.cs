using System.Collections.Generic;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class FichaCadastral : ISelfValidation
    {
        public FichaCadastral()
        {
            Banco = new HashSet<Banco>();
            Contato = new HashSet<Contato>();
            Endereco = new HashSet<Endereco>();
            Solicitacao = new HashSet<Solicitacao>();
        }

        public FichaCadastral(int tipoFicha, Solicitacao solicitacaoModel)
            : this()
        {
            Status = tipoFicha;
        }

        public int Id { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Banco> Banco { get; set; }
        public virtual ICollection<Contato> Contato { get; set; }
        public virtual ICollection<Endereco> Endereco { get; set; }
        public virtual ICollection<Solicitacao> Solicitacao { get; set; }

        public bool EhValido
        {
            get
            {
                //var validacaoExterna = new SolicitacaoValidacao();
                //ValidationResult = validacaoExterna.Validar(this);
                return true;
            }
        }

        public ValidationResult ValidationResult { get; private set; }

        public void AlterarSolicitacao(Solicitacao solicitacao)
        {
            Solicitacao.Add(solicitacao);
        }

        public void AdicionarBanco(Banco banco)
        {
            Banco.Add(banco);
        }

        public void AdicionarContato(Contato banco)
        {
            Contato.Add(banco);
        }

        public void AdicionarEndereco(Endereco banco)
        {
            Endereco.Add(banco);
        }
    }
}