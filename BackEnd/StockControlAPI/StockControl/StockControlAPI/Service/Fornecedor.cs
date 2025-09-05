using System.Collections.Generic;

namespace StockControlAPI.Models
{
    public class Fornecedor
    {
        public Fornecedor()
        {
            Produtos = new List<Produto>();
        }

        public int Id_fornecedor { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        
        public ICollection<Produto> Produtos { get; set; }
    }
}