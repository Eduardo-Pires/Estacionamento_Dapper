using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamento.Models;

public class Veiculo
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(10)]
    public string Placa { get; set; } = default!;

    [Required]
    [MaxLength(255)]
    public string Modelo { get; set; } = default!;

    [Required]
    [MaxLength(255)]
    public string Marca { get; set; } = default!;

    [ForeignKey("Cliente")]
    public int ClienteId { get; set; }

    // Navigation properties
    public Cliente Cliente { get; set; } = default!;
    public ICollection<Ticket> Tickets { get; set; } = default!;
}



