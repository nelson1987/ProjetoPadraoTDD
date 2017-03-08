using System.Collections.Generic;
using System.Linq;

namespace WebForLink.Domain.Entities.Tipos
{
    public class Cliente : Empresa
    {
        public Cliente(string razaoSocial, string documento, TipoEmpresa tipo)
            : base(razaoSocial, documento, tipo)
        {
            //Fabricantes = new List<Fabricante>();
        }
        public int Tipagem { get; private set; }
        public List<Fornecedor> Fornecedores
        {
            get
            {
                return Contratantes.Cast<Fornecedor>().ToList();
            }
        }
        public List<Fabricante> Fabricantes
        {
            get
            {
                return Contratantes.Cast<Fabricante>().ToList();
            }
        }

        //public void AdicionarFabricante(Fabricante Fabricante)
        //{
        //    Fabricantes.Add(Fabricante);
        //}

        //public void AdicionarFornecedor(Fornecedor fornecedor)
        //{
        //    Fornecedores.Add(fornecedor);
        //}
    }
}
