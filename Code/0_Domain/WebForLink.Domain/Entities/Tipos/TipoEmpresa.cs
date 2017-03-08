using System.Collections.Generic;

namespace WebForLink.Domain.Entities.Tipos
{
    public abstract class TipoEmpresa
    {
        private TipoEmpresa()
        {
        }

        protected TipoEmpresa(string nome)
            : this()
        {
            Nome = nome;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public List<Contratante> Contratantes { get; private set; }
    }

    public class EmpresaPessoaJuridica : TipoEmpresa
    {
        public EmpresaPessoaJuridica()
            : base("Pessoa Jurídica")
        {
        }
    }

    public class EmpresaPessoaFisica : TipoEmpresa
    {
        public EmpresaPessoaFisica()
            : base("Pessoa Física")
        {
        }
    }

    public class EmpresaEstrangeira : TipoEmpresa
    {
        public EmpresaEstrangeira()
            : base("Estrangeira")
        {
        }
    }
}