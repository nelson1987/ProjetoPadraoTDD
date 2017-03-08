using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForLink.Domain.Builders;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Domain.Tests.Entities
{
    [TestClass]
    public class SolicitacaoCadastroTests
    {
        private Aplicacao _webForLink;
        private Contratante _samarco;
        private Usuario _nelson;
        private TipoEmpresa _pessoaJuridica;
        private Empresa _sorteq;

        [TestInitialize]
        public void SetUp()
        {
            _webForLink = new AplicacaoBuilder().Nomeado("WebForLink").Nomeado("Cadastro de Fornecedores").Build();
            _samarco = new ClienteAncora("Samarco",_webForLink);
            _nelson = new Usuario("nelson.neto", _samarco);
            _pessoaJuridica = new EmpresaPessoaJuridica();
            _sorteq = new Fornecedor("Sorteq", "12345678900", _pessoaJuridica);
        }

        [TestMethod]
        public void CriarSolicitacaoDeCadastro()
        {
            Solicitacao solicitacaoDeCadastro = new SolicitacaoCadastro(_nelson, _sorteq);
        }

        [TestMethod]
        public void CriarSolicitacaoDeFornecedorComFluxo()
        {
            TipoEmpresa pessoaJuridica = new EmpresaPessoaJuridica();
            Empresa sorteq = new Fornecedor("Sorteq", "12345678900", pessoaJuridica);
            Solicitacao solicitacaoCadastro = new SolicitacaoCadastro(_nelson, sorteq);

            Assert.IsNull(solicitacaoCadastro.Fluxo);

            var cadastro = new Fluxo(new FluxoCriacao(), _samarco, pessoaJuridica);
            var a = new Etapa("A");
            var b = new Etapa("B");
            var c = new Etapa("C");
            cadastro.AdicionarPassos(a, new Passo("A.1"), new Passo("A.2"));
            cadastro.AdicionarPassos(b, new Passo("B.1"));
            cadastro.AdicionarPassos(c, new Passo("C.1"), new Passo("C.2"), new Passo("C.3"));

            solicitacaoCadastro.SetFluxo(cadastro);

            Assert.AreEqual(solicitacaoCadastro.EtapaAtual, a);
            cadastro.AprovarPasso(new Passo("A.2"));
            Assert.AreEqual(solicitacaoCadastro.EtapaAtual, a);
            cadastro.AprovarPasso(new Passo("A.1"));
            Assert.AreEqual(solicitacaoCadastro.EtapaAtual, b);
            //---
            cadastro.AprovarPasso(new Passo("B.1"));
            Assert.AreEqual(solicitacaoCadastro.EtapaAtual, c);
            //---
            cadastro.AprovarPasso(new Passo("C.3"));
            Assert.AreEqual(solicitacaoCadastro.EtapaAtual, c);
            cadastro.AprovarPasso(new Passo("C.1"));
            Assert.AreEqual(solicitacaoCadastro.EtapaAtual, c);
            cadastro.ReprovarPasso(new Passo("C.1"));
            Assert.AreEqual(solicitacaoCadastro.EtapaAtual, c);
            cadastro.AprovarPasso(new Passo("C.1"));
            Assert.AreEqual(solicitacaoCadastro.EtapaAtual, c);
            cadastro.AprovarPasso(new Passo("C.2"));
            Assert.IsNull(solicitacaoCadastro.EtapaAtual);


            //var cadastroDeFornecedor = new Fluxo(cadastroFornecedor, _samarco, _pessoaJuridica);
            //var cadastroFornecedor = new TipoFluxo("Cadastro de Fornecedor");
            //solicitacaoCadastro.Tipo.SetFluxo(cadastroDeFornecedor);
            //cadastroDeFornecedor.AdicionarEtapas(new Etapa("Solicitacao"), new Etapa("MDA"), new Etapa("Conclusão"));
            //Assert.AreEqual(cadastroDeFornecedor.EtapaAtual.Nome, "Solicitacao");
            //Assert.AreEqual(solicitacaoCadastro.Tipo.Fluxo.EtapaAtual.Nome, "Solicitacao");
        }
    }
}