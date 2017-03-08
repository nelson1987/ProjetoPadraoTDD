using System.Collections.Generic;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Domain.Entities
{
    public class Banco
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

        public int Id { get; private set; }
        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public Empresa Contratante { get; private set; }
        public List<SolicitacaoModificacaoBanco> SolicitacaoModificacaoBanco { get; private set; }
    }
}