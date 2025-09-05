using Microsoft.EntityFrameworkCore;
using StockControlAPI.Data;
using StockControlAPI.Models;

namespace StockControlAPI.Repository
{
    public class PorFornecedorRepository : FornecedoresRepository
    {
        private readonly AppDbContext _baseDeDados;

        public PorFornecedorRepository(AppDbContext baseDeDados)
        {
            _baseDeDados = baseDeDados;
        }

        public async Task<IEnumerable<Fornecedor>> ObterTodosAsync()
        {
            return await _baseDeDados.Fornecedores.ToListAsync();
        }

        public async Task<Fornecedor> ObterPorIdAsync(int id)
        {
            return await _baseDeDados.Fornecedores.FindAsync(id);
        }

        public async Task<Fornecedor> AdicionarAsync(Fornecedor fornecedor)
        {
            _baseDeDados.Fornecedores.Add(fornecedor);
            await _baseDeDados.SaveChangesAsync();
            return fornecedor;
        }

        public async Task AtualizarAsync(Fornecedor fornecedor)
        {
            _baseDeDados.Entry(fornecedor).State = EntityState.Modified;
            await _baseDeDados.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var fornecedorParaRemover = await _baseDeDados.Fornecedores.FindAsync(id);
            if (fornecedorParaRemover != null)
            {
                _baseDeDados.Fornecedores.Remove(fornecedorParaRemover);
                await _baseDeDados.SaveChangesAsync();
            }
        }
    }
}