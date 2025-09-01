using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.SQL.Repositories
{
    public interface IProcessedBasketItemRepository
    {
        Task AddManyAsync(IEnumerable<ProcessedBasketItem> items);
        Task<IEnumerable<ProcessedBasketItem>> GetByBasketIdAsync(string basketId);
    }
}
