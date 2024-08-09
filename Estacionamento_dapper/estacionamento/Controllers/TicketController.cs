using Microsoft.AspNetCore.Mvc;
using estacionamento.Models;
using estacionamento.Repositorios;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using estacionamento.DTO;

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
            PreencherVagas();
            return View();
        }

        [HttpPost("Criar")]
        public IActionResult Criar([FromForm] TicketDTO ticketDTO)
        {  
            Cliente cliente = BuscaOuCadastraClientePorDTO(ticketDTO);
            Veiculo veiculo = BuscaOuCadastraVeiculoPorDTO(ticketDTO, cliente);
             
            Ticket ticket = new Ticket{
                VeiculoId = veiculo.Id,
                VagaId = ticketDTO.VagaId,
                DataEntrada = DateTime.Now
            };
            _repo.Inserir(ticket);

            AlteraStatusVaga(ticketDTO.VagaId, true);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost("apagar")]
        public IActionResult Apagar([FromForm] int id)
        {
            Ticket ticket = _repo.ObterPorId(id);
            _repo.Excluir(id);
            AlteraStatusVaga(ticket.VagaId , false);
            return RedirectToAction(nameof(Index));
        }

        private void PreencherVagas()
        {
            var sql = @"
                SELECT * FROM vagas
                WHERE Ocupada = false 
            ";
            var vagas = _cnn.Query<Vaga>(sql);
            ViewBag.Vagas = new SelectList(vagas, "Id", "CodigoLocalizacao");
        }

        private Cliente BuscaOuCadastraClientePorDTO(TicketDTO ticketDTO)
        {
            Cliente? cliente = null;
        
            if(!string.IsNullOrEmpty(ticketDTO.CPF))
            {
                var query = @"
                    SELECT * FROM Clientes
                    WHERE CPF = @CPF
                ";
                
                cliente = _cnn.QueryFirstOrDefault<Cliente>(query, new { CPF = ticketDTO.CPF });
            }
        
            if (cliente != null)
            {
                return cliente;
            }

            cliente = new Cliente
            {
                Nome = ticketDTO.Nome,
                CPF = ticketDTO.CPF
            };
            
            var sqlInsert = "INSERT INTO Clientes (Nome, CPF) VALUES (@Nome, @CPF);";
            _cnn.Execute(sqlInsert, cliente);
            
            var sqlLastInsertId = "SELECT LAST_INSERT_ID();";
            cliente.Id = _cnn.ExecuteScalar<int>(sqlLastInsertId);
            
            return cliente;
        }

        private Veiculo BuscaOuCadastraVeiculoPorDTO(TicketDTO ticketDTO, Cliente cliente)
        {
            Veiculo? veiculo = null;
        
            if(!string.IsNullOrEmpty(ticketDTO.Placa))
            {
                var query = @"
                    SELECT * FROM veiculos 
                    WHERE Placa = @Placa and ClienteId = @ClienteId
                ";
                
                veiculo = _cnn.QueryFirstOrDefault<Veiculo>(query, new { Placa = ticketDTO.Placa, ClienteId = cliente.Id });
            }
        
            if (veiculo != null)
            {
                return veiculo;
            }

            veiculo = new Veiculo
            {
                Placa = ticketDTO.Placa,
                Marca = ticketDTO.Marca,
                Modelo = ticketDTO.Modelo,
                ClienteId = cliente.Id
            };

            var sql = $"INSERT INTO Veiculos (Placa, Marca, Modelo, ClienteId) VALUES (@Placa, @Marca, @Modelo, @ClienteId); SELECT Last_Insert_Id();";
            veiculo.Id = _cnn.ExecuteScalar<int>(sql, veiculo);

            return veiculo;
        }
        private void AlteraStatusVaga(int vagaId, bool ocupada)
        {
            var sql = $"Update Vagas SET Ocupada = @Ocupada WHERE Id = @Id";
            
            _cnn.Execute(sql, new {Ocupada = ocupada, Id = vagaId });
        }
    }
}


