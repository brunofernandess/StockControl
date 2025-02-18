namespace StockControl.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Marca { get; set; } = string.Empty;

        public DateTime DataValidade { get; set; }

        public string CodigoProduto { get; set; } = string.Empty;
    }
}
