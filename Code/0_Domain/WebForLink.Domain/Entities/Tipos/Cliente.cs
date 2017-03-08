using System.Collections.Generic;

namespace WebForLink.Domain.Entities.Tipos
{
    public class Cliente : Empresa
    {
        public Cliente(string razaoSocial, string documento, TipoEmpresa tipo)
            : base(razaoSocial, documento, tipo)
        {
            Fornecedores = new List<Fornecedor>();
            Fabricantes = new List<Fabricante>();
        }

        public List<Fornecedor> Fornecedores { get; private set; }
        public List<Fabricante> Fabricantes { get; private set; }

        public void AdicionarFabricante(Fabricante Fabricante)
        {
            Fabricantes.Add(Fabricante);
        }

        public void AdicionarFornecedor(Fornecedor fornecedor)
        {
            Fornecedores.Add(fornecedor);
        }
    }
}
