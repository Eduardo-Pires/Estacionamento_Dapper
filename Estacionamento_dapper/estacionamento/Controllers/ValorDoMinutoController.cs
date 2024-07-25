using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using estacionamento.Models;
using System.Data;
using Dapper;

namespace estacionamento.Controllers;

[Route("/valores")]
public class ValorDoMinutoController : Controller
{
    private readonly IDbConnection _connection;

    public ValorDoMinutoController(IDbConnection connection)
    {
        _connection = connection;
    }

    public IActionResult Index()
    {
        var valores = _connection.Query<ValorDoMinuto>("SELECT * FROM valores");
        return View(valores);
    }

    [HttpGet("/novo")]
    public IActionResult Novo()
    {
        return View();
    }

    [HttpPost("/Criar")]
    public IActionResult Criar([FromForm] ValorDoMinuto valorDoMinuto)
    {
        var sql = "INSERT INTO valores (Minutos, Valor) VALUES (@Minutos, @Valor)";
        _connection.Execute(sql, valorDoMinuto);

        return RedirectToAction(nameof(Index));
    }
}
