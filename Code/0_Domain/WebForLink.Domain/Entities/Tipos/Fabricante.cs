using System.Collections.Generic;

namespace WebForLink.Domain.Entities.Tipos
{
    public class Fabricante : Empresa
    {
        public Fabricante(string razaoSocial, string documento)
            : base(razaoSocial, documento, new EmpresaPessoaJuridica())
        {
            Clientes = new List<Cliente>();
            Fornecedores = new List<Fornecedor>();
        }

        public List<Cliente> Clientes { get; private set; }
        public List<Fornecedor> Fornecedores { get; private set; }

        public void AdicionarCliente(Cliente cliente)
        {
            Clientes.Add(cliente);
        }

        public void AdicionarFornecedor(Fornecedor fornecedor)
        {
            Fornecedores.Add(fornecedor);
        }
    }

}
