using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using estacionamento.Models;
using System.Data;
using Dapper;
using estacionamento.Repositorios;

namespace estacionamento.Controllers;

[Route("/Clientes")]
public class ClienteController : Controller
{
    private readonly IRepositorio<Cliente> _repo;

    public ClienteController(IRepositorio<Cliente> repositorio)
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
    public IActionResult Criar([FromForm] Cliente cliente)
    {
        _repo.Inserir(cliente);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}/Editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        Cliente? valor = _repo.ObterPorId(id);

        if (valor == null)
        {
            return NotFound();
        }

        return View(valor);
    }

    [HttpPost("{id}/Alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] Cliente cliente)
    {
        cliente.Id = id;
        _repo.Atualizar(cliente);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("apagar")]
    public IActionResult Apagar([FromForm] int id)
    {
        _repo.Excluir(id);

        return RedirectToAction(nameof(Index));
    }
}
