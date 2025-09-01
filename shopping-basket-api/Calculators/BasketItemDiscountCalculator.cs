using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.Calculators
{
    public class BasketItemDiscountCalculator: IBasketCalculator<BasketItem>
    {
        public float CalculateResult(BasketItem item, bool withVAT)
        {
            var discountValue = item.Discount != default ? (item.Discount / 100.0) : 0;
            var vatValue = withVAT ? (float)(0.2 * item.Price) : 0; 
            var priceReductionValue = (float)discountValue * item.Price;

            return (item.Price - priceReductionValue) + vatValue;
        }
    }
}
