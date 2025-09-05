using StockControlAPI.Models;

namespace StockControlAPI.Repository
{
    public interface ProdutosRepository
    {
        Task<IEnumerable<Produto>> ObterPorFornecedorAsync(int fornecedorId);
        Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<Produto> ObterPorIdAsync(int id);
        Task<Produto> AdicionarAsync(Produto produto);
        Task AtualizarAsync(Produto produto);
        Task RemoverAsync(int id);
    }
}