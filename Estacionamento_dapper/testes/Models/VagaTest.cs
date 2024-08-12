using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using estacionamento.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testes.Models
{
    [TestClass]
    public class VagaTest
    {
        [TestMethod]
        public void TestandoPropriedadesDoModelVaga()
        {
            // Arrange
            Vaga vaga = new Vaga();
            
            // Act
            vaga.Id = 1;
            vaga.CodigoLocalizacao = "A1";
            vaga.Ocupada = true;
            
            // Assert
            Assert.AreEqual(1, vaga.Id);
            Assert.AreEqual("A1", vaga.CodigoLocalizacao);
            Assert.IsTrue(vaga.Ocupada);
        }
    }
}