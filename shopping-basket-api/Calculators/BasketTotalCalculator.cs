using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.Calculators
{
    public class BasketTotalCalculator(IBasketCalculator<BasketItem> discountCalculator): IBasketCalculator<IEnumerable<BasketItem>>
    {
        public float CalculateResult(IEnumerable<BasketItem> items, bool withVAT)
        {
            return items.Select(x => discountCalculator.CalculateResult(x, withVAT)).Sum();
        }
    }
}
