using StockControlAPI.Models;

namespace StockControlAPI.Repository
{
    public interface FornecedoresRepository
    {
        Task<IEnumerable<Fornecedor>> ObterTodosAsync();
        Task<Fornecedor> ObterPorIdAsync(int id);
        Task<Fornecedor> AdicionarAsync(Fornecedor fornecedor);
        Task AtualizarAsync(Fornecedor fornecedor);
        Task RemoverAsync(int id);
    }
}