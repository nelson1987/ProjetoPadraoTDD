using System.Collections.Generic;

namespace WebForLink.Domain.Entities.Tipos
{
    public class Fornecedor : Empresa
    {
        public Fornecedor(string razaoSocial, string documento, TipoEmpresa tipo)
            : base(razaoSocial, documento, tipo)
        {
            Clientes = new List<Cliente>();
            Fabricantes = new List<Fabricante>();
        }

        public List<Cliente> Clientes { get; private set; }
        public List<Fabricante> Fabricantes { get; private set; }

        public void AdicionarCliente(Cliente cliente)
        {
            Clientes.Add(cliente);
        }

        public void AdicionarFabricante(Fabricante Fabricante)
        {
            Fabricantes.Add(Fabricante);
        }
    }
}