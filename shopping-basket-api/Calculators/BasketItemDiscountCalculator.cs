using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.Calculators
{
    public class BasketItemDiscountCalculator: IBasketCalculator<BasketItem>
    {
        public float CalculateResult(BasketItem item, bool withVAT)
        {
            var discountedPrice = item.Discount != default ? item.Price - (item.Price * item.Discount) : item.Price;

            return withVAT ? discountedPrice + (discountedPrice * (float)0.2) : discountedPrice;
        }
    }
}
