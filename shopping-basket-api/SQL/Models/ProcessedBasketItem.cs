namespace shopping_basket_api.SQL.Models
{
    public class ProcessedBasketItem
    {
        public string Id { get; set; }
        public string BasketId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public DateTime DateProcessed { get; set; }
    }
}
