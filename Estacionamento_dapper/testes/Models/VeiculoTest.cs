using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using estacionamento.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace testes.Models
{
    [TestClass]
    public class VeiculoTest
    {
        [TestMethod]
        public void TestandoPropriedadesDoModelVeiculo()
        {
            // Arrange
            Veiculo veiculo = new Veiculo();
            int expectedId = 1;
            string expectedPlaca = "ABC123";
            string expectedModelo = "Sedan";
            string expectedMarca = "Ford";
            int expectedClienteId = 2;
            Cliente expectedCliente = new Cliente();

            // Act
            veiculo.Id = expectedId;
            veiculo.Placa = expectedPlaca;
            veiculo.Modelo = expectedModelo;
            veiculo.Marca = expectedMarca;
            veiculo.ClienteId = expectedClienteId;
            veiculo.Cliente = expectedCliente;

            // Assert
            Assert.AreEqual(expectedId, veiculo.Id);
            Assert.AreEqual(expectedPlaca, veiculo.Placa);
            Assert.AreEqual(expectedModelo, veiculo.Modelo);
            Assert.AreEqual(expectedMarca, veiculo.Marca);
            Assert.AreEqual(expectedClienteId, veiculo.ClienteId);
            Assert.AreEqual(expectedCliente, veiculo.Cliente);
        }
    }
}