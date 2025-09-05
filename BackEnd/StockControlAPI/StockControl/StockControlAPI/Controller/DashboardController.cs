using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockControlAPI.Data;
using StockControlAPI.Models;

namespace StockControlAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet("stats")]
        public async Task<IActionResult> GetDashboardStats()
        {
            try
            {
                var totalProdutos = await _context.Produtos.CountAsync();
                var totalFornecedores = await _context.Fornecedores.CountAsync();
                
                var totalSaidas = await _context.Movimentos
                    .CountAsync(m => m.Tipo == "Saída");

                var valorGastoEmCompras = await _context.Movimentos
                    .Where(m => m.Tipo == "Entrada")
                    .SumAsync(m => m.Quantidade * m.PrecoUnitario);

                var stats = new DashboardStats
                {
                    TotalProdutos = totalProdutos,
                    TotalFornecedores = totalFornecedores,
                    TotalSaidas = totalSaidas,
                    ValorGastoEmCompras = valorGastoEmCompras
                };

                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
        
        [HttpGet("alerta-estoque")]
        public async Task<IActionResult> GetAlertaEstoque()
        {
            try
            {
                var produtosEmAlerta = await _context.Produtos
                    .Where(p => p.Qtd_estoque < p.EstoqueMinimo)
                    .OrderBy(p => p.Qtd_estoque)
                    .Select(p => new 
                    {
                        p.Id_produto,
                        p.Nome,
                        p.Qtd_estoque,
                        p.EstoqueMinimo
                    })
                    .ToListAsync();

                return Ok(produtosEmAlerta);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
        
        [HttpGet("top-vendas")]
        public async Task<IActionResult> GetTopVendas()
        {
            try
            {
                var topVendas = await _context.Movimentos
                    .Where(m => m.Tipo == "Saída")
                    .GroupBy(m => new { m.Id_produto, m.Produto.Nome })
                    .Select(g => new TopVendaDto
                    {
                        IdProduto = g.Key.Id_produto,
                        NomeProduto = g.Key.Nome,
                        TotalVendido = g.Sum(m => m.Quantidade)
                    })
                    .OrderByDescending(x => x.TotalVendido)
                    .Take(5)
                    .ToListAsync();

                return Ok(topVendas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }
    }
}