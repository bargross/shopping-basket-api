using Microsoft.EntityFrameworkCore;
using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.SQL.Repositories
{
    public class BasketItemRepository(IDbContextFactory<AppDbContext> contextFactory) : IBasketITemRepository
    {
        public async Task AddManyAsync(IEnumerable<BasketItem> items)
        {
            await using var context = contextFactory.CreateDbContext();

            await context.BasketItem.AddRangeAsync(items);
        }

        public async Task RemoveAsync(string basketId, string itemId)
        {
            await using var context = contextFactory.CreateDbContext();

            await RemoveManyAsync(basketId, [itemId]);
        }

        public async Task RemoveManyAsync(string basketId, IEnumerable<string> itemIds)
        {
            await using var context = contextFactory.CreateDbContext();

            var items = await context.BasketItem.Where(x => x.BasketId == basketId && itemIds.Contains(x.Id)).ToArrayAsync();

            context.BasketItem.RemoveRange(items);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BasketItem>> GetByBasketIdAsync(string basketId)
        {
            await using var context = contextFactory.CreateDbContext();

            return await context.BasketItem.Where(x => x.BasketId == basketId).ToArrayAsync();
        }
    }
}
