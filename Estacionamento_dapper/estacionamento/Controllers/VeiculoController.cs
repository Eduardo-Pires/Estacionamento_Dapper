using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using estacionamento.Models;
using System.Data;
using Dapper;
using estacionamento.Repositorios;

namespace estacionamento.Controllers;

[Route("/Veiculos")]
public class VeiculoController : Controller
{
    private readonly IRepositorio<Veiculo> _repo;
    private readonly IDbConnection _cnn;

    public VeiculoController(IDbConnection conexao)
    {
        _cnn = conexao;
        _repo = new RepositorioDapper<Veiculo>(_cnn);
    }

    public IActionResult Index()
    {
        var sql = """
            SELECT v.*, c.Id, c.Nome
            FROM veiculos v INNER JOIN clientes c
            ON c.Id = v.ClienteId
        """;
        var valores = _cnn.Query<Veiculo, Cliente, Veiculo>(sql, (veiculo, cliente) => {
            veiculo.Cliente = cliente;
            return veiculo;
        }, splitOn: "Id");
        return View(valores);
    }

    [HttpGet("Novo")]
    public IActionResult Novo()
    {
        return View();
    }

    [HttpPost("Criar")]
    public IActionResult Criar([FromForm] Veiculo veiculo)
    {
        _repo.Inserir(veiculo);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("{id}/Editar")]
    public IActionResult Editar([FromRoute] int id)
    {
        Veiculo? valor = _repo.ObterPorId(id);

        if (valor == null)
        {
            return NotFound();
        }

        return View(valor);
    }

    [HttpPost("{id}/Alterar")]
    public IActionResult Alterar([FromRoute] int id, [FromForm] Veiculo veiculo)
    {
        veiculo.Id = id;
        _repo.Atualizar(veiculo);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("apagar")]
    public IActionResult Apagar([FromForm] int id)
    {
        _repo.Excluir(id);

        return RedirectToAction(nameof(Index));
    }
}
