using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForLink.Domain.Builders;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Domain.Tests.Entities
{
    [TestClass]
    public class ContratanteTests
    {
        private Aplicacao _webForLink;
        private Contratante _samarco;

        [TestInitialize]
        public void SetUp()
        {
            _webForLink = new AplicacaoBuilder()
                .Nomeado("WebForLink")
                .Descrito("Cadastro de Fornecedores")
                .Build();
            _samarco = new ClienteAncora("Samarco", _webForLink);
        }

        [TestMethod]
        public void CriarContratante()
        {
            Assert.AreEqual(_samarco.RazaoSocial, "Samarco");
            Assert.AreEqual(_samarco.Empresas.Count, 0);
            Assert.AreEqual(_samarco.Usuarios.Count, 0);
        }
        [TestMethod]
        public void IncluirClienteNoContratante()
        {
            Usuario nelson = new Usuario("Nelson");
            _samarco.AdicionarUsuario(nelson);
            Assert.AreEqual(_samarco.Usuarios.Count, 1);
            Assert.AreEqual(_samarco.Usuarios[0].Login, "Nelson");
        }

        [TestMethod]
        public void AdicionarEmpresaAUmContratante()
        {
            Empresa sorteq = new Fornecedor("Sorteq", "12345678900", new EmpresaPessoaJuridica());
            Assert.AreEqual(_samarco.Empresas.Count, 0);
            _samarco.AdicionarEmpresa(sorteq);
            Assert.AreEqual(_samarco.Empresas.Count, 1);
        }

        [TestMethod]
        public void ValidarEmpresaContratanteEFornecedorIndividual()
        {
            var samarcoDados = new Cliente("Samarco", "12345678900", new EmpresaPessoaJuridica());
            _samarco.SetDadosGerais(samarcoDados);
            Assert.AreEqual(_samarco.DadosGerais.RazaoSocial, samarcoDados.RazaoSocial);
        }

        [TestMethod]
        public void ValidarEmpresaContratanteEClienteAncora()
        {
            var samarcoDados = new Cliente("Samarco", "12345678900", new EmpresaPessoaJuridica());
            _samarco.SetDadosGerais(samarcoDados);
            Assert.AreEqual(_samarco.DadosGerais.RazaoSocial, samarcoDados.RazaoSocial);
        }
    }
}