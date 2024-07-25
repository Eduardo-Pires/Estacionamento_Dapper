using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace estacionamento.Models;

public class ValorDoMinuto
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Minutos { get; set; }

    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public decimal Valor { get; set; }
}

