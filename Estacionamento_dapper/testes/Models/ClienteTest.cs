using System;
using System.Collections.Generic;
using estacionamento.Models;
using Microsoft.AspNetCore.Components.RenderTree;

[TestClass]
public class ClienteTest
{

    [TestMethod]
    public void TestandoPropriedadesDoModelCliente()
    {    
        //arrange
        var cliente = new Cliente();

        //act
        cliente.Id = 1;
        cliente.CPF = "109.716.320-22";
        cliente.Nome = "Danila";

        //assert
        Assert.AreEqual(1, cliente.Id);
        Assert.AreEqual("109.716.320-22", cliente.CPF);
        Assert.AreEqual("Danila", cliente.Nome);

    }
}
