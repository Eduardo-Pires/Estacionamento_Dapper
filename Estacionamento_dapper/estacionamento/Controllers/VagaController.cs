using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using estacionamento.Models;
using System.Data;
using Dapper;
using estacionamento.Repositorios;

namespace estacionamento.Controllers;

[Route("/Vagas")]
public class VagaController : Controller
{
    private readonly IRepositorio<Vaga> _repo;

    public VagaController(IRepositorio<Vaga> repositorio)
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
    public IActionResult Criar([FromForm] Vaga vaga)
    {
        _repo.Inserir(vaga);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}/Editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        Vaga? valor = _repo.ObterPorId(id);

        if (valor == null)
        {
            return NotFound();
        }

        return View(valor);
    }

    [HttpPost("{id}/Alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] Vaga vaga)
    {
        vaga.Id = id;
        _repo.Atualizar(vaga);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("apagar")]
    public IActionResult Apagar([FromForm] int id)
    {
        _repo.Excluir(id);

        return RedirectToAction(nameof(Index));
    }
}
