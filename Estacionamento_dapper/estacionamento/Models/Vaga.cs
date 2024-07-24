using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamento.Models;

public class Vaga
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string CodigoLocalizacao { get; set; } = default!;

    [Required]
    public bool Ocupada { get; set; }

    // Navigation property
    public ICollection<Ticket> Tickets { get; set; } = default!;
}

