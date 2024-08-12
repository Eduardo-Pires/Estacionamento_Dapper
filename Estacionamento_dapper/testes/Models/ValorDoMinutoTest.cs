using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using estacionamento.Models;

namespace testes.Models
{
    [TestClass]
    public class ValorDoMinutoTest
    {
        [TestMethod]
        public void TestandoPropriedadesDoModelValorDoMinuto()
        {
            // Arrange
            var valorDoMinuto = new ValorDoMinuto();

            // Act
            valorDoMinuto.Minutos = 10;
            valorDoMinuto.Valor = 5.99m;

            // Assert
            Assert.AreEqual(10, valorDoMinuto.Minutos);
            Assert.AreEqual(5.99m, valorDoMinuto.Valor);
        }
    }
}
