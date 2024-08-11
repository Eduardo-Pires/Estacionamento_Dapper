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
        if (DataSaida == null)
        {
            var valor = valorDoMinuto.Valor / valorDoMinuto.Minutos;
            TimeSpan diferenca = DateTime.Now - DataEntrada;
            int minutos = (int)diferenca.TotalMinutes;
            return minutos * valor;
        }
        else
        {
            var valor = valorDoMinuto.Valor / valorDoMinuto.Minutos;
            TimeSpan diferenca = DataSaida.Value - DataEntrada;
            int minutos = (int)diferenca.TotalMinutes;
            return minutos * valor;
        }
        
    }
}

