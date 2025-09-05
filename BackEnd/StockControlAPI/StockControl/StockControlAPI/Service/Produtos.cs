using System;

namespace StockControlAPI.Models
{
    public class Produto
    {
        public int Id_produto { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public int EstoqueMinimo { get; set; }
        public decimal Peso { get; set; }
        public string Marca { get; set; }
        public decimal Preco { get; set; }
        public int Qtd_estoque { get; set; }
        public int Id_fornecedor { get; set; }
        public DateTime Data_entrada { get; set; }
        public DateTime Data_saida { get; set; }
        public Fornecedor? Fornecedor { get; set; }
        
    }
}