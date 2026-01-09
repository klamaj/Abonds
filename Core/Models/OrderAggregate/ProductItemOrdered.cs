namespace Core.Models.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string productName)
        {
            ProductItemId = productItemId;
            ProductName = productName;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
    }
}