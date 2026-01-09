namespace Core.Models
{
    public class BasketItems
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; } = 1;
        
    }
}