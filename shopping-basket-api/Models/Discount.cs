using System.ComponentModel.DataAnnotations;

namespace shopping_basket_api.Models
{
    public class Discount
    {
        public string Code { get; set; }

        [MinLength(1), MaxLength(100)]
        public float DiscountValue { get; set; }
    }
}
