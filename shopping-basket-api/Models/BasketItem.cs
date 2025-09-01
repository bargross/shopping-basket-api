namespace shopping_basket_api.Models
{
    public class BasketItem
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public float? Discount { get; set; } // in %
    }
}
