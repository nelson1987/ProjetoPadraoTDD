using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForLink.Domain.Entities;

namespace WebForLink.Domain.Tests.Entities
{
    [TestClass]
    public class ArquivoTests
    {
        [TestMethod]
        public void CriarArquivo()
        {
            var file = new Arquivo("index.html");
            Assert.AreEqual(file.Tamanho, 0);
        }

        [TestMethod]
        public void AdicionarArquivoAUmDocumento()
        {
            var file = new Arquivo("index.html");
            var carteiraDeHabilitacao = new Documento("Carteira de Habilitação");
            carteiraDeHabilitacao.AdicionarArquivo(file);
            Assert.AreEqual(carteiraDeHabilitacao.Arquivos.Count, 1);
        }
    }
}