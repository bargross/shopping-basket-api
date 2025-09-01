using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.SQL.Repositories
{
    public interface IBasketITemRepository
    {
        Task AddManyAsync(IEnumerable<BasketItem> items);
        Task RemoveAsync(string basketId, string itemId);
        Task RemoveManyAsync(string basketId, IEnumerable<string> itemIds);
        Task<IEnumerable<BasketItem>> GetByBasketIdAsync(string basketId);
    }
}
