using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamento.Models;

public class Ticket
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime DataEntrada { get; set; }

    public DateTime? DataSaida { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Valor { get; set; }

    [ForeignKey("Veiculo")]
    public int VeiculoId { get; set; }

    [ForeignKey("Vaga")]
    public int VagaId { get; set; }

    // Navigation properties
    public Veiculo Veiculo { get; set; } = default!;
    public Vaga Vaga { get; set; } = default!;
}

