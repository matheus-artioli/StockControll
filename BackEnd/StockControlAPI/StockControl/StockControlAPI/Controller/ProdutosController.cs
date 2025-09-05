using Microsoft.AspNetCore.Mvc;
using StockControlAPI.Models;
using StockControlAPI.Repository;

namespace StockControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly ProdutosRepository _operador;

        public ProdutosController(ProdutosRepository operador)
        {
            _operador = operador;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> ObterTodos()
        {
            var todosOsProdutos = await _operador.ObterTodosAsync();
            return Ok(todosOsProdutos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> ObterPorId(int id)
        {
            var produto = await _operador.ObterPorIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }
        
        [HttpGet("por-fornecedor/{fornecedorId}")]
        public async Task<ActionResult<IEnumerable<Produto>>> ObterPorFornecedor(int fornecedorId)
        {
            var produtosDoFornecedor = await _operador.ObterPorFornecedorAsync(fornecedorId);
            if (!produtosDoFornecedor.Any())
            {
                return NotFound();
            }
            return Ok(produtosDoFornecedor);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Adicionar(Produto produto)
        {
            var novoProduto = await _operador.AdicionarAsync(produto);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoProduto.Id_produto }, novoProduto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Produto produto)
        {
            if (id != produto.Id_produto)
            {
                return BadRequest();
            }
            await _operador.AtualizarAsync(produto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var produtoExistente = await _operador.ObterPorIdAsync(id);
            if (produtoExistente == null)
            {
                return NotFound();
            }
            await _operador.RemoverAsync(id);
            return NoContent();
        }
    }
}