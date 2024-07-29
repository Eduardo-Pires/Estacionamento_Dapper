using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using estacionamento.Repositorios;

namespace estacionamento.Models;
[Table("valores")]
public class ValorDoMinuto
{
    [IgnoreDapper]
    public int Id { get; set; }
    public int Minutos { get; set; }
    public decimal Valor { get; set; }
}

