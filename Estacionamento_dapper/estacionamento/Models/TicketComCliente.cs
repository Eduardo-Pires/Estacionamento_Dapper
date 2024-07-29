using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace estacionamento.Models
{
    public class TicketComCliente
    {
        public Ticket Ticket { get; set; } = default!;
        public Cliente Cliente { get; set; } = default!;
    }
}