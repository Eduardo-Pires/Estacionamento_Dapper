using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamento.Models;

public class Cliente
{
    [Key]
    public int Id { get; set; } = default!;

    [MaxLength(255)]
    public string? Nome { get; set; }

    [MaxLength(14)]
    public string? CPF { get; set; }

    [MaxLength(15)]
    public string? Telefone { get; set; }

    // Navigation property
    public ICollection<Veiculo> Veiculos { get; set; } = default!;
}
