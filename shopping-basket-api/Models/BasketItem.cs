using System.ComponentModel.DataAnnotations;

namespace shopping_basket_api.Models
{
    public class BasketItem
    {
        public string Name { get; set; }
        public float Price { get; set; }

        [MinLength(1), MaxLength(100)]
        public float? Discount { get; set; } // in %
    }
}
