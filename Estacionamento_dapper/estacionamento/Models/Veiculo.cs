using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using estacionamento.Repositorios;

namespace estacionamento.Models;
[Table("veiculos")]
public class Veiculo
{
    [IgnoreDapper]
    public int Id { get; set; }
    public string Placa { get; set; } = default!;
    public string Modelo { get; set; } = default!;
    public string Marca { get; set; } = default!;
    public int ClienteId { get; set; }
    [IgnoreDapper]
    public Cliente Cliente { get; set; } = default!;
}



