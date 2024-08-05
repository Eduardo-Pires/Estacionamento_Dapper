using Microsoft.AspNetCore.Mvc;
using estacionamento.Models;
using estacionamento.Repositorios;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace estacionamento.Controllers
{
    [Route("/Tickets")]
    public class TicketController : Controller
    {
        private readonly IRepositorio<Ticket> _repo;
        private readonly IRepositorio<Vaga> _repoVaga;
        private readonly IDbConnection _cnn;

        public TicketController(IDbConnection conexao)
        {
            _cnn = conexao;
            _repo = new RepositorioDapper<Ticket>(_cnn);
        }

        public IActionResult Index()
        {
            var sql = @"
            SELECT t.*, v.*, c.*
            FROM tickets t INNER JOIN veiculos v ON v.Id = t.VeiculoId
                           INNER JOIN clientes c ON c.Id = v.ClienteId
            ";
        var valores = _cnn.Query<Ticket, Veiculo, Cliente, Ticket>(sql, (ticket,veiculo, cliente) => {
            veiculo.Cliente = cliente;
            ticket.Veiculo = veiculo;
            return ticket;
        }, splitOn: "Id");
        return View(valores);
        }

        [HttpGet("Novo")]
        public IActionResult Novo()
        {
            var Vagas = _repoVaga.ObterTodos();
            ViewBag.Vagas = new SelectList(Vagas, "Id", "Nome");
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
