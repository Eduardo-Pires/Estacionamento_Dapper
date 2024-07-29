using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using estacionamento.Repositorios;

namespace estacionamento.Models;
[Table("vagas")]
public class Vaga
{
    [IgnoreDapper]
    public int Id { get; set; }
    public string CodigoLocalizacao { get; set; } = default!;
    public bool Ocupada { get; set; }
}

