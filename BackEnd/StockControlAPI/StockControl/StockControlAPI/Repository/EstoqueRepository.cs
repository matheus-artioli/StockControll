using Microsoft.EntityFrameworkCore;
using StockControlAPI.Data;
using StockControlAPI.Models;

namespace StockControlAPI.Repository
{
    public class EstoqueRepository : ProdutosRepository
    {
        private readonly AppDbContext _baseDeDados;

        public EstoqueRepository(AppDbContext baseDeDados)
        {
            _baseDeDados = baseDeDados;
        }
        public async Task<IEnumerable<Produto>> ObterPorFornecedorAsync(int fornecedorId)
        {
            return await _baseDeDados.Produtos
                .Where(p => p.Id_fornecedor == fornecedorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _baseDeDados.Produtos.ToListAsync();
        }

        public async Task<Produto> ObterPorIdAsync(int id)
        {
            return await _baseDeDados.Produtos.FindAsync(id);
        }

        public async Task<Produto> AdicionarAsync(Produto produto)
        {
            _baseDeDados.Produtos.Add(produto);
            await _baseDeDados.SaveChangesAsync();
            return produto;
        }

        public async Task AtualizarAsync(Produto produto)
        {
            _baseDeDados.Entry(produto).State = EntityState.Modified;
            await _baseDeDados.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var produtoParaRemover = await _baseDeDados.Produtos.FindAsync(id);
            if (produtoParaRemover != null)
            {
                _baseDeDados.Produtos.Remove(produtoParaRemover);
                await _baseDeDados.SaveChangesAsync();
            }
        }
    }
}