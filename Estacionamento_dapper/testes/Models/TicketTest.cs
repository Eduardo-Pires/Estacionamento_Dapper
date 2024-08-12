using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using estacionamento.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Org.BouncyCastle.Asn1.Misc;

namespace testes.Models
{
    [TestClass]
    public class TicketTest
    {
        [TestMethod]
        public void TestandoPropriedadesDoModelTicket()
        {
            // Arrange
            Ticket ticket = new Ticket();
            int expectedId = 1;
            DateTime expectedDataEntrada = DateTime.Now;
            DateTime? expectedDataSaida = null;
            decimal expectedValor = 0;
            int expectedVeiculoId = 2;
            int expectedVagaId = 3;
            Veiculo expectedVeiculo = new Veiculo();
            Vaga expectedVaga = new Vaga();

            // Act
            ticket.Id = expectedId;
            ticket.DataEntrada = expectedDataEntrada;
            ticket.DataSaida = expectedDataSaida;
            ticket.Valor = expectedValor;
            ticket.VeiculoId = expectedVeiculoId;
            ticket.VagaId = expectedVagaId;
            ticket.Veiculo = expectedVeiculo;
            ticket.Vaga = expectedVaga;

            // Assert
            Assert.AreEqual(expectedId, ticket.Id);
            Assert.AreEqual(expectedDataEntrada, ticket.DataEntrada);
            Assert.AreEqual(expectedDataSaida, ticket.DataSaida);
            Assert.AreEqual(expectedValor, ticket.Valor);
            Assert.AreEqual(expectedVeiculoId, ticket.VeiculoId);
            Assert.AreEqual(expectedVagaId, ticket.VagaId);
            Assert.AreEqual(expectedVeiculo, ticket.Veiculo);
            Assert.AreEqual(expectedVaga, ticket.Vaga);
        }

        [TestMethod]
        public void TestandoMetodoValorTotalSemDataSaida()
        {
            // Arrange
            Ticket ticket = new Ticket();
            ValorDoMinuto valorDoMinuto = new ValorDoMinuto();
            valorDoMinuto.Valor = 2;
            valorDoMinuto.Minutos = 120;
            ticket.DataEntrada = DateTime.Now.AddHours(-1);
            var valorDesejado = 1.0m;
            // Act
            decimal valorTotal = ticket.ValorTotal(valorDoMinuto);

            // Assert
            Assert.AreEqual(valorDesejado, (int)valorTotal); 

        }
        [TestMethod]
        public void TestandoMetodoValorTotalComDataSaida()
        {
            // Arrange
            Ticket ticket = new Ticket();
            ValorDoMinuto valorDoMinuto = new ValorDoMinuto();
            valorDoMinuto.Valor = 1;
            valorDoMinuto.Minutos = 1;
            ticket.DataEntrada = DateTime.Now;
            ticket.DataSaida = DateTime.Now.AddHours(1);
            var valorDesejado = 60.0m; 
            // Act
            decimal valorTotal = ticket.ValorTotal(valorDoMinuto);

            // Assert
            Assert.AreEqual(valorDesejado, valorTotal); 
        }

        [TestMethod]
        public void TestandoValorPagoDoTicket()
        {
            // Arrange
            Ticket ticket = new Ticket();
            ValorDoMinuto valorDoMinuto = new ValorDoMinuto();
            valorDoMinuto.Valor = 1;
            valorDoMinuto.Minutos = 1;
            ticket.DataEntrada = DateTime.Now.AddHours(-1);
            decimal valorTotal = ticket.ValorTotal(valorDoMinuto);
           
            // Act
            ticket.FecharTicket(valorDoMinuto);
            bool valorPago = ticket.Valor == valorTotal;

            // Assert
            Assert.IsTrue(valorPago); 
            Assert.IsNotNull(ticket.DataSaida);
        }
        
    }
}