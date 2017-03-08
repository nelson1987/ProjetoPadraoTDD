using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebForLink.Domain.Entities;
using System.Collections.Generic;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Domain.Tests.Entities
{
    [TestClass]
    public class FichaTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ficha = new FichaCompartilhada(new Compartilhamento(new Usuario("nelson"), new List<Documento>()));

        }
    }
}
