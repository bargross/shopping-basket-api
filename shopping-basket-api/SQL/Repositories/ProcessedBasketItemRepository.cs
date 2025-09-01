using Microsoft.EntityFrameworkCore;
using shopping_basket_api.SQL.Models;

namespace shopping_basket_api.SQL.Repositories
{
    public class ProcessedBasketItemRepository(IDbContextFactory<AppDbContext> contextFactory) : IProcessedBasketItemRepository
    {
        public async Task AddManyAsync(IEnumerable<ProcessedBasketItem> items)
        {
            await using var context = contextFactory.CreateDbContext();

            await context.ProcessedBasketItem.AddRangeAsync(items);

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProcessedBasketItem>> GetByBasketIdAsync(string basketId)
        {
            await using var context = contextFactory.CreateDbContext();

            return await context.ProcessedBasketItem.Where(x => x.BasketId == basketId).ToArrayAsync();
        }
    }
}
