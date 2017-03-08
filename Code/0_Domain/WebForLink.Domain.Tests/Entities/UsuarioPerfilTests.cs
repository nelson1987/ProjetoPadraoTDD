using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForLink.Domain.Builders;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Domain.Tests.Entities
{
    [TestClass]
    class UsuarioPerfilTests
    {
        [TestInitialize]
        public void SetUp()
        {
        }
        [TestMethod]
        public void CriarUsuario()
        {
            #region Usuarios
            Usuario nelson = new Usuario("nelson.neto");
            Usuario carlos = new Usuario("carlos.jesus");
            Usuario diego = new Usuario("diego.messeri");
            #endregion
            #region Aplicações
            Aplicacao webForLink = new AplicacaoBuilder().Nomeado("WFL").Descrito("WebForLink").Build();
            Aplicacao webNotForn = new AplicacaoBuilder().Nomeado("WNF").Descrito("WebNotForn").Build();
            Aplicacao webPourBreak = new AplicacaoBuilder().Nomeado("WPB").Descrito("WebPourBreak").Build();
            Aplicacao vendorList = new AplicacaoBuilder().Nomeado("VL").Descrito("VendorList").Build();
            Aplicacao surplus = new AplicacaoBuilder().Nomeado("SP").Descrito("Surplus").Build();

            #endregion

            Perfil visualizador = new Perfil("Visualizador");
            Perfil solicitante = new Perfil("Solicitante");
            Perfil administrador = new Perfil("Administrador");

            //Nelson -> Aplicacoes
            nelson.AdicionarPerfilNumaAplicacao(webForLink, visualizador);
            nelson.AdicionarPerfilNumaAplicacao(webNotForn, administrador);
            nelson.AdicionarPerfilNumaAplicacao(webPourBreak, visualizador);
            //Carlos -> Aplicacoes
            carlos.AdicionarPerfilNumaAplicacao(vendorList, solicitante);
            //Diego -> Aplicacoes
            diego.AdicionarPerfilNumaAplicacao(surplus, administrador);

            Contratante SorteqWebForLink = new FornecedorIndividual("Sorteq"
                , new EmpresaPessoaJuridica(), webForLink);
            Contratante SorteqWebNotForn = new FornecedorIndividual("Sorteq"
                , new EmpresaPessoaJuridica(), webNotForn);
            Contratante SorteqWebPourBreak = new FornecedorIndividual("Sorteq"
                , new EmpresaPessoaJuridica(), webPourBreak);

            //fornecedorIndividual.AdicionarUsuario(nelson);
            //fornecedorIndividual.AdicionarUsuario(carlos);
            //fornecedorIndividual.AdicionarUsuario(diego);

            //--Validações
            Assert.AreEqual(nelson.Login, "nelson.neto");
            //Assert.AreEqual(nelson.Aplicacoes.Count, 3);
            //Assert.AreEqual(carlos.Aplicacoes.Count, 1);
            //Assert.AreEqual(diego.Aplicacoes.Count, 1);
            //Assert.AreEqual(nelson.Aplicacoes[0].Perfis.Count, 1);
            //Assert.AreEqual(nelson.Aplicacoes[1].Perfis.Count, 1);
            //Assert.AreEqual(nelson.Aplicacoes[2].Perfis.Count, 1);
            //Assert.AreEqual(fornecedorIndividual.Usuarios.Count, 1);
        }
    }
}
