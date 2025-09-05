using Microsoft.AspNetCore.Mvc;
using StockControlAPI.Models;
using StockControlAPI.Repository;

namespace StockControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FornecedoresController : ControllerBase
    {
        private readonly FornecedoresRepository _gestor;

        public FornecedoresController(FornecedoresRepository gestor)
        {
            _gestor = gestor;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fornecedor>>> ObterTodos()
        {
            var todosOsFornecedores = await _gestor.ObterTodosAsync();
            return Ok(todosOsFornecedores);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Fornecedor>> ObterPorId(int id)
        {
            var fornecedor = await _gestor.ObterPorIdAsync(id);
            if (fornecedor == null)
            {
                return NotFound();
            }
            return Ok(fornecedor);
        }
        
        [HttpPost]
        public async Task<ActionResult<Fornecedor>> Adicionar(Fornecedor fornecedor)
        {
            var novoFornecedor = await _gestor.AdicionarAsync(fornecedor);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoFornecedor.Id_fornecedor }, novoFornecedor);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, Fornecedor fornecedor)
        {
            if (id != fornecedor.Id_fornecedor)
            {
                return BadRequest();
            }
            await _gestor.AtualizarAsync(fornecedor);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var fornecedorExistente = await _gestor.ObterPorIdAsync(id);
            if (fornecedorExistente == null)
            {
                return NotFound();
            }
            await _gestor.RemoverAsync(id);
            return NoContent();
        }
    }
}