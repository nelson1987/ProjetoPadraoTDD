using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;
using WebForLink.Domain.Builders;

namespace WebForLink.Domain.Tests.Entities
{
    [TestClass]
    public class UsuarioTests
    {
        private Usuario _nelson;
        private Contratante _samarco;
        private Aplicacao _webForLink;
        private Aplicacao _webForMat;

        [TestInitialize]
        public void SetUp()
        {
            _webForLink = new AplicacaoBuilder().Nomeado("WebForLink").Descrito("Cadastro De Fornecedores").Build();
            _webForMat = new AplicacaoBuilder().Nomeado("WebForMat").Descrito("Cadastro de Materiais").Build();
            _samarco = new ClienteAncora("Samarco",_webForLink);
            _nelson = new Usuario("nelson.neto", _samarco);
        }

        [TestMethod]
        public void CriarUsuario()
        {
            Assert.AreEqual(_nelson.Login, "nelson.neto");
            //Assert.AreEqual(_nelson.Aplicacoes.Count, 2);
        }

        [TestMethod]
        public void IncluirUsuarioEmUmContratante()
        {
            _nelson.SetContratante(_samarco);
            Assert.AreEqual(_nelson.Contratante.RazaoSocial, "Samarco");
        }

        [TestMethod]
        public void IncluirUsuarioEmUmContratanteEmDuasAplicacoesDiferentes()
        {
            //_webForLink.AdicionarUsuario(_nelson);
            _nelson.SetContratante(_samarco);
            Assert.AreEqual(_nelson.Contratante.RazaoSocial, "Samarco");
            //Assert.AreEqual(_webForLink.Usuarios.Select(x => x.Contratante).FirstOrDefault(), _samarco);
        }

        [TestMethod]
        public void VerificarPerfilDeUsuario()
        {
            var administrador = new Perfil("Administrador");
            _webForLink.AdicionarPerfil(administrador);
            _nelson.AdicionarPerfil(administrador);
            Assert.AreEqual(_nelson.Perfis.Count, 1);
            Assert.AreEqual(_nelson.Perfis[0].Nome, "Administrador");
        }
    }
}