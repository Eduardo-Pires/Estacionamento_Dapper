using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using estacionamento.Models;
using System.Data;
using Dapper;
using estacionamento.Repositorios;

namespace estacionamento.Controllers;

[Route("/valores")]
public class ValorDoMinutoController : Controller
{
    private readonly IRepositorio<ValorDoMinuto> _repo;

    public ValorDoMinutoController(IRepositorio<ValorDoMinuto> repositorio)
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
    public IActionResult Criar([FromForm] ValorDoMinuto valorDoMinuto)
    {
        _repo.Inserir(valorDoMinuto);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}/Editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        ValorDoMinuto? valor = _repo.ObterPorId(id);

        if (valor == null)
        {
            return NotFound();
        }

        return View(valor);
    }

    [HttpPost("{id}/Alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] ValorDoMinuto valorDoMinuto)
    {
        valorDoMinuto.Id = id;
        _repo.Atualizar(valorDoMinuto);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("apagar")]
    public IActionResult Apagar([FromForm] int id)
    {
        _repo.Excluir(id);

        return RedirectToAction(nameof(Index));
    }
}
