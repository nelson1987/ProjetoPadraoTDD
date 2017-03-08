using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Domain.Tests.Entities
{
    [TestClass]
    public class ContratanteEmpresaTests
    {
        [TestMethod]
        public void ValidarContratante()
        {
            Aplicacao webforlink = new Aplicacao();

            Contratante ch = new ClienteAncora("CH", webforlink);
            Contratante sorteq = new FornecedorIndividual("Sorteq", new EmpresaPessoaJuridica(), webforlink);

            Empresa chCliente = new Cliente("CH", "123.4-90", new EmpresaPessoaJuridica());
            Empresa sorteqFornecedor = new Fornecedor("Sorteq", "123.4-91", new EmpresaPessoaJuridica());
            Empresa sandvikFornecedor = new Fornecedor("Sandvik", "123.4-92", new EmpresaPessoaJuridica());
            Empresa sorteqCliente = new Cliente("Sorteq", "123.4-91", new EmpresaPessoaJuridica());

            Assert.AreEqual(ch.Empresas.Count, 0);
            ch.AdicionarEmpresa(chCliente);
            ch.AdicionarEmpresa(sorteqFornecedor);
            ch.AdicionarEmpresa(sandvikFornecedor);

            Assert.AreEqual(ch.Empresas.Count, 3);
            Assert.AreEqual(ch.Empresas[0].Documento, ch.DadosGerais.Documento);
            Assert.AreEqual(ch.Empresas[1].RazaoSocial, "Sorteq");

            sorteq.AdicionarEmpresa(sorteqFornecedor);
        }
    }
}
