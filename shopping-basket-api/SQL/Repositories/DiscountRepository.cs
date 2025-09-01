using Microsoft.EntityFrameworkCore;
using shopping_basket_api.SQL.Models;


namespace shopping_basket_api.SQL.Repositories
{
    public class DiscountRepository(IDbContextFactory<AppDbContext> contextFactory) : IDiscountRepository
    {
        public async Task AddManyDiscountsAsync(IEnumerable<Discount> discounts)
        {
            await using var context = contextFactory.CreateDbContext();

            await context.BasketDiscounts.AddRangeAsync(discounts);
        }

        public async Task<List<Discount>> GetDiscountByBasketIdAsync(string basketId)
        {
            await using var context = contextFactory.CreateDbContext();

            return await context.BasketDiscounts.Where(x => x.BasketId == basketId).ToListAsync();
        }
    }
}
