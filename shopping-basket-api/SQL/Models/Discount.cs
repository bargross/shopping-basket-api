namespace shopping_basket_api.SQL.Models
{
    public class Discount
    {
        public string Id { get; set; } // code
        public string BasketId { get; set; }
        public float DiscountValue { get; set; }
    }
}
