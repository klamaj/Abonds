namespace Core.Models
{
    public class CustomerBasket
    {
        public CustomerBasket(string id)
        {
            Id = id;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public CustomerBasket() {}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public string Id { get; set; }
        public List<BasketItems> Items { get; set; } = new List<BasketItems>();
    }

}