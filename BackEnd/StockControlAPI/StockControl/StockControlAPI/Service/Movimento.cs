using StockControlAPI.Models;

public class Movimento
{
    public int Id_mov { get; set; }
    public int Id_produto { get; set; }
    public string Tipo { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public DateTime Data { get; set; }
    public Produto Produto { get; set; }
}