using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using estacionamento.Repositorios;

namespace estacionamento.Models;

[Table("clientes")]
public class Cliente
{
    [IgnoreDapper]
    public int Id { get; set; } = default!;

    public string? Nome { get; set; }

    public string? CPF { get; set; }

    public string? Telefone { get; set; }
}
