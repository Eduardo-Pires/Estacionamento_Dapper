using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using estacionamento.Repositorios;

namespace estacionamento.Models;
[Table("tickets")]
public class Ticket
{
    [IgnoreDapper]
    public int Id { get; set; }

    public DateTime DataEntrada { get; set; }

    public DateTime? DataSaida { get; set; }

    public decimal Valor { get; set; }

    public int VeiculoId { get; set; }
    public int VagaId { get; set; }

    [IgnoreDapper]
    public Veiculo Veiculo { get; set; } = default!;
    [IgnoreDapper]
    public Vaga Vaga { get; set; } = default!;

    public decimal ValorTotal(ValorDoMinuto valorDoMinuto)
    {
        TimeSpan diferenca;
        int minutos;
        var valorPorMinuto = valorDoMinuto.Valor / valorDoMinuto.Minutos;

        if (DataSaida == null)
        {
            diferenca = DateTime.Now - DataEntrada;
        }
        else
        {
            diferenca = DataSaida.Value - DataEntrada;
        }
        
        minutos = (int)diferenca.TotalMinutes;
        return valorPorMinuto * minutos;
    }

    public void FecharTicket(ValorDoMinuto valorDoMinuto)
    {
        if (DataSaida != null)
        {
            throw new Exception("Ticket já fechado");
        }

        if (DataEntrada > DateTime.Now)
        {
            throw new Exception("Data de entrada maior que a data atual");
        }

        if (valorDoMinuto == null)
        {
            throw new Exception("Valor do minuto não encontrado");
        }

        DataSaida = DateTime.Now;
        Valor = ValorTotal(valorDoMinuto);
    }
}

