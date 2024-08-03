using Microsoft.AspNetCore.Mvc;
using estacionamento.Models;
using estacionamento.Repositorios;

namespace estacionamento.Controllers
{
    [Route("/Tickets")]
    public class TicketController : Controller
    {
        private readonly IRepositorio<Ticket> _repo;

        public TicketController(IRepositorio<Ticket> repositorio)
        {
            _repo = repositorio;
        }

            public IActionResult Index()
        {
            var valores = _repo.ObterTodos();
            return View(valores);
        }

        [HttpGet("Novo")]
        public IActionResult Novo()
        {
            return View();
        }

        [HttpPost("Criar")]
        public IActionResult Criar([FromForm] Ticket ticket)
        {   
            _repo.Inserir(ticket);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet("{id}/Editar")]
        public IActionResult Editar([FromRoute] int id)
        {
            Ticket? valor = _repo.ObterPorId(id);

            if (valor == null)
            {
                return NotFound();
            }

            return View(valor);
        }

        [HttpPost("{id}/Alterar")]
        public IActionResult Alterar([FromRoute] int id, [FromForm] Ticket ticket)
        {
            ticket.Id = id;
            _repo.Atualizar(ticket);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost("apagar")]
        public IActionResult Apagar([FromForm] int id)
        {
            _repo.Excluir(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
