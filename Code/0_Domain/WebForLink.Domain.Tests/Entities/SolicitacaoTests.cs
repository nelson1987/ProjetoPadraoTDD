using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForLink.Domain.Builders;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Domain.Tests.Entities
{
    [TestClass]
    public class SolicitacaoTests
    {
        private Aplicacao _webForLink;
        private Contratante _samarco;
        private Usuario _nelson;
        private TipoEmpresa _pessoaJuridica;
        private Empresa _sorteq;

        [TestInitialize]
        public void SetUp()
        {
            _webForLink = new AplicacaoBuilder().Nomeado("WebForLink").Descrito("Cadastro de Fornecedores").Build();
            _samarco = new ClienteAncora("Samarco", _webForLink);
            _nelson = new Usuario("nelson.neto", _samarco);
            _pessoaJuridica = new EmpresaPessoaJuridica();
            _sorteq = new Fornecedor("Sorteq", "12345678900", _pessoaJuridica);
        }

        [TestMethod]
        public void CriarSolicitacao()
        {
            Solicitacao criacaoFornecedor = new SolicitacaoCadastro(_nelson, _sorteq);
            Assert.AreEqual(criacaoFornecedor.Criador.Contratante, _nelson.Contratante);
            Assert.AreEqual(criacaoFornecedor.Contratante, _nelson.Contratante);
            Assert.AreEqual(criacaoFornecedor.Solicitado, _sorteq);
        }

        [TestMethod]
        public void CriarSolicitacaoCriacaoFornecedor()
        {
            Solicitacao criacaoFornecedor = new SolicitacaoCadastro(_nelson, _sorteq);
            Assert.AreEqual(criacaoFornecedor.Criador, _nelson);
            Assert.AreEqual(criacaoFornecedor.Contratante, _nelson.Contratante);
        }
    }
}