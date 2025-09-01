using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.SQL.Repositories
{
    public interface IDiscountRepository
    {
        Task AddManyDiscountsAsync(IEnumerable<Discount> discounts);
        Task<List<Discount>> GetDiscountByBasketIdAsync(string basketId);
    }
}
